using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Configs
{
    /// <summary>
    /// Scriptable object that holds references to all config files of the game
    /// </summary>
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ConfigsReference", order = 1)]
    public class ConfigsReferences : ScriptableObject
    {   
        //Spawn config file
        public TetrominoSpawnConfig tetrominoSpawnConfig;

        //All tetromino configs
        public List<TetrominoConfig> tetrominoConfigs;

        //Board config file
        public BoardConfig boardConfig;

        //All input configs
        public List<InputConfig> inputConfigs;

        /// <summary>
        /// Returns the proper config file
        /// </summary>
        /// <param name="configType">Type of config file requested</param>
        /// <returns></returns>
        public BaseConfig GetConfig(ConfigType configType)
        {
            switch(configType)
            {
                case ConfigType.Board:
                return boardConfig;

                case ConfigType.KeyboardInput:
                return inputConfigs[0];

                case ConfigType.TetrominoSpawn:
                return tetrominoSpawnConfig;

                default:
                return null;
            }
        }

    }

}

