using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public Animator anim;
    public Coroutine moveCoroutine;

    private AnimationData animData = new AnimationData();
    private Vector2 endPoint;
    private int currentHealth;
    private UnityAction onRespawnEnemy;

    public void Init(EnemyData enemyData, UnityAction onRespawn)
    {
        animData.Init();
        endPoint = GameManager.Instance.Spawner.endPoint;

        data = enemyData;
        currentHealth = data.Health;
        onRespawnEnemy = onRespawn;

        anim.SetBool(animData.DieParameterHash, false);
        anim.SetBool(animData.HitParameterHash, false);

        moveCoroutine = StartCoroutine(MoveToEndPoint());
    }

    private IEnumerator MoveToEndPoint()
    {
        while (!IsDie() && Vector2.Distance(transform.position, endPoint) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoint, data.Speed * Time.deltaTime);
            yield return null;
        }

        // 끝 지점에 도달
        onRespawnEnemy?.Invoke();
        OnEnemyDie();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (IsDie())
        {
            anim.SetTrigger(animData.DieParameterHash);
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            onRespawnEnemy?.Invoke();
        }
        else
        {
            anim.SetTrigger(animData.HitParameterHash);
        }
    }

    private bool IsDie()
    {
        return currentHealth <= 0;
    }

    public void OnEnemyDie()
    {
        GameManager.Instance.ObjectPool.ReturnToPool("Enemy", gameObject);
    }
}
