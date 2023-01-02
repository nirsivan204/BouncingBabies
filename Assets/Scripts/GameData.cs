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

    internal class StringsConsts
    {
        public static string PPMusicVolName { get; } = "MusicVol";
        public static string PPSFXVolName { get; } = "SFXVol";
        public static string PPTotalBabiesLost { get; } = "TotalBabiesLost";
        public static string PPTotalBabiesSaved { get; } = "TotalBabiesSaved";
        public static string PPMaxLevelRecord { get; } = "MaxLevelRecord";

    }


    public static int MaxLevelRecord 
    {
        get
        {
           return PlayerPrefs.GetInt(StringsConsts.PPMaxLevelRecord,0);
        }
        set 
        {
            PlayerPrefs.SetInt(StringsConsts.PPMaxLevelRecord, value);
        }
    }

    public static int TotalBabiesSaved
    {
        get
        {
            return PlayerPrefs.GetInt(StringsConsts.PPTotalBabiesSaved,0);
        }
        set
        {
            PlayerPrefs.SetInt(StringsConsts.PPTotalBabiesSaved, value);
        }
    }
    public static int TotalBabiesLost
    {
        get
        {
            return PlayerPrefs.GetInt(StringsConsts.PPTotalBabiesLost, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringsConsts.PPTotalBabiesLost, value);
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
