using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public int score = 0;
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        score = 0;
        ScoreUI();
    }

    public void IncrementScore(int value)
    {
        score += value;
        ScoreUI();
    }

    public void ScoreUI()
    {
        textMeshPro.text = "Score: " + score;
    }
}
