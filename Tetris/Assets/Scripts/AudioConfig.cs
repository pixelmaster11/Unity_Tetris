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

    }



}
