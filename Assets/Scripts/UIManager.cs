using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] LivesUIManager _livesUI;
    [SerializeField] TMP_Text _levelText;
    [SerializeField] TMP_Text _savedBabiesText;
    [SerializeField] TMP_Text _TargetBabiesText;
    [SerializeField] TMP_Text _gameMsgText;
    [SerializeField] Text _continueButtonText;
    [SerializeField] float _msgAnimDuration;
    [SerializeField] GameObject _EndLevelManu;



    [SerializeField] Ambulance _ambulance;
    [SerializeField] Floor _floor;
    int _savedBabies = 0;

    private void OnEnable()
    {
        _ambulance.BabyCollectedEvent += OnCollect;
        _floor.BabyCollectedEvent += OnReduceLives;
        GameManager.LevelEndEvent += OnLevelEnd;
    }

    private void OnDisable()
    {

        _ambulance.BabyCollectedEvent -= OnCollect;
        _floor.BabyCollectedEvent -= OnReduceLives;
        GameManager.LevelEndEvent -= OnLevelEnd;
    }



    public void Start()
    {
        _savedBabies = 0;
        UpdateSavedBabiesText();
        UpdateTargetBabiesText();
        SetLevelText(GameData.CurrentLevel+1); // zero based levels
        AnimateGameMsg($"Level {GameData.CurrentLevel + 1}");
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
    void UpdateTargetBabiesText()
    {
        _TargetBabiesText.text = LevelManager.GetLevel().targetSaves.ToString();
    }

    private void ShowGameMsg(LevelResult result)
    {
        if (result == LevelResult.Win)
        {
            AnimateGameMsg("You Win");
        }
        else
        {
            AnimateGameMsg("You Lose");
        }
    }

    private void AnimateGameMsg(string msg)
    {
        _gameMsgText.text = msg;
        _gameMsgText.gameObject.SetActive(true);
    }

    private void OnLevelEnd(LevelResult result)
    {
        ShowGameMsg(result);
        _EndLevelManu.SetActive(true);
        _continueButtonText.text = result == LevelResult.Win ? "Next Level" : "Retry";

    }

    public void OnContinue()
    {
        SceneManager.LoadScene((int)Scenes.GameScene);
    }

    public void OnBackToMainManu()
    {
        SceneManager.LoadScene((int)Scenes.MainManuScene);
    }
}
