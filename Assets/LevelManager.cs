using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Level
{
    public readonly int targetSaves;
    public readonly int numOfWindows;
    public readonly float maxBabiesPerSecond;
    public readonly float minBabiesPerSecond;

    public Level(int level)
    {
        targetSaves = 5 + (level / 2) * 2; // every 2 levels, add 2.
        numOfWindows = level < 2 ? 1 : 2; // first 2 levels with 1 window, others with 2.
        maxBabiesPerSecond = 0.2f + level * 0.05f; //start with baby every 5 seconds.
        minBabiesPerSecond = 0.1f + level * 0.05f; // start with baby every 10 seconds.
    }
}

public static class LevelManager
{
    private static Level currentLevel;

    public static void InitLevel()
    {
        currentLevel = new Level(GameData.CurrentLevel);
    }
    public static Level GetLevel()
    {
        return currentLevel;
    }
}
