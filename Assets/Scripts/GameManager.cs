using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject[] levelPrefabs;
    private GameObject _currentLevel;
    public UnityEvent<bool> OnEndGame; //Won or Failed
    private int _levelIndex = 0;
    public List<Ball> Balls;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        instance.OnEndGame.AddListener(LoadLevel);
        LoadLevel(false);
    }

    public void RemoveBall(Ball ball)
    {
        Balls.Remove(ball);
        UIManager.instance.UpdateScore();
        if (Balls.Count == 0) OnEndGame.Invoke(true);
    }

    private void LoadLevel(bool _levelCompleted)
    {
        if (_levelCompleted) instance._levelIndex++;
        Balls.Clear();
        Destroy(instance._currentLevel);
        instance._currentLevel = Instantiate(instance.levelPrefabs[instance._levelIndex]);
    }
}
