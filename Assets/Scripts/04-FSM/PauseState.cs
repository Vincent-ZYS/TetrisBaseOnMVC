using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : FSMState
{
    private void Awake()
    {
        stateID = StateID.Pause;
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
    }
}
