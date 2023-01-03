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
    #region PrivateParams
    [Header("Text References")]
    [SerializeField] TMP_Text _levelText;
    [SerializeField] TMP_Text _savedBabiesText;
    [SerializeField] TMP_Text _TargetBabiesText;
    [SerializeField] TMP_Text _gameMsgText;
    [Header("Buttons")]
    [SerializeField] Text _continueButtonText;

    [Header("Player Stats")]
    [SerializeField] LivesUIManager _livesUI;

    [Header("Manus")]
    [SerializeField] GameObject _EndLevelManu;

    [Header("Collectors References")]
    [SerializeField] Ambulance _ambulance;
    [SerializeField] Floor _floor;

    [Header("Parameters")]
    [SerializeField] float _msgAnimDuration;

    int _savedBabies = 0;
    #endregion

    #region UnityLifeCycleFuncs
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
        SetLevelText(GameData.CurrentLevel + 1); // zero based levels
        AnimateGameMsg($"Level {GameData.CurrentLevel + 1}");
    }
    #endregion

    #region GameEventsHandlers
    private void OnReduceLives()
    {
        _livesUI.ReduceLives(1);
    }

    private void OnCollect()
    {
        _savedBabies++;
        UpdateSavedBabiesText();
    }

    private void OnLevelEnd(LevelResult result)
    {
        ShowGameMsg(result);
        _EndLevelManu.SetActive(true);
        _continueButtonText.text = result == LevelResult.Win ? "Next Level" : "Retry";

    }
    #endregion

    #region TextAndMsgHandling
    private void SetLevelText(int level)
    {
        _levelText.text = level.ToString();
    }

    void UpdateSavedBabiesText()
    {
        _savedBabiesText.text = _savedBabies.ToString();
    }
    void UpdateTargetBabiesText()
    {
        _TargetBabiesText.text = LevelManager.GetLevel().targetSaves.ToString();
    }

    private async void ShowGameMsg(LevelResult result)
    {
        if (result == LevelResult.Win)
        {
            await AnimateGameMsg("You Win");
        }
        else
        {
            await AnimateGameMsg("You Lose");
        }
    }

    private async Task AnimateGameMsg(string msg)
    {
        _gameMsgText.text = msg;
        _gameMsgText.gameObject.SetActive(true);
        await Task.Delay((int)_msgAnimDuration * 1000);
        _gameMsgText.gameObject.SetActive(false);

    }
    #endregion

    #region ButtonsCallbacks
    public void OnContinue()
    {
        SceneManager.LoadScene((int)Scenes.GameScene);
    }

    public void OnBackToMainManu()
    {
        SceneManager.LoadScene((int)Scenes.MainManuScene);
    }
    #endregion

}
