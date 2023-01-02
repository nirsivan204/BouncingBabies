using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUIManager : MonoBehaviour
{
    [SerializeField] Image[] Lives;
    int livesLeft = GameManager.MAX_LIVES;

    private void OnValidate()
    {
        if(Lives.Length != GameManager.MAX_LIVES)
        {
            throw new Exception("Max lives defined in game manager not equal to number of lives images");
        }
    }

    public void ReduceLives(int amount)
    {
        for(int i = 0; i<amount; i++)
        {
            if (livesLeft > 0)
            {
                RemoveLiveIcon();
                livesLeft--;
            }
        }
    }

    private void RemoveLiveIcon()
    {
        Image icon = Lives[livesLeft - 1];
        icon.CrossFadeColor(Color.black, 1, true, false);
    }
}
