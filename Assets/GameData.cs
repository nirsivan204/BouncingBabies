using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int MaxLevelRecord {
        get
        {
           return PlayerPrefs.GetInt("MaxLevelRecord");
        }
        set 
        {
            PlayerPrefs.SetInt("MaxLevelRecord",value);
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
