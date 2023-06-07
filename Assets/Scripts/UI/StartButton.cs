using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public Button button;
    [SerializeField] int sceneNumber;

    private void Awake()
    {
        button.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
