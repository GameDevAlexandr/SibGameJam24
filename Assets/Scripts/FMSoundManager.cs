using UnityEngine;
using FMODUnity;

public class FMSoundManager : MonoBehaviour
{
    [field: SerializeField] public EventReference EnemyHi;

    public struct SoundDat 
    {          
        
    }
    public void Attack()
    {
        RuntimeManager.PlayOneShot(_attack, transform.position);
    }

}
