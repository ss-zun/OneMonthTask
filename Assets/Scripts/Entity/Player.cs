using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform firePoint;
    public Animator anim;
    public AnimationData animData = new AnimationData();
    public float attackRadius = 14f; // 감지 범위
    public LayerMask enemyLayer; // 적이 속한 레이어
    public Collider2D playerCollider;

    private WaitForSeconds attackInterval = new WaitForSeconds(1f);

    private void Start()
    {
        animData.Init();
        StartCoroutine(DetectionCoroutine());
    }

    // 1초마다 적을 감지하는 코루틴
    private IEnumerator DetectionCoroutine()
    {
        while (true)
        {
            Collider2D currentEnemy = Physics2D.OverlapCircle(transform.position, attackRadius, enemyLayer);

            // 적이 감지되면 공격 애니메이션 트리거
            if (currentEnemy != null)
            {
                anim.SetTrigger(animData.AttackParameterHash); // 공격 애니메이션 트리거
            }

            // 1초 대기 후 다시 감지
            yield return attackInterval;
        }
    }

    public void FireArrow()
    {
        GameManager.Instance.ObjectPool.SpawnFromPool("Arrow", firePoint.position, firePoint.rotation);
    }

    // 감지 범위를 시각적으로 표시
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (playerCollider != null)
        {
            Vector2 playerCenter = playerCollider.bounds.center;
            Gizmos.DrawWireSphere(playerCenter, attackRadius); // 감지 범위를 플레이어 중앙 기준으로 표시
        }
    }
#endif
}
