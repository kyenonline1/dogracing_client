using AppConfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {



    public void PlayAudio(AudioSource source)
    {
        //source.volume = _volumeSet;
        if (ClientConfig.Sound.ENABLE)
        {
            source.volume = ClientConfig.Setting.VOLUM_SOUND;
            source.Play();
        }
    }

    public void PlayBackgroundSound(AudioSource source)
    {
        if (ClientConfig.Sound.ENABLE_BGSOUND)
        {
            source.volume = 0;
            StartCoroutine(source.FadeSound(1, ClientConfig.Setting.VOLUM_MUSIC));
            source.Play();
        }
    }

    public IEnumerator PlayAudioSquence(AudioSource[] sources)
    {
        for(int i = 0; i < sources.Length; i++)
        {
            sources[i].Play();
            Debug.Log("PlayAudioSquence: " + i + "sources[i].time: " + sources[i].timeSamples);
            yield return new WaitForSeconds(sources[i].timeSamples);
        }
        //foreach (AudioSource s in sources)
        //{
        //    //s.volume = _volumeSet;
        //    s.Play();
        //    yield return new WaitForSeconds(s.time);
        //}
    }

    public void PlayAudioDelayed(AudioSource source, float delayTime = 0f)
    {
        source.volume = ClientConfig.Setting.VOLUM_SOUND;
        source.PlayDelayed(delayTime);
    }

    public void StopAudio(AudioSource source)
    {
        source.Stop();
    }
    
}
