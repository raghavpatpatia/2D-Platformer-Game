using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ContinueLevel : MonoBehaviour
{
    private Button button;
    private int sceneToContinue;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Load);
    }

    private void Load()
    {
        sceneToContinue = PlayerPrefs.GetInt("LoadLevel");

        if (sceneToContinue != 0)
        {
            SceneManager.LoadScene(sceneToContinue);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
