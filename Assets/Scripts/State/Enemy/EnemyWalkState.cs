using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : StateMachineBehaviour
{
    private bool colliderUpdated = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        colliderUpdated = false;  // ���� ���� �� �÷��� �ʱ�ȭ
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!colliderUpdated)
        {
            // ��������Ʈ�� ����� �� �ݶ��̴� ũ�⸦ �� ���� ������Ʈ
            animator.GetComponentInParent<Enemy>().UpdateColliderSize();
            colliderUpdated = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        colliderUpdated = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
