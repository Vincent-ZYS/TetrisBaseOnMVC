using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : FSMState
{
    private void Awake()
    {
        stateID = StateID.Play;
        AddTransition(Transition.OnPauseGameClick, StateID.Menu);
        AddTransition(Transition.OnGameOverCalled, StateID.GameOver);
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        ctrl.cameraManager.OnZoomIn();
        ctrl.viewGo.ShowGameUI(ctrl.modelGo.CurScore,ctrl.modelGo.HighestScore);
        ctrl.gameManager.StartGame();
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
        ctrl.viewGo.HideGameUI();
        ctrl.viewGo.OnRestartButtonShow();
        ctrl.gameManager.PauseGame();
    }

    public void OnPauseGameClick()
    {
        ctrl.audioManager.PlayCursorSound();
        fsm.PerformTransition(Transition.OnPauseGameClick);
    }

    public void OnGameOverCalled()
    {
        fsm.PerformTransition(Transition.OnGameOverCalled);
    }
}
