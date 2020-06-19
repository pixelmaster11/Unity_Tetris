
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BoardConfigSO", order = 1)]
public class BoardConfig : ScriptableObject
{
    //Board widht and height
    public int width;
    public int height;

    //Offsets to be applied in positioning of sprites
    public float offX;
    public float offY;

    //Sprite to use for the board
    public SpriteRenderer backgroundSprite;

    public SpriteRenderer boundarySprite;

   

}
