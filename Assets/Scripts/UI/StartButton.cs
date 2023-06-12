using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button button;
    public GameObject LevelSelection;
    public GameObject GamePanel;

    private void Awake()
    {
        LevelSelection.SetActive(false);
        GamePanel.SetActive(true);
        button.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        GamePanel.SetActive(false);
        LevelSelection.SetActive(true);
    }
}
