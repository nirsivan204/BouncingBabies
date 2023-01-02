using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManuManager : MonoBehaviour
{
    [SerializeField] GameObject _settingsManu;
    [SerializeField] GameObject _mainManu;
    [SerializeField] GameObject _statsManu;


    private void Start()
    {
        AudioManager.Instance.PlaySound(SoundType.BG_Music,true);
    }

    public void OnSettingsPressed()
    {
        AudioManager.Instance.PlaySound(SoundType.Click);
        _settingsManu.SetActive(true);
        _mainManu.SetActive(false);
    }

    public void OnBackPressed()
    {
        AudioManager.Instance.PlaySound(SoundType.Click);
        _settingsManu.SetActive(false);
        _mainManu.SetActive(true);
        _statsManu.SetActive(false);

    }

    public void OnStatsPressed()
    {
        AudioManager.Instance.PlaySound(SoundType.Click);
        _statsManu.SetActive(true);
        _mainManu.SetActive(false);

    }

    public void OnContinuePressed()
    {
        AudioManager.Instance.PlaySound(SoundType.Click);
        GameData.CurrentLevel = GameData.MaxLevelRecord;
        StartGame();

    }

    public void OnStartPressed()
    {
        AudioManager.Instance.PlaySound(SoundType.Click);
        GameData.CurrentLevel = 0;
        StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene((int)Scenes.GameScene);

    }
}
