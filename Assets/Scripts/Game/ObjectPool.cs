using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;  // 풀의 태그
        public GameObject prefab;  // 오브젝트 프리팹
        public int size;  // 초기 풀 크기
    }

    public List<Pool> pools;  // 여러 풀의 목록
    private Dictionary<string, ObjectPool<GameObject>> poolDic;  // 풀을 관리할 딕셔너리

    private void Awake()
    {
        poolDic = new Dictionary<string, ObjectPool<GameObject>>();

        // 각 Pool에 대해 오브젝트 풀을 초기화
        foreach (var pool in pools)
        {
            var newPool = new ObjectPool<GameObject>(
                createFunc: () => {
                    GameObject obj = Instantiate(pool.prefab);  // 프리팹을 인스턴스화
                    obj.SetActive(false);  // 초기에는 비활성화
                    return obj;
                },
                actionOnGet: (obj) => {
                    obj.SetActive(true);  // 풀에서 오브젝트를 꺼내면 활성화
                },
                actionOnRelease: (obj) => {
                    obj.SetActive(false);  // 풀로 반환될 때 비활성화
                },
                actionOnDestroy: (obj) => Destroy(obj),  // 필요 없을 때 오브젝트 파괴
                collectionCheck: false,  // 동일 오브젝트 중복 방지 체크 비활성화
                defaultCapacity: pool.size,  // 초기 풀 크기 설정
                maxSize: pool.size  // 최대 풀 크기 설정
            );

            poolDic.Add(pool.tag, newPool);  // 딕셔너리에 풀 추가
        }
    }

    // 풀에서 GameObject를 가져와서 위치와 회전값을 설정
    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogWarning($"태그 '{tag}'에 해당하는 풀이 존재하지 않습니다.");
            return null;
        }

        // 풀에서 객체를 가져옴
        GameObject objectToSpawn = poolDic[tag].Get();

        // 위치와 회전값 설정
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    // GameObject를 풀로 반환
    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogWarning($"태그 '{tag}'에 해당하는 풀이 존재하지 않습니다.");
            return;
        }

        poolDic[tag].Release(obj);  // 오브젝트를 풀로 반환
    }
}
