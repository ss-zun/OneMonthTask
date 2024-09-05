using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public List<EnemyData> enemyList = new List<EnemyData>();  // �� �����͸� ������ ����Ʈ

    void Awake()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("Data/SampleMonster");
        string[] data = csvFile.text.Split(new char[] { '\n' });

        // ù ��° ���� ����̹Ƿ�, �״��� �ٺ��� �Ľ�
        for (int i = 1; i < data.Length; i++)
        {
            if (string.IsNullOrEmpty(data[i])) continue; // �� �� �ǳʶ�
            string[] row = data[i].Split(',');

            EnemyData enemy = new EnemyData
            {
                Name = row[0],
                Grade = row[1],
                Speed = float.Parse(row[2]),
                Health = int.Parse(row[3])
            };

            // ����Ʈ�� �� �����͸� �߰�
            enemyList.Add(enemy);
        }

        // �Ľ̵� ������ ��� (�׽�Ʈ��)
        foreach (EnemyData enemy in enemyList)
        {
            Debug.Log($"{enemy.Name} ({enemy.Grade}) - Speed: {enemy.Speed}, Health: {enemy.Health}");
        }
    }
}
