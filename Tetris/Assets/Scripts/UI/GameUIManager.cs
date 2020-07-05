using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Configs;

public class GameUIManager : MonoBehaviour
{
    [Header("Preview Tetrominos")]
    [SerializeField]
    private Transform [] previewPositions;

    private List<PreviewTetromino> previewTetrominos;
    private PreviewTetrominoFactory previewTetrominoFactory;


    private void OnEnable()
    {
        EventManager.TetrominoSpawnEvent += DisplayPreview;
    }

    private void OnDisable()
    {
        EventManager.TetrominoSpawnEvent -= DisplayPreview;
    }

    public void SetSpawnConfig(BaseConfig config)
    {   
        TetrominoSpawnConfig spawnConfig = (TetrominoSpawnConfig) config;
        previewTetrominoFactory = new PreviewTetrominoFactory(spawnConfig.PreviewTetrominoPrefabs, 
                                  spawnConfig.MaxTetrominosInQueue, this.transform);

        previewTetrominoFactory.CreatePool();
       
        previewTetrominos = new List<PreviewTetromino>();
    }

    private void DisplayPreview(int id)
    {   
        PreviewTetromino p = previewTetrominoFactory.GetPreview(id);

        previewTetrominos.Add(p);
        p.gameObject.SetActive(true);
        
        if(previewTetrominos.Count > 3)
        {
            previewTetrominos[0].gameObject.SetActive(false);
            previewTetrominos.RemoveAt(0);
            SetPositions();
        }
            
    }


    private void SetPositions()
    {
        for(int i = 0; i < previewTetrominos.Count; i++)
        {
            previewTetrominos[i].transform.position = previewPositions[i].position;
        }
    }


    

}
