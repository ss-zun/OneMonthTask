using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public List<EnemyData> enemyList = new List<EnemyData>();  // 적 데이터를 저장할 리스트

    void Awake()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("Data/SampleMonster");
        string[] data = csvFile.text.Split(new char[] { '\n' });

        // 첫 번째 줄은 헤더이므로, 그다음 줄부터 파싱
        for (int i = 1; i < data.Length; i++)
        {
            if (string.IsNullOrEmpty(data[i])) continue; // 빈 줄 건너뜀
            string[] row = data[i].Split(',');

            EnemyData enemy = new EnemyData
            {
                Name = row[0],
                Grade = row[1],
                Speed = float.Parse(row[2]),
                Health = int.Parse(row[3])
            };

            // 리스트에 적 데이터를 추가
            enemyList.Add(enemy);
        }

        // 파싱된 데이터 출력 (테스트용)
        foreach (EnemyData enemy in enemyList)
        {
            Debug.Log($"{enemy.Name} ({enemy.Grade}) - Speed: {enemy.Speed}, Health: {enemy.Health}");
        }
    }
}
