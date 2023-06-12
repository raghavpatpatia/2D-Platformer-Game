using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueLevel : MonoBehaviour
{
    private int continueLevel;
    public int GetLevel(int level = 1)
    {
        PlayerPrefs.SetInt("LoadLevel", level);
        PlayerPrefs.Save();
        return level;
    }

    public void LoadLastScene()
    {
        continueLevel = PlayerPrefs.GetInt("LoadLevel", GetLevel());
        SceneManager.LoadScene(continueLevel);
    }
}
