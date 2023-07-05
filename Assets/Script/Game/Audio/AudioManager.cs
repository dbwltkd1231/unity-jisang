using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("오디오 Mixer"), SerializeField]
    public AudioMixer audioMixer;

   

    [Header("타격 사운드"), SerializeField]
    AudioSource hitSoundSource;
    [Header("착지 사운드"), SerializeField]
    AudioSource landingSoundSource;
    [Header("점프 사운드"),SerializeField]
    AudioSource jumpSoundSource;
    [Header("확인 사운드"), SerializeField]
    AudioSource confirmSoundSource;
    [Header("구매 사운드"), SerializeField]
    AudioSource buySoundSource;
    [Header("실패 사운드"),SerializeField]
    AudioSource denySoundSource;

    [Header("BGM 배열"), SerializeField]
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
