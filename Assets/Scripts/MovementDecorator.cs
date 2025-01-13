using UnityEngine;
using DG.Tweening;

public class MovementDecorator : MonoBehaviour
{
    [SerializeField] bool _onOff = false;

    private Vector3 _startPosition;
    [SerializeField] private Ball _ball;

    [SerializeField] Movement _movementType;

    [SerializeField] private float _duration = 2f;

    [Header("BackAndForth")]
    [SerializeField] private float _horizontalDistance;
    [SerializeField] private float _verticalDistance;

    [Header("Circular and Arc")]
    [SerializeField] private float _radius = 5f;
    [SerializeField] private GameObject _center;
    [SerializeField] private bool _clockwise = true;
    [Header("-Only for Arc Movement: [multiply the wanted angle by 2 / _totalAngleRadiants]")]
    [SerializeField] private float _startAngleDegrees = 0f;
    [Header("-Only for Arc Movement: (should be 2 at all times for circular movement)")]
    [SerializeField] private float _totalAngleRadiants = 2f;

    private Tween _moveTween;

    private void Awake()
    {
        if (!_onOff) return;

        _startPosition = transform.position;
        StartMoving();
        GameManager.instance.OnEndGame.AddListener(PauseMovement);
        if (_ball != null)
        {
            Ball.OnRally.AddListener(PauseMovementOnGathering);
        }
    }

    private void PauseMovement(bool awd)
    {
        _moveTween.Kill();
    }
    private void PauseMovementOnGathering(Ball ball)
    {
        if (ball.BallColour == _ball.BallColour)
        {
            _moveTween.Kill();
        }
    }

    private void StartMoving()
    {
        switch (_movementType)
        {
            case Movement.BackAndForth:
                MoveBackAndForth(); break;
            default:
                MoveCircular(); break;
        }
    }

    private void MoveBackAndForth()
    {
        Vector3 endPosition;
        endPosition = new Vector3(_startPosition.x + _horizontalDistance, _startPosition.y + _verticalDistance, _startPosition.z);
        _moveTween = transform.DOMove(endPosition, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void MoveCircular()
    {
        float startAngleRadians = _startAngleDegrees * Mathf.Deg2Rad;

        _moveTween = DOTween.To(() => 0f, (x) =>
        {
            float direction = _clockwise ? 1f : -1f;
            float angle = (x * direction + _startAngleDegrees / 360f) * Mathf.PI * _totalAngleRadiants;
            Vector3 newPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * _radius;
            transform.position = _center.transform.position + newPos;
        }, 1f, _duration)
        .SetEase(Ease.Linear);

        if (_movementType == Movement.Arc) _moveTween.SetLoops(-1, LoopType.Yoyo);
        else _moveTween.SetLoops(-1, LoopType.Restart);
    }
}

public enum Movement
{
    BackAndForth, Circular, Arc
}
