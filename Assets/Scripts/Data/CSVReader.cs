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

        // 첫 번째 줄은 헤더이므로, 그다음 줄부터 파싱
        for (int i = 1; i < data.Length; i++)
        {
            if (string.IsNullOrEmpty(data[i])) continue; // 빈 줄 건너뜀
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
                Debug.LogWarning($"딕셔너리에 존재하지 않는 Key: {unit.Name}");
            }
        }

        // 파싱된 데이터 출력 (테스트용)
        foreach (KeyValuePair<string, EnemyData> entry in unitDataDic)
        {
            Debug.Log($"{entry.Key}: {entry.Value.Name} ({entry.Value.Grade}) - Speed: {entry.Value.Speed}, Health: {entry.Value.Health}");
        }
    }
}
