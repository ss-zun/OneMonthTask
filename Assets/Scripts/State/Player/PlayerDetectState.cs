using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectState : StateMachineBehaviour
{
    public float attackRadius = 14f; // 감지 범위
    public LayerMask enemyLayer;
    private Player player;
    private Collider2D playerCollider;
    private Collider2D currentEnemy; // 현재 타겟
    private float lastDetectionTime = 0f; // 마지막 감지 시간
    private float detectionInterval = 1f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<Player>();
        playerCollider = animator.GetComponent<Collider2D>();
        lastDetectionTime = Time.time;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 현재 시간이 마지막 감지 시간보다 1초 이상 경과한 경우 감지 수행
        if (Time.time >= lastDetectionTime + detectionInterval)
        {
            lastDetectionTime = Time.time; // 감지 후 시간 갱신

            // 플레이어의 중앙을 계산
            Vector2 playerCenter = playerCollider.bounds.center;

            // 중앙을 기준으로 적 감지
            currentEnemy = Physics2D.OverlapCircle(playerCenter, attackRadius, enemyLayer);

            // 적이 감지되면 공격
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
