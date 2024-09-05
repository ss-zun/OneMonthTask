using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    public Vector2 spawnPoint;  // ���� ������ ��ġ
    public Vector2 endPoint;  // ���� �̵��� ��ǥ ��ġ
    public float gizmoRadius = 0.5f;  // ������� ������

    private int currentEnemyIndex = 0;  // ���� �� �ε���
    private string animatorPath = "Animations/Animators/";

    private void Start()
    {
        SpawnNextEnemy();  // ���� ���� �� ���� ����
    }

    public void SpawnNextEnemy()
    {
        // �� ����Ʈ���� �� �����͸� ������ �� �ε����� ��ȯ��Ŵ
        if (GameManager.Instance.CSVReader.enemyList.Count == 0)
        {
            Debug.LogWarning("�� ����Ʈ�� ����ֽ��ϴ�.");
            return;
        }

        currentEnemyIndex = currentEnemyIndex % GameManager.Instance.CSVReader.enemyList.Count;

        EnemyData enemyData = GameManager.Instance.CSVReader.enemyList[currentEnemyIndex++];

        // ������Ʈ Ǯ���� ���� ������
        GameObject enemyObject = GameManager.Instance.ObjectPool.SpawnFromPool("Enemy", spawnPoint, Quaternion.identity);

        Enemy enemy = enemyObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animatorPath + enemyData.Name);
            enemy.Init(enemyData, SpawnNextEnemy);  // �� �����Ϳ� ���� ���� �� ������ �ݹ� ����
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // spawnPoint�� ������ ����� �׸���
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnPoint, gizmoRadius);

        // endPoint�� �Ķ��� ����� �׸���
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(endPoint, gizmoRadius);

        // �� ���� �����ϴ� �� �׸���
        Gizmos.color = Color.green;
        Gizmos.DrawLine(spawnPoint, endPoint);
    }
#endif
}
