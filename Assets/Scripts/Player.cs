using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    void Update()
    {
        anim.SetBool("Attack", false);
        if (Input.GetKeyDown(KeyCode.S))
            anim.SetBool("Attack", true);
    }
}
