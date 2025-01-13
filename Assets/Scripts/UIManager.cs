using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI _scoreUI;
     private int _baseScore = 0;
     private int _levelScore = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameManager.instance.OnEndGame.AddListener(HandleEndGame);
    }

    public void UpdateScore()
    {
        _scoreUI.text = "" + (_baseScore + ++_levelScore);
    }

    private void HandleEndGame(bool _levelCompleted)
    {
        if (_levelCompleted) WinGame();
        else FailGame();
    }

    private void FailGame()
    {
        _scoreUI.text = "" + _baseScore;
        _levelScore = 0;
    }

    private void WinGame()
    {
        _baseScore += _levelScore;
        _levelScore = 0;
        _scoreUI.text = "" + _baseScore;
    }
}
