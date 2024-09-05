using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;  // Ǯ�� �±�
        public GameObject prefab;  // ������Ʈ ������
        public int size;  // �ʱ� Ǯ ũ��
    }

    public List<Pool> pools;  // ���� Ǯ�� ���
    private Dictionary<string, ObjectPool<GameObject>> poolDic;  // Ǯ�� ������ ��ųʸ�

    private void Awake()
    {
        poolDic = new Dictionary<string, ObjectPool<GameObject>>();

        // �� Pool�� ���� ������Ʈ Ǯ�� �ʱ�ȭ
        foreach (var pool in pools)
        {
            var newPool = new ObjectPool<GameObject>(
                createFunc: () => {
                    GameObject obj = Instantiate(pool.prefab);  // �������� �ν��Ͻ�ȭ
                    obj.SetActive(false);  // �ʱ⿡�� ��Ȱ��ȭ
                    return obj;
                },
                actionOnGet: (obj) => {
                    obj.SetActive(true);  // Ǯ���� ������Ʈ�� ������ Ȱ��ȭ
                },
                actionOnRelease: (obj) => {
                    obj.SetActive(false);  // Ǯ�� ��ȯ�� �� ��Ȱ��ȭ
                },
                actionOnDestroy: (obj) => Destroy(obj),  // �ʿ� ���� �� ������Ʈ �ı�
                collectionCheck: false,  // ���� ������Ʈ �ߺ� ���� üũ ��Ȱ��ȭ
                defaultCapacity: pool.size,  // �ʱ� Ǯ ũ�� ����
                maxSize: pool.size  // �ִ� Ǯ ũ�� ����
            );

            poolDic.Add(pool.tag, newPool);  // ��ųʸ��� Ǯ �߰�
        }
    }

    // Ǯ���� GameObject�� �����ͼ� ��ġ�� ȸ������ ����
    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogWarning($"�±� '{tag}'�� �ش��ϴ� Ǯ�� �������� �ʽ��ϴ�.");
            return null;
        }

        // Ǯ���� ��ü�� ������
        GameObject objectToSpawn = poolDic[tag].Get();

        // ��ġ�� ȸ���� ����
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    // GameObject�� Ǯ�� ��ȯ
    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDic.ContainsKey(tag))
        {
            Debug.LogWarning($"�±� '{tag}'�� �ش��ϴ� Ǯ�� �������� �ʽ��ϴ�.");
            return;
        }

        poolDic[tag].Release(obj);  // ������Ʈ�� Ǯ�� ��ȯ
    }
}
