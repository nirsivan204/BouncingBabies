using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int MAX_LIVES = 3;
    public static GameManager Instance { get; private set; }
    public Floor Floor { get => _floor; set => _floor = value; }
    public Ambulance Ambulance { get => _ambulance; set => _ambulance = value; }

    [SerializeField] Spawner _spawner;
    [SerializeField] Ambulance _ambulance;
    [SerializeField] Floor _floor;

    private int _lives;
    private int _targetSaves;
    private int _babiesSaved;

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
        if (_lives > 0)
        {
            _lives--;
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        EndLevel();
        Debug.Log("gameOver");
    }

    private void OnBabySaved()
    {
        _babiesSaved++;
        if(_babiesSaved == _targetSaves)
        {
            LevelAccomplished();
        }
    }

    private void LevelAccomplished()
    {
        StartNextLevel();
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
        _spawner.Init(level.numOfWindows, level.minBabiesPerSecond, level.maxBabiesPerSecond);
        _spawner.StartSpawn();
    }

    private void EndLevel()
    {
        _spawner.StopSpawn();
    }
    public void OnStart()
    {

    }

    public void StartNextLevel()
    {
        GameData.CurrentLevel++;
        if (GameData.MaxLevelRecord < GameData.CurrentLevel)
        {
            GameData.MaxLevelRecord = GameData.CurrentLevel;
        }
        SceneManager.LoadScene(0);
    }
}
