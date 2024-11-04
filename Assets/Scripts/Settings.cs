using UnityEngine;
using UnityEngine.UI;
using static GameData;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sound;
    [SerializeField] private Toggle _mute;
    private Sounds _soundManager =>Sounds.chooseSound;
   // private float musicVolume { get => GData.settings.musicVolume; set => GData.settings.musicVolume = value; } 
    //private float soundVolume { get => GData.settings.soundVolume; set => GData.settings.soundVolume = value; } 
   // private bool mute { get => GData.settings.mute; set => GData.settings.mute = value; } 
    public void FollowToURL(string url)
    {
        Application.OpenURL(url);
    }

    //private void Awake()
    //{
    //    _music.value = musicVolume;
    //    _sound.value = soundVolume;
    //    _mute.isOn = mute;
    //    _music.onValueChanged.AddListener((float v)=>ChangeMusicVolume());
    //    _sound.onValueChanged.AddListener((float v)=>ChangeSoundVolume());
    //    _mute.onValueChanged.AddListener((bool v) => Mute());
    //}
    //public void ChangeSoundVolume()
    //{
    //    _soundManager.SetSoundsVolume(_sound.value);
    //    _mute.isOn = false;
    //    soundVolume = _sound.value;
    //}
    //public void ChangeMusicVolume()
    //{
    //    _soundManager.SetMusicVolume(_music.value);
    //    _mute.isOn = false;
    //    musicVolume = _music.value;
    //}
    //public void Mute()
    //{
    //    _soundManager.Mute(_mute.isOn);
    //    mute = _mute.isOn;
    //}
    public void LoadScene(int index) => SceneManager.LoadScene(index);
    public void ExitGame() => Application.Quit();
    
}
