using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CSVReader CSVReader;
    public ObjectPool ObjectPool;
    public Spawner Spawner;

    public GameObject HpBar;
    public Image HpFillBar;

    private void Awake()
    {
        Instance = this;
    }
}