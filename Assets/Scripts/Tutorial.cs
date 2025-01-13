using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private bool _doOpenAllTutorials = true;

    private void Awake()
    {
        if (_doOpenAllTutorials)
        {
            PlayerPrefs.SetString(transform.parent.name, "NotDone");
        }

        if (PlayerPrefs.GetString(transform.parent.name) == "Done") gameObject.SetActive(false);
        else
        {
            GameManager.instance.OnEndGame.AddListener(HandleEndGame);
        }
    }

    private void HandleEndGame(bool isCompleted)
    {
        if (isCompleted) PlayerPrefs.SetString(transform.parent.name, "Done");
    }
}
