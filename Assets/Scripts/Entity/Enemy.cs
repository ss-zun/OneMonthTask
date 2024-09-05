using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public Animator anim;
    private int currentHealth;
    private AnimationData animData = new AnimationData();

    private void Start()
    {
        data = new EnemyData();
        animData.Init();       
    }

    private void OnEnable()
    {
        currentHealth = data.Health;
    }

    public void TakeDamage()
    {
        if (IsDie())
        {
            anim.SetBool(animData.DieParameterHash, true);
            Die();
        }
        else
        {
            anim.SetBool(animData.HitParameterHash, true);
        }
    }

    private bool IsDie()
    {
        if(currentHealth <= 0)
            return true;
        
        return false;
    }

    private void Die()
    {
        GameManager.Instance.ObjectPool.ReturnToPool("Enemy", gameObject);
    }
}
