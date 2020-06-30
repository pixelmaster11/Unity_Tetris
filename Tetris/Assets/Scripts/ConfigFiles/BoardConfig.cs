
using UnityEngine;
using Enums;

namespace Configs
{

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BoardConfigSO", order = 1)]
    public class BoardConfig : BaseConfig
    {
        //Board widht and height
        public int width;
        public int height;

        //Offsets to be applied in positioning of sprites
        public float offX;
        public float offY;

        //Sprite to use for the board
        public SpriteRenderer backgroundSprite;

        //Sprite to use for the board boundary
        public SpriteRenderer boundarySprite;

        //Code to be used for empty space
        public const int EMPTY_SPACE = 100;

        //Code to be used for Boundary
        public const int BOUNDARY = 101;

        //Type of rotation to be used to rotate tetrominos
        public RotateType rotationType;

        //Time after which tetrominos automatically fall
        public float fallTime = 0.5f;

    }
}
