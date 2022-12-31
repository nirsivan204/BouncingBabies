using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManuManager : MonoBehaviour
{
    [SerializeField] GameObject _settingsManu;
    [SerializeField] GameObject _mainManu;
    [SerializeField] GameObject _statsManu;


    public void OnSettingsPressed()
    {
        _settingsManu.SetActive(true);
        _mainManu.SetActive(false);
    }

    public void OnBackPressed()
    {
        _settingsManu.SetActive(false);
        _mainManu.SetActive(true);
        _statsManu.SetActive(false);

    }

    public void OnStatsPressed()
    {
        _statsManu.SetActive(true);
        _mainManu.SetActive(false);

    }

    public void OnContinuePressed()
    {
        GameData.CurrentLevel = GameData.MaxLevelRecord;
        StartGame();

    }

    public void OnStartPressed()
    {
        GameData.CurrentLevel = 0;
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }

}
