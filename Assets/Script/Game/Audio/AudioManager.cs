using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("����� Mixer"), SerializeField]
    public AudioMixer audioMixer;

   

    [Header("Ÿ�� ����"), SerializeField]
    AudioSource hitSoundSource;
    [Header("���� ����"), SerializeField]
    AudioSource landingSoundSource;
    [Header("���� ����"),SerializeField]
    AudioSource jumpSoundSource;
    [Header("Ȯ�� ����"), SerializeField]
    AudioSource confirmSoundSource;
    [Header("���� ����"), SerializeField]
    AudioSource buySoundSource;
    [Header("���� ����"),SerializeField]
    AudioSource denySoundSource;

    [Header("BGM �迭"), SerializeField]
    AudioSource[] BgmSources;

    int previndex = 0;

   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }

        }
    }

  

    public void MasterCtrl(Slider slider)
    {
        float sound = slider.value;
        if (sound == -40f)
        {
            audioMixer.SetFloat("Master", -80);
        }
        else
        {
            audioMixer.SetFloat("Master", sound);
        }
    }

    public void BgmCtrl(Slider slider)
    {
        float sound = slider.value;
        if(sound==-40f)
        {
            audioMixer.SetFloat("BGM", -80);
        }
        else
        {
            audioMixer.SetFloat("BGM", sound);
        }
    }
    public void EffectCtrl(Slider slider)
    {
        float sound = slider.value;
        if (sound == -40f)
        {
            audioMixer.SetFloat("Effect", -80);
        }
        else
        {
            audioMixer.SetFloat("Effect", sound);
        }
    }


    public void HitSound()
    {
       
        hitSoundSource.Play();
    }
    public void LandingSound()
    {
        if (landingSoundSource.isPlaying)
        {
            return;
        }
        landingSoundSource.Play();
    }
    public void JumpSound()
    {
        if (jumpSoundSource.isPlaying)
        {
            return;
        }
        jumpSoundSource.Play();
    }
    public void ConfirmSound()
    {
        if (confirmSoundSource.isPlaying)
        {
            return;
        }
        confirmSoundSource.Play();
    }
    public void BuySound()
    {
        if (buySoundSource.isPlaying)
        {
            return;
        }
        buySoundSource.Play();
    }
    public void DenySound()
    {
        if (denySoundSource.isPlaying)
        {
            return;
        }
        denySoundSource.Play();
    }

    
    public void BgmPlay(int index)
    {
        if(BgmSources[previndex].isPlaying)
        {
            BgmSources[previndex].Stop();
        }
        BgmSources[index].Play();
        previndex = index;
    }
}
