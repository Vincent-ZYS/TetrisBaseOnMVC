using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : FSMState
{
    private void Awake()
    {
        stateID = StateID.GameOver;
        AddTransition(Transition.OnRestartGameClick, StateID.Play);
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        ctrl.audioManager.PlayGameOverSound();
        ctrl.viewGo.ShowGameOverUI(ctrl.modelGo.CurScore);
        ctrl.modelGo.GameRoundCounts++;
        ctrl.modelGo.SetHighestScoreAndRoundData();
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
        ctrl.viewGo.HideGameOverUI(false);
    }

    public void OnRestartGameClick()
    {
        ctrl.modelGo.GameRestartCalled();
        fsm.PerformTransition(Transition.OnRestartGameClick);
    }

    public void OnHomePageClick()
    {
        ctrl.viewGo.HideGameOverUI(true,ctrl);
    }
}
