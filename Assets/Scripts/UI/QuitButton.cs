using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public Button button;
    private void Awake()
    {
        button.onClick.AddListener(QuitGame);
    }
    
    private void QuitGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        PlayerPrefs.DeleteAll();
#else
        Application.Quit();
#endif

    }

}
