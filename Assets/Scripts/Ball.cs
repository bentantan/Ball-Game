using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour, IPointerClickHandler
{
    public BallColor BallColour;
    public static UnityEvent<Ball> OnRally;
    public UnityEvent OnBallGathered;

    private static float _duration = 0.5f;

    [SerializeField] private Rigidbody _playerRb;
    private bool _isClickable;
    private Tween _moveTween;

    static public UnityEvent<bool> OnBallShout;

    private void Awake()
    {
        if (OnRally == null) OnRally = new UnityEvent<Ball>();
        OnBallGathered = new UnityEvent();
        OnRally.AddListener(MoveToBall);
        OnBallGathered.AddListener(EliminateBall);
        GameManager.instance.OnEndGame.AddListener(HandleEndGame);
        GameManager.instance.Balls.Add(this);
        _isClickable = true;
    }

    private void HandleEndGame(bool isCompleted)
    {
        gameObject.transform.DOKill();
        
        //if enabled makes the balls faster everytime the player fails on the level
        //if (isCompleted) _duration = 0.5f;
        //else _duration *= 0.99f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnRally.Invoke(this);
    }

    public void MoveToBall(Ball targetBall)
    {
        if (targetBall.BallColour == BallColour && _isClickable)
        {
            _isClickable = false;

            if (targetBall == this) _playerRb.constraints = RigidbodyConstraints.FreezePosition;

            else _moveTween = gameObject.transform.DOMove(targetBall.transform.position, _duration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        OnBallGathered.Invoke();
                        targetBall.OnBallGathered?.Invoke();
                    });
        }
    }

    void EliminateBall()
    {

        gameObject.transform.DOScale(0, 1.0f).OnComplete(() =>
        {
            gameObject.transform.DOKill();
            GameManager.instance.RemoveBall(this);
        });
    }
}
public enum BallColor
{
    White, Black, Purple, Green, Blue
}

