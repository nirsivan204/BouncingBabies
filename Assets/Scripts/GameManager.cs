using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enums
public enum LevelResult
{
    Win,
    Lose,
}
#endregion

public class GameManager : MonoBehaviour
{
    #region StaticMembersAndConsts

    public static Action<LevelResult> LevelEndEvent;
    
    public const int MAX_LIVES = 3;

    #endregion

    #region PrivateParams

    [SerializeField] BabiesSpawner _spawner;
    [SerializeField] Ambulance _ambulance;
    [SerializeField] Floor _floor;

    private int _lives;
    private int _targetSaves;
    private int _babiesSaved;
    private bool _isGameEnded = false;

    #endregion

    #region UnityLifeCycleFuncs
    private void Awake()
    {
        LevelManager.InitLevel();
    }

    public void Start()
    {
        StartLevel();
    }

    private void OnEnable()
    {
        _ambulance.BabyCollectedEvent += OnBabySaved;
        _floor.BabyCollectedEvent += OnBabyLost;
    }
    private void OnDisable()
    {
        _ambulance.BabyCollectedEvent -= OnBabySaved;
        _floor.BabyCollectedEvent -= OnBabyLost;
    }

    #endregion

    #region LevelHandling
    public void StartLevel()
    {
        Level level = LevelManager.GetLevel();
        _lives = MAX_LIVES;
        _targetSaves = level.targetSaves;
        _babiesSaved = 0;
        AudioManager.Instance.PlaySound(SoundType.LevelStart);
        _spawner.Init(level.numOfWindows, level.minBabiesPerSecond, level.maxBabiesPerSecond);
        _spawner.StartSpawn();
    }

    private void EndLevel()
    {
        _spawner.StopSpawn();
        _isGameEnded = true;
        GameData.TotalBabiesLost += MAX_LIVES - _lives;
        GameData.TotalBabiesSaved += _babiesSaved;
    }
    #endregion

    #region CollectorsEventsHandlers

    private void OnBabySaved()
    {
        if (_isGameEnded)
        {
            return;
        }
        _babiesSaved++;
        if (_babiesSaved == _targetSaves)
        {
            LevelAccomplished();
        }
    }

    private void OnBabyLost()
    {
        if (_isGameEnded)
        {
            return;
        }
        _lives--;
        if (_lives < 0)
        {
            GameOver();
        }
    }

    #endregion

    #region WinLoseHandlers
    private void GameOver()
    {
        AudioManager.Instance.PlaySound(SoundType.Lose);
        LevelEndEvent(LevelResult.Lose);
        EndLevel();
    }

    private void LevelAccomplished()
    {
        AudioManager.Instance.PlaySound(SoundType.Win);
        LevelEndEvent(LevelResult.Win);
        UpdateLevelRecord();
        EndLevel();
    }

    private void UpdateLevelRecord()
    {
        GameData.CurrentLevel++;
        if (GameData.MaxLevelRecord < GameData.CurrentLevel)
        {
            GameData.MaxLevelRecord = GameData.CurrentLevel;
        }
    }
    #endregion




}
