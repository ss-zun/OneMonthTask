using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectState : StateMachineBehaviour
{
    public float attackRadius = 14f; // ���� ����
    public LayerMask enemyLayer;
    private Player player;
    private Collider2D playerCollider;
    private Collider2D currentEnemy; // ���� Ÿ��
    private float lastDetectionTime = 0f; // ������ ���� �ð�
    private float detectionInterval = 1f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<Player>();
        playerCollider = animator.GetComponent<Collider2D>();
        lastDetectionTime = Time.time;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���� �ð��� ������ ���� �ð����� 1�� �̻� ����� ��� ���� ����
        if (Time.time >= lastDetectionTime + detectionInterval)
        {
            lastDetectionTime = Time.time; // ���� �� �ð� ����

            // �÷��̾��� �߾��� ���
            Vector2 playerCenter = playerCollider.bounds.center;

            // �߾��� �������� �� ����
            currentEnemy = Physics2D.OverlapCircle(playerCenter, attackRadius, enemyLayer);

            // ���� �����Ǹ� ����
            if (currentEnemy != null)
            {
                animator.SetTrigger(player.animData.AttackParameterHash);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
