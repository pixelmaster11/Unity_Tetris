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

        EventManager.FallTimeDecreaseEvent += IncreaseBGPitch;
        EventManager.GameOverEvent += OnGameOver;
    }

     //Unsubscribe to Events
    private void OnDisable()
    {        
        EventManager.HoldPieceEvent -= PlayHoldSfx;
        EventManager.LineCompleteEvent -= PlayLineCompleteSfx;
        EventManager.MoveSuccessEvent -= PlayMoveSfx;
        EventManager.RotateSuccessEvent -= PlayRotateSfx;
        EventManager.SnapSuccessEvent -= PlaySnapSfx;

        EventManager.FallTimeDecreaseEvent -= IncreaseBGPitch;
        EventManager.GameOverEvent -= OnGameOver;
    }


    public void Enable()
    {
        PlayBG();
    }
    
    //Sets the preview factory
    public void SetSpawnConfig(BaseConfig _config)
    {   
        config = (AudioConfig) _config;
          
    }



    private void IncreaseBGPitch(float time)
    {
        bgSource.pitch += config.BGPitchIncrement;
    }


    private void PlayHoldSfx(int id, int rotID)
    {
        AudioSource src = sfxSources[GetFreeAudioSource()];
        src.PlayOneShot(config.HoldSfx, config.HoldSfxVolumeScale);
    }


    private void PlayLineCompleteSfx(int linesCleared)
    {   
        AudioSource src = sfxSources[GetFreeAudioSource()];
        src.PlayOneShot(config.LineClearSfx);
             
    }


    private void PlayMoveSfx()
    {
        AudioSource src = sfxSources[GetFreeAudioSource()];
        src.PlayOneShot(config.MoveSfx, config.MoveSfxVolumeScale);
    }

    private void PlayRotateSfx()
    {
        AudioSource src = sfxSources[GetFreeAudioSource()];
        src.PlayOneShot(config.RotateSfx, config.RotateSfxVolumeScale);
    }

      private void PlaySnapSfx()
    {
        AudioSource src = sfxSources[GetFreeAudioSource()];
        src.PlayOneShot(config.SnapSfx, config.SnapSfxVolumeScale);
    }



    private void PlayBG()
    {
        TryGetComponent<AudioSource>(out bgSource);

        if(bgSource == null)
        {
            bgSource = gameObject.AddComponent<AudioSource>();
        }

        bgSource.clip = config.BgClip;
        bgSource.loop = true;
        bgSource.Play();
    }


    private void OnGameOver()
    {
        bgSource.Stop();
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
