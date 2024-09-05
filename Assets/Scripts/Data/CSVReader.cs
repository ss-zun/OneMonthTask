using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public Dictionary<string, EnemyData> unitDataDic = new Dictionary<string, EnemyData>();

    void Start()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("SampleMonster");
        string[] data = csvFile.text.Split(new char[] { '\n' });

        // ù ��° ���� ����̹Ƿ�, �״��� �ٺ��� �Ľ�
        for (int i = 1; i < data.Length; i++)
        {
            if (string.IsNullOrEmpty(data[i])) continue; // �� �� �ǳʶ�
            string[] row = data[i].Split(',');

            EnemyData unit = new EnemyData
            {
                Name = row[0],
                Grade = row[1],
                Speed = float.Parse(row[2]),
                Health = int.Parse(row[3])
            };

            if (!unitDataDic.ContainsKey(name))
            {
                unitDataDic.Add(unit.Name, unit);
            }
            else
            {
                Debug.LogWarning($"��ųʸ��� �������� �ʴ� Key: {unit.Name}");
            }
        }

        // �Ľ̵� ������ ��� (�׽�Ʈ��)
        foreach (KeyValuePair<string, EnemyData> entry in unitDataDic)
        {
            Debug.Log($"{entry.Key}: {entry.Value.Name} ({entry.Value.Grade}) - Speed: {entry.Value.Speed}, Health: {entry.Value.Health}");
        }
    }
}
