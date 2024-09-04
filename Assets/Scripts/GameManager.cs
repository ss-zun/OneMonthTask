using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player Player;
    public ObjectPool ObjectPool;

    private void Awake()
    {
        Instance = this;
    }
}