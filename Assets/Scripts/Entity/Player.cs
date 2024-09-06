using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform firePoint;
    public Animator anim;
    public AnimationData animData = new AnimationData();
    public float attackRadius = 14f; // ���� ����
    public LayerMask enemyLayer; // ���� ���� ���̾�
    public Collider2D playerCollider;

    private WaitForSeconds attackInterval = new WaitForSeconds(1f);

    private void Start()
    {
        animData.Init();
        StartCoroutine(DetectionCoroutine());
    }

    // 1�ʸ��� ���� �����ϴ� �ڷ�ƾ
    private IEnumerator DetectionCoroutine()
    {
        while (true)
        {
            Collider2D currentEnemy = Physics2D.OverlapCircle(transform.position, attackRadius, enemyLayer);

            // ���� �����Ǹ� ���� �ִϸ��̼� Ʈ����
            if (currentEnemy != null)
            {
                anim.SetTrigger(animData.AttackParameterHash); // ���� �ִϸ��̼� Ʈ����
            }

            // 1�� ��� �� �ٽ� ����
            yield return attackInterval;
        }
    }

    public void FireArrow()
    {
        GameManager.Instance.ObjectPool.SpawnFromPool("Arrow", firePoint.position, firePoint.rotation);
    }

    // ���� ������ �ð������� ǥ��
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (playerCollider != null)
        {
            Vector2 playerCenter = playerCollider.bounds.center;
            Gizmos.DrawWireSphere(playerCenter, attackRadius); // ���� ������ �÷��̾� �߾� �������� ǥ��
        }
    }
#endif
}
