using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public enum LevelResult
{
    Win,
    Lose,
}

public class GameManager : MonoBehaviour
{
    public static Action<LevelResult> LevelEndEvent;

    private const int MAX_LIVES = 3;
    public static GameManager Instance { get; private set; }
    public Floor Floor { get => _floor; set => _floor = value; }
    public Ambulance Ambulance { get => _ambulance; set => _ambulance = value; }

    [SerializeField] BabiesSpawner _spawner;
    [SerializeField] Ambulance _ambulance;
    [SerializeField] Floor _floor;

    private int _lives;
    private int _targetSaves;
    private int _babiesSaved;
    private bool _isGameEnded = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            LevelManager.InitLevel();
        }
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

    private void GameOver()
    {
        AudioManager.Instance.PlaySound(SoundType.Lose);
        LevelEndEvent(LevelResult.Lose);
        EndLevel();
    }

    private void OnBabySaved()
    {
        if (_isGameEnded)
        {
            return;
        }
        _babiesSaved++;
        if(_babiesSaved == _targetSaves)
        {
            LevelAccomplished();
        }
    }

    private void LevelAccomplished()
    {
        AudioManager.Instance.PlaySound(SoundType.Win);
        LevelEndEvent(LevelResult.Win);
        UpdateLevelRecord();
        EndLevel();
    }

    private void OnDisable()
    {
        _ambulance.BabyCollectedEvent -= OnBabySaved;
        _floor.BabyCollectedEvent -= OnBabyLost;
    }
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

    public void UpdateLevelRecord()
    {
        GameData.CurrentLevel++;
        if (GameData.MaxLevelRecord < GameData.CurrentLevel)
        {
            GameData.MaxLevelRecord = GameData.CurrentLevel;
        }
    }

    
}
