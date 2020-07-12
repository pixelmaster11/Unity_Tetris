using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;

public class AudioManager : MonoBehaviour
{
    

    AudioConfig config;
    AudioSource bgSource;

    List<AudioSource> sfxSources = new List<AudioSource>();
    
    //Subscribe to events
    private void OnEnable()
    {
        EventManager.HoldPieceEvent += PlayHoldSfx;
        EventManager.LineCompleteEvent += PlayLineCompleteSfx;
        EventManager.MoveSuccessEvent += PlayMoveSfx;
        EventManager.RotateSuccessEvent += PlayRotateSfx;
        EventManager.SnapSuccessEvent += PlaySnapSfx;
    }

     //Unsubscribe to Events
    private void OnDisable()
    {        
        EventManager.HoldPieceEvent -= PlayHoldSfx;
        EventManager.LineCompleteEvent -= PlayLineCompleteSfx;
        EventManager.MoveSuccessEvent -= PlayMoveSfx;
        EventManager.RotateSuccessEvent -= PlayRotateSfx;
        EventManager.SnapSuccessEvent -= PlaySnapSfx;
    }

    //Sets the preview factory
    public void SetSpawnConfig(BaseConfig _config)
    {   
        config = (AudioConfig) _config;
        PlayBG();   
    }



    private void PlayHoldSfx(int id, int rotID)
    {
        AudioSource src = sfxSources[GetFreeAudioSource()];
        src.PlayOneShot(config.HoldSfx);
    }


    private void PlayLineCompleteSfx(int linesCleared)
    {   
        AudioSource src = sfxSources[GetFreeAudioSource()];
        src.PlayOneShot(config.LineClearSfx);
             
    }


    private void PlayMoveSfx()
    {
        AudioSource src = sfxSources[GetFreeAudioSource()];
        src.PlayOneShot(config.MoveSfx, 0.1f);
    }

    private void PlayRotateSfx()
    {
        AudioSource src = sfxSources[GetFreeAudioSource()];
        src.PlayOneShot(config.RotateSfx, 0.1f);
    }

      private void PlaySnapSfx()
    {
        AudioSource src = sfxSources[GetFreeAudioSource()];
        src.PlayOneShot(config.SnapSfx, 0.1f);
    }



    private void PlayBG()
    {
        TryGetComponent<AudioSource>(out bgSource);

        if(bgSource == null)
        {
            bgSource = gameObject.AddComponent<AudioSource>();
        }

        bgSource.clip = config.BgClip;
        bgSource.Play();
    }


    private AudioSource AddAudioSource()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        sfxSources.Add(source);

        return source;
    }


    private int GetFreeAudioSource()
    {
        for (int i = 0; i < sfxSources.Count; i++)
        {
            if(!sfxSources[i].isPlaying)
            {
                
                return i;
            }
        }

        AddAudioSource();

        return (sfxSources.Count - 1);
    }

}
