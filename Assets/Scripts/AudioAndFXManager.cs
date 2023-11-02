using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAndFXManager : MonoBehaviour
{
    [SerializeField]
    private List<SpecialFXSys> FXSys = new List<SpecialFXSys>();
    //[SerializeField]
    //private List<AudioSource> m_audiosources;
    [SerializeField]
    private AudioSource m_feetAS;
    [SerializeField]
    private AudioSource m_hitAS;
    [SerializeField]
    private AudioSource m_audioSource2Play;
    [SerializeField]
    //private AudioClip m_audioClip;
    private ParticleSystem m_particleSystem;


    public void PlayFXSys(EActionTYpe actionType)
    {
        SpecialFXSys sys = FXSys[(int)actionType];
        Debug.Log("PlayFXSys with actionType: " + actionType);
        switch (actionType)
        {
            case EActionTYpe.Walk:
                m_audioSource2Play = m_feetAS;
                m_audioSource2Play.clip = sys.audioClips[0];
               
                //m_particleSystem = sys.particleSystems[0];
                break;
            case EActionTYpe.Jump:
                m_feetAS.clip = sys.audioClips[0];
                m_audioSource2Play = m_feetAS;
                // m_particleSystem = sys.particleSystems[0];
                sys.particleSystems[0].Play();
                break;
            case EActionTYpe.Land:
                m_audioSource2Play = m_feetAS;
                m_audioSource2Play.clip = sys.audioClips[0];
                //m_particleSystem = sys.particleSystems[0];
                sys.particleSystems[0].Play();
                break;
            case EActionTYpe.Attack:
                m_audioSource2Play = m_hitAS;
                m_audioSource2Play.clip = sys.audioClips[0];
                //m_particleSystem = sys.particleSystems[0];
                break;
            case EActionTYpe.Hit:
                m_audioSource2Play = m_hitAS;
                m_audioSource2Play.clip = sys.audioClips[0];
                m_particleSystem = sys.particleSystems[0];
                break;
            default:
                Debug.LogWarning("No Fxsys of this type");
                break;
        }

        m_audioSource2Play?.Play();
        
    }
}

public enum EActionTYpe
{
    Walk,
    Jump,
    Land,
    Attack,
    Hit,
    Count
}

[System.Serializable]
public struct SpecialFXSys
{
    public EActionTYpe actionType;
    [SerializeField]
    public List<AudioClip> audioClips;
    [SerializeField]
    public List<ParticleSystem> particleSystems;
}
