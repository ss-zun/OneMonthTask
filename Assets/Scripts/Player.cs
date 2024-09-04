using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform firePoint;

    void Update()
    {
        anim.SetBool("Attack", false);
        if (Input.GetKeyDown(KeyCode.S))
            anim.SetBool("Attack", true);
    }

    public void FireArrow()
    {
        GameManager.Instance.ObjectPool.SpawnFromPool("Arrow", firePoint.position, firePoint.rotation);
    }
}
