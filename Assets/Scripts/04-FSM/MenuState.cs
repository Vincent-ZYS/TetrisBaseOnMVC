using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState
{
    private void Awake()
    {
        stateID = StateID.Menu;
        AddTransition(Transition.OnStartGameClick, StateID.Play);
        AddTransition(Transition.OnRestartGameClick, StateID.Play);
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        ctrl.viewGo.ShowMenu(ctrl.modelGo.GameRoundCounts,ctrl.modelGo.HighestScore);
        ctrl.cameraManager.OnZoomOut();
        IsMuteSoundInitiation();
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
        ctrl.viewGo.HideMenu();
    }
    public void OnStartGameClick()
    {
        ctrl.audioManager.PlayCursorSound();
        fsm.PerformTransition(Transition.OnStartGameClick);
    }

    public void OnRestartGameClick()
    {
        ctrl.audioManager.PlayCursorSound();
        ctrl.viewGo.ShowPromptUIPanel("RestartGame");
    }

    public void OnSettingClick()
    {
        ctrl.audioManager.PlayCursorSound();
        ctrl.viewGo.ShowSettingUI();
    }

    public void OnSettingBackHomeClick()
    {
        ctrl.audioManager.PlayCursorSound();
        ctrl.viewGo.HideSettingUI();
    }

    public void OnRankClick()
    {
        ctrl.audioManager.PlayCursorSound();
        ctrl.viewGo.ShowRankUI();
    }

    public void OnRankBackHomeClick()
    {
        ctrl.audioManager.PlayCursorSound();
        ctrl.viewGo.HideRankUI();
    }

    public void OnMuteSoundClick()
    {
        bool isMute = ctrl.audioManager.MuteAudio();
        ctrl.viewGo.OnMuteSoundImageActive(isMute);
        ctrl.modelGo.SetIsSoundMutedOrNotData(isMute);
        ctrl.audioManager.PlayCursorSound();
    }
    
    public void OnDeleteAllLocalDataClick()
    {
        ctrl.audioManager.PlayCursorSound();
        ctrl.viewGo.ShowPromptUIPanel("DeleteData");
    }

    public void OnConfirmPromptUIPanelClick()
    {
        switch(ctrl.viewGo.PromptBehaviourName)
        {
            case "DeleteData":
                OnHidePromptUIPanelClick();
                ctrl.modelGo.DeleteAllLocalData();
                ctrl.viewGo.ShowRanKDataTxt(ctrl.modelGo.GameRoundCounts, ctrl.modelGo.HighestScore);
                break;
            case "RestartGame":
                OnHidePromptUIPanelClick();
                ctrl.gameManager.DestroyCurTetrisShape();
                ctrl.modelGo.GameRestartCalled();
                fsm.PerformTransition(Transition.OnRestartGameClick);
                break;
        }
    }

    public void OnHidePromptUIPanelClick()
    {
        ctrl.audioManager.PlayCursorSound();
        ctrl.viewGo.HidePromptUIPanel();
    }

    private void IsMuteSoundInitiation()
    {
        ctrl.audioManager.IsMute = ctrl.modelGo.IsMute;
        ctrl.viewGo.OnMuteSoundImageActive(ctrl.modelGo.IsMute);
    }
}
