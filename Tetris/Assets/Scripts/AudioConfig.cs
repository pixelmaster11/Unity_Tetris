using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{

    /// <summary>
    /// Config class responsible for all audio clips / settings
    /// </summary>
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioConfigSO", order = 1)]
    public class AudioConfig : BaseConfig
    {
        
        public AudioClip BgClip;
        public AudioClip HoldSfx;
        public AudioClip RotateSfx;
        public AudioClip MoveSfx;
        public AudioClip SnapSfx;
        public AudioClip LineClearSfx;

        public float BGPitchIncrement = 0.01f;
        public float MoveSfxVolumeScale = 0.2f;
        public float HoldSfxVolumeScale = 1f;
        public float RotateSfxVolumeScale = 0.5f;
        public float SnapSfxVolumeScale = 0.3f;
    }



}
