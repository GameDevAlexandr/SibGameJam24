using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData GData { get; private set;}
    private void Awake()
    {
        if (GData == null)
        {
            GData = this;
        }
        else if (GData == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public Settings settings;

    [Serializable]
    public struct SettingsData
    {
        public string language;
        public float soundVolume;
        public float musicVolume;
        public bool mute;
    }

}
