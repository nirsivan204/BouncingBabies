using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Scenes
{
    MainManuScene,
    GameScene,

}
public static class GameData
{
    public static int MaxLevelRecord 
    {
        get
        {
           return PlayerPrefs.GetInt("MaxLevelRecord",0);
        }
        set 
        {
            PlayerPrefs.SetInt("MaxLevelRecord",value);
        }
    }

    public static int TotalBabiesSaved
    {
        get
        {
            return PlayerPrefs.GetInt("TotalBabiesSaved",0);
        }
        set
        {
            PlayerPrefs.SetInt("TotalBabiesSaved", value);
        }
    }
    public static int TotalBabiesLost
    {
        get
        {
            return PlayerPrefs.GetInt("TotalBabiesLost",0);
        }
        set
        {
            PlayerPrefs.SetInt("TotalBabiesLost", value);
        }
    }

    public static int CurrentLevel { get; set; } = 0; // Zero Based

    public static void SavePlayerMusicPrefs(float musicVol, float sfxVol)
    {
        PlayerPrefs.SetFloat(StringsConsts.PPMusicVolName, musicVol);
        PlayerPrefs.SetFloat(StringsConsts.PPSFXVolName, sfxVol);
    }

    public static void LoadPlayerMusicPrefs(out float musicVol, out float sfxVol)
    {
        musicVol = PlayerPrefs.GetFloat(StringsConsts.PPMusicVolName, 1);
        sfxVol = PlayerPrefs.GetFloat(StringsConsts.PPSFXVolName, 1);
    }
}
