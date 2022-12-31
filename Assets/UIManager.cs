using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] LivesUIManager _livesUI;
    [SerializeField] TMP_Text _levelText;
    [SerializeField] TMP_Text _savedBabiesText;

    [SerializeField] Ambulance _ambulance;
    [SerializeField] Floor _floor;
    int _savedBabies = 0;

    private void OnEnable()
    {
        _ambulance.BabyCollectedEvent += OnCollect;
        _floor.BabyCollectedEvent += OnReduceLives;
    }

    private void OnDisable()
    {

        _ambulance.BabyCollectedEvent -= OnCollect;
        _floor.BabyCollectedEvent -= OnReduceLives;

    }

    public void Init()
    {
        _savedBabies = 0;
        UpdateSavedBabiesText();
        SetLevelText(GameData.CurrentLevel);
    }


    private void SetLevelText(int level)
    {
        _levelText.text = level.ToString();
    }

    private void OnReduceLives()
    {
        _livesUI.ReduceLives(1);
    }

    private void OnCollect()
    {
        _savedBabies += 1;
        UpdateSavedBabiesText();
    }

    void UpdateSavedBabiesText()
    {
        _savedBabiesText.text = _savedBabies.ToString();
    }


}
