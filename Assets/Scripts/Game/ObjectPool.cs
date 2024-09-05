using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;  // Ǯ �±�
        public GameObject prefab;  // ������Ʈ ������
        public int size;  // �ʱ� Ǯ ũ��
    }

    public List<Pool> pools;  // ���� Ǯ ���
    private Dictionary<string, ObjectPool<GameObject>> poolDic;

    private void Awake()
    {
        poolDic = new Dictionary<string, ObjectPool<GameObject>>();

        // �� Pool�� ���� ������Ʈ Ǯ �ʱ�ȭ
        foreach (var pool in pools)
        {
            ObjectPool<GameObject> newPool = new ObjectPool<GameObject>(
                createFunc: () => {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    return obj;
                },
                actionOnGet: (obj) => obj.SetActive(true),  // Ǯ���� ������Ʈ�� ������ Ȱ��ȭ
                actionOnRelease: (obj) => obj.SetActive(false),  // Ǯ�� ��ȯ�� �� ��Ȱ��ȭ
                actionOnDestroy: (obj) => Destroy(obj),  // �ʿ� ���� �� ������Ʈ �ı�
                collectionCheck: false,  // ���� ������Ʈ �ߺ� ���� üũ
                defaultCapacity: pool.size,  // �ʱ� Ǯ ũ��
                maxSize: pool.size  // �ִ� Ǯ ũ��
            );

            poolDic.Add(pool.tag, newPool);
        }
    }

    // ������Ʈ Ǯ���� �±׸� ������� ������Ʈ�� �������� �޼���
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogWarning($"Ǯ�� �±� '{tag}'�� �������� �ʽ��ϴ�.");
            return null;
        }

        // ������Ʈ Ǯ���� ������Ʈ�� ������
        GameObject objectToSpawn = poolDic[tag].Get();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    // ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogWarning($"Ǯ�� �±� '{tag}'�� �������� �ʽ��ϴ�.");
            return;
        }

        poolDic[tag].Release(obj);  // ������Ʈ�� Ǯ�� ��ȯ
    }
}
