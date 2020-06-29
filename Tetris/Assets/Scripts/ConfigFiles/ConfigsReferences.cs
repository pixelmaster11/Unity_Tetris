using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Configs
{

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ConfigsReference", order = 1)]
    public class ConfigsReferences : ScriptableObject
    {
        public TetrominoSpawnConfig tetrominoSpawnConfig;

        public List<TetrominoConfig> tetrominoConfigs;

        public BoardConfig boardConfig;

        public List<InputConfig> inputConfigs;


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

