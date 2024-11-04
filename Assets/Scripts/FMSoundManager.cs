using UnityEngine;
using FMODUnity;
using static Enums;

public class FMSoundManager : MonoBehaviour
{
    public static FMSoundManager Sound { get; private set; }
    [SerializeField] private SoundData[] _sonuds;

    [System.Serializable]
    public struct SoundData
    {
        public EventReference er;
        public SoundName sName;
        public bool isState;
        public FMOD.Studio.EventInstance state;
    }
    private void Awake()
    {
        Sound = this;
        for (int i = 0; i < _sonuds.Length; i++)
        {
            if (_sonuds[i].isState)
            {
                _sonuds[i].state = RuntimeManager.CreateInstance(_sonuds[i].er);
            }
        }
    }
  
    public void Play(SoundName sName, Vector2 position)
    {
        for (int i = 0; i < _sonuds.Length; i++)
        {
            if(_sonuds[i].sName == sName)
            {
                if (_sonuds[i].isState)
                {
                    _sonuds[i].state.start();
                }
                else
                {
                    RuntimeManager.PlayOneShot(_sonuds[i].er, position);
                }
            }
            
        }
    }
    public void Play(SoundName sName)
    {
        Play(sName, transform.position);
    }
    public void Stop(SoundName sName)
    {
        for (int i = 0; i < _sonuds.Length; i++)
        {
            if (_sonuds[i].sName == sName)
            {
                if (_sonuds[i].isState)
                {
                    _sonuds[i].state.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
            }

        }
    }

}
