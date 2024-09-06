using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CSVReader CSVReader;
    public ObjectPool ObjectPool;
    public Spawner Spawner;
    public UIManager UIManager;

    private void Awake()
    {
        Instance = this;
    }
}