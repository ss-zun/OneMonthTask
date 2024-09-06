using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    public EnemyData data;

    public Animator anim;
    private AnimationData animData = new AnimationData();

    public Coroutine moveCoroutine;
    private Vector2 endPoint;
    
    private UnityAction onRespawnEnemy;

    #region 체력바
    private int currentHealth;
    private Image hpFillBar;
    public SpriteRenderer enemySkin;
    public BoxCollider2D enemyCollider;
    #endregion

    public void Init(EnemyData enemyData, UnityAction onRespawn)
    {
        animData.Init();
        hpFillBar = GameManager.Instance.UIManager.HpFillBar;
        hpFillBar.fillAmount = 1f;
        endPoint = GameManager.Instance.Spawner.endPoint;

        data = enemyData;
        currentHealth = data.Health;
        onRespawnEnemy = onRespawn;
        
        anim.SetBool(animData.DieParameterHash, false);
        anim.SetBool(animData.HitParameterHash, false);

        UpdateColliderSize();

        moveCoroutine = StartCoroutine(MoveToEndPoint());
    }

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(CalculateHpPos());
        GameManager.Instance.UIManager.HpBar.transform.position = screenPos;
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
        GameManager.Instance.ObjectPool.ReturnToPool("Enemy", gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth, data.Health);
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

    private void SetHealth(float currentHealth, float maxHealth)
    {
        hpFillBar.fillAmount = currentHealth / maxHealth;
    }

    private Vector3 CalculateHpPos()
    {
        float headTopY = enemySkin.bounds.max.y; // 스프라이트의 Y축 상단
        return new Vector3(enemySkin.bounds.center.x, headTopY + 0.3f, 0);
    }

    public void UpdateColliderSize()
    {
        if (enemySkin != null && enemyCollider != null)
        {
            enemyCollider.size = enemySkin.bounds.size;
            enemyCollider.offset = Vector2.zero;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.UIManager.ShowEnemyInfoPopup(data);
    }
}
