using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : GameState
{

    [SerializeField]
    private GameObject gameplayObject;

    [SerializeField]
    private GameStateController gameStateController;

    public override void Entry()
    {
        if(gameplayObject == null)
        {
            gameplayObject = GetComponentsInChildren<Transform>(true)[1].gameObject;
        }

        gameplayObject.SetActive(true);

        EventManager.GameOverQuitEvent += OnQuit;
    }

    public override void StateUpdate()
    {
        
    }


    public override void Exit()
    {
        EventManager.GameOverQuitEvent -= OnQuit;
        gameplayObject.SetActive(false);
    }


    public void OnQuit()
    {
        gameStateController.ChangeState(Enums.GameStateType.MenuState);
    }

}
