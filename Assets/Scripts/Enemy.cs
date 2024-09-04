using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public int maxHealth = 100;
    private int currentHealth;
    private AnimationData animData = new AnimationData();

    private void Start()
    {
        animData.Init();
        currentHealth = maxHealth;
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

    public bool IsDie()
    {
        if(currentHealth <= 0)
            return true;
        
        return false;
    }

    public void Die()
    {
        // 풀에 반환
    }
}
