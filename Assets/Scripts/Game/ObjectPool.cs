using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;  // 풀 태그
        public GameObject prefab;  // 오브젝트 프리팹
        public int size;  // 초기 풀 크기
    }

    public List<Pool> pools;  // 여러 풀 목록
    private Dictionary<string, ObjectPool<GameObject>> poolDic;

    private void Awake()
    {
        poolDic = new Dictionary<string, ObjectPool<GameObject>>();

        // 각 Pool에 대해 오브젝트 풀 초기화
        foreach (var pool in pools)
        {
            ObjectPool<GameObject> newPool = new ObjectPool<GameObject>(
                createFunc: () => {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    return obj;
                },
                actionOnGet: (obj) => obj.SetActive(true),  // 풀에서 오브젝트를 꺼내면 활성화
                actionOnRelease: (obj) => obj.SetActive(false),  // 풀로 반환될 때 비활성화
                actionOnDestroy: (obj) => Destroy(obj),  // 필요 없을 때 오브젝트 파괴
                collectionCheck: false,  // 동일 오브젝트 중복 방지 체크
                defaultCapacity: pool.size,  // 초기 풀 크기
                maxSize: pool.size  // 최대 풀 크기
            );

            poolDic.Add(pool.tag, newPool);
        }
    }

    // 오브젝트 풀에서 태그를 기반으로 오브젝트를 가져오는 메서드
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogWarning($"풀에 태그 '{tag}'가 존재하지 않습니다.");
            return null;
        }

        // 오브젝트 풀에서 오브젝트를 가져옴
        GameObject objectToSpawn = poolDic[tag].Get();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    // 오브젝트를 풀로 반환하는 메서드
    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogWarning($"풀에 태그 '{tag}'가 존재하지 않습니다.");
            return;
        }

        poolDic[tag].Release(obj);  // 오브젝트를 풀로 반환
    }
}
