using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.Fail);

        gameObject.transform.DOKill();
        GameManager.instance.OnEndGame.Invoke(false);
    }
}
