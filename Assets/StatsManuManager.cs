using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManuManager : MonoBehaviour
{
    [SerializeField] Text _babiesCollectedText;
    [SerializeField] Text _babiesLostText;
    [SerializeField] Text _highestLevelText;

    void OnEnable()
    {
        _babiesCollectedText.text = GameData.TotalBabiesSaved.ToString();
        _babiesLostText.text = GameData.TotalBabiesLost.ToString();
        _highestLevelText.text = (GameData.MaxLevelRecord+1).ToString();
    }
}
