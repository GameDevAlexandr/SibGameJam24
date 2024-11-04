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
    }
    private void Awake()
    {
        Sound = this;
    }
  
    public void Play(SoundName sName, Vector2 position)
    {
        for (int i = 0; i < _sonuds.Length; i++)
        {
            if(_sonuds[i].sName == sName)
            {
                Debug.Log("Play " + sName);
                RuntimeManager.PlayOneShot(_sonuds[i].er, position);
            }
        }
    }

}
