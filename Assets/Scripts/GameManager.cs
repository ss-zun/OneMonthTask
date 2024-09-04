using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public ObjectPool ObjectPool;

    private void Awake()
    {
        Instance = this;
    }
}