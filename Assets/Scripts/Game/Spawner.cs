using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    public Vector2 spawnPoint;  // 적이 생성될 위치
    public Vector2 endPoint;  // 적이 이동할 목표 위치
    public float gizmoRadius = 0.5f;  // 기즈모의 반지름

    private int currentEnemyIndex = 0;  // 현재 적 인덱스
    private string animatorPath = "Animations/Animators/";

    private void Start()
    {
        SpawnNextEnemy();  // 게임 시작 시 적을 생성
    }

    public void SpawnNextEnemy()
    {
        // 적 리스트에서 적 데이터를 가져올 때 인덱스를 순환시킴
        if (GameManager.Instance.CSVReader.enemyList.Count == 0)
        {
            Debug.LogWarning("적 리스트가 비어있습니다.");
            return;
        }

        currentEnemyIndex = currentEnemyIndex % GameManager.Instance.CSVReader.enemyList.Count;

        EnemyData enemyData = GameManager.Instance.CSVReader.enemyList[currentEnemyIndex++];

        // 오브젝트 풀에서 적을 가져옴
        GameObject enemyObject = GameManager.Instance.ObjectPool.SpawnFromPool("Enemy", spawnPoint, Quaternion.identity);

        Enemy enemy = enemyObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animatorPath + enemyData.Name);
            enemy.Init(enemyData, SpawnNextEnemy);  // 적 데이터와 적이 죽을 때 실행할 콜백 전달
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // spawnPoint에 빨간색 기즈모 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnPoint, gizmoRadius);

        // endPoint에 파란색 기즈모 그리기
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(endPoint, gizmoRadius);

        // 두 점을 연결하는 선 그리기
        Gizmos.color = Color.green;
        Gizmos.DrawLine(spawnPoint, endPoint);
    }
#endif
}
