using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : GameState
{
    [SerializeField]
    private GameObject menuUI;

    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private GameStateController gameStateController;

    public override void Entry()
    {
        if(menuUI == null)
        {
           menuUI = GetComponentsInChildren<Transform>(true)[1].gameObject;
        }

        menuUI.SetActive(true);

        playButton.onClick.AddListener(OnPlay);
        settingsButton.onClick.AddListener(OnSettings);
        quitButton.onClick.AddListener(OnQuit);
    
    }

    public override void StateUpdate()
    {
        
    }

    public override void Exit()
    {
        menuUI.SetActive(false);
        playButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }


    private void OnPlay()
    {
        gameStateController.ChangeState(Enums.GameStateType.GamePlayState);
    }


    private void OnSettings()
    {

    }


    private void OnQuit()
    {

    }

  
}
