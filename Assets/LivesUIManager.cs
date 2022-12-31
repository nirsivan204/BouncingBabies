using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUIManager : MonoBehaviour
{
    [SerializeField] Image[] Lives;
    int livesLeft = 3;
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

    public void ResetLives()
    {
        livesLeft = 3;
    }

    private void RemoveLiveIcon()
    {
        Image icon = Lives[livesLeft - 1];
        icon.CrossFadeColor(Color.black, 1, true, false);
    }
}
