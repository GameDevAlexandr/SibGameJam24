
using UnityEngine;
using UnityEngine.Audio;

public class Sounds : MonoBehaviour
{
    public static Sounds chooseSound { get; private set; }
    [SerializeField] private AudioMixerGroup mixer;
    public AudioSource backGroundSpace;
    public AudioSource startMenu;
    public AudioSource backGroundShip;
    public AudioSource move;
    public AudioSource selectCard;
    public AudioSource butonClick;
    public AudioSource endMove;
    public AudioSource pickResource;
    public AudioSource openInfo;


    private void Awake()
    {
        if (chooseSound == null)
        {
            chooseSound = this;
        }
        else if(chooseSound == this)
        {
            Destroy(gameObject);
        }           

        DontDestroyOnLoad(gameObject);        
    }
    public void RandomPitch(AudioSource pitchedAudio, float spread)
    {
        float pitch = Random.Range(-spread, spread);
        pitchedAudio.pitch = 1 + pitch;
        if (!pitchedAudio.isPlaying)
        {
            pitchedAudio.Play();
        }
        else if(pitchedAudio.time>0.1f)
        {
            pitchedAudio.Play();
        }
    }
    public void SetMusicVolume(float volume)
    {
        mixer.audioMixer.SetFloat("SoundsVolume", volume);
    }
    public void SetSoundsVolume(float volume)
    {        
        mixer.audioMixer.SetFloat("MusicVolume", volume);
    }
    public void Mute(bool mute)
    {
        if (mute)
        {
            mixer.audioMixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            mixer.audioMixer.SetFloat("MasterVolume", 0);
        }
    }
    public void ButtonClick(int typeNumber)
    {
        switch(typeNumber)
        {
            case 1: butonClick.Play();
               break;
            //case 2: buyAtCoins.Play();
            //    break;
            //default: otherButtons.Play();
            //    break;
        }
    }
    //public void StartVibro()
    //{
    //    if (!settings.vibro)
    //    {
    //        Vibration.Vibrate(100);
    //    }
    //}
}
