using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisShape : MonoBehaviour
{
    private bool isFallDoneOrPause = false;
    private float timer = 0f;
    private float fallIntervalTime;
    private Transform pivot;
    private ControlLayer ctrl;

    private void Update()
    {
        if (isFallDoneOrPause) { DestroyEmptySelf(); return; }
        TetrisShapeFall();
        InputControl(); 
        InputAccelerateFall();
    }

    public void InitCurrentShape(Color color, ControlLayer ctrl, GameManager gm)
    {
        foreach(Transform tf in transform)
        {
            if(tf.tag == "Block")
            {
                this.ctrl = ctrl;
                pivot = transform.Find("pivot");
                fallIntervalTime = ctrl.modelGo.tetrisNormalFallSpeedTime;
                tf.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    private void DestroyEmptySelf()
    {
        if(ctrl.modelGo.isEliminateRow)
        {
            foreach(Transform child in transform)
            {
                if (child.tag == "Block") { return; }
            }
            Destroy(gameObject);
        }
    }

    public void DestroyCurrentShapeBlock()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
    }

    private void TetrisShapeFall()
    {
        timer += Time.deltaTime;
        if(timer>= fallIntervalTime)
        {
            timer = 0f;
            Vector3 pos = transform.position;
            pos.y -= 1f;
            transform.position = pos;
            if(!ctrl.modelGo.IsValidMapPosition(transform))
            {
                pos.y += 1f;
                transform.position = pos;
                isFallDoneOrPause = true;
                ctrl.gameManager.TetrisShapeFalledAndNull();
                //TODO Control a series of behaviour after current shape object falled
                if (ctrl.modelGo.PlaceTetrisShapeInMap2DArray(transform))
                {
                    ctrl.audioManager.PlayTetrisRowEliminatedSound();
                    ctrl.viewGo.UpdateScoreGameUI(ctrl.modelGo.CurScore, ctrl.modelGo.HighestScore);
                }
                if (ctrl.modelGo.IsGameOverAfterTetrisShapeFalled())
                {
                    //TODO GAMEOVER UI
                    PlayState playState = (PlayState)ctrl.fsmSystem.CurrentState;
                    if (playState != null) { playState.OnGameOverCalled(); }
                }
                return;
            }
            ctrl.audioManager.PlayDropSound();
        }
    }

    public void TetrisShapePause()
    {
        isFallDoneOrPause = true;
    }

    public void TetrisShapeResume()
    {
        isFallDoneOrPause = false;
    }

    private void InputControl()
    {
        float ipt = 0f;
        // Left Key or Right Key to move the current Tetris Shape object
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ipt -= 1f;
        }else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ipt += 1f;
        }
        if(ipt != 0)
        {
            Vector3 pos = transform.position;
            pos.x += ipt;
            transform.position = pos;
            if(!ctrl.modelGo.IsValidMapPosition(transform))
            {
                pos.x -= ipt;
                transform.position = pos;
            }else
            {
                ctrl.audioManager.PlayLeftRightMoveSound();
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)) // Rotate the current Tetris Shape object
        {
            transform.RotateAround(pivot.position, Vector3.forward, -90f);
            if(!ctrl.modelGo.IsValidMapPosition(transform))
            {
                transform.RotateAround(pivot.position, Vector3.forward, 90f);
            }else
            {
                ctrl.audioManager.PlayLeftRightMoveSound();
            }
        }
    }

    private void InputAccelerateFall()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            fallIntervalTime = fallIntervalTime / ctrl.modelGo.tetrisAccelarationRate;
        }else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            fallIntervalTime = ctrl.modelGo.tetrisNormalFallSpeedTime;
        }
    }

}