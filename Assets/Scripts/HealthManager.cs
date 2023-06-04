using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health = 3;
    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        Heart();
    }

    void Heart()
    {
        foreach (Image image in hearts)
        {
            image.sprite = emptyHeart;
        }

        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
}
