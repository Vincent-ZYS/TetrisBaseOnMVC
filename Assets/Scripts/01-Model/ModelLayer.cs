using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLayer : MonoBehaviour
{
    public float tetrisNormalFallSpeedTime = 0.8f; // To store the normal fall rate fixed time
    public float tetrisAccelarationRate = 8f; // To store the acceleration rate of the falling tetirs shape
    public const int NORMAL_ROWS = 20; // Normal rows we have for the tetris map
    public const int MAX_ROWS = 23; // Max rows should more than 20, since player can operate the Tetris on top
    public const int MAX_COLUMNS = 10;

    private Transform[,] map = new Transform[MAX_COLUMNS, MAX_ROWS];

    //The Current Score and Highest Score data, and the round counts date
    private int curScore = 0;
    private int highestScore = 0;
    private int gameRoundCounts = 0;
    private bool isMute = false;

    [HideInInspector]
    public bool isEliminateRow = false; // The flag to distinguish is current elimination happening or not

    public int CurScore { get { return curScore; } }
    public int HighestScore { get { return highestScore; } }
    public int GameRoundCounts { get { return gameRoundCounts; } set { gameRoundCounts = value; } }
    public bool IsMute { get { return isMute; } }

    private void Awake()
    {
        LoadLocalData();
    }

    public bool IsValidMapPosition(Transform tf) // will be called by each spawned Tetris shape
    {
        foreach(Transform child in tf)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            if (!IsInsideMap(pos) || map[(int)pos.x, (int)pos.y] != null) return false;
        }
        return true;
    }

    public bool IsInsideMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0;
    }

    public bool PlaceTetrisShapeInMap2DArray(Transform tf)
    {
        int minY = (int)tf.Find("Block").GetComponent<Transform>().position.Round().y; // To record the current tetirs shape block max Pos.Y
        foreach(Transform child in tf)
        {
            if (child.tag != "Block") continue;
            Vector3 pos = child.position.Round();
            minY = minY > pos.y ? (int)pos.y : minY;
            map[(int)pos.x, (int)pos.y] = child;
        }
        return isEliminateRow = CheckTetrisMapNeedEliminateOrNot(minY);
    }

    private bool IsMapRowTetrisFilled(int row)
    {
        for(int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i, row] == null) { return false; }
        }
        return true;
    }

    private bool CheckTetrisMapNeedEliminateOrNot(int row)
    {
        bool isRowFilled = false;
        for(int i = row; i < MAX_ROWS; i++)
        {
            if(IsMapRowTetrisFilled(i))
            {
                curScore += 100;
                highestScore = curScore > highestScore ? curScore : highestScore;
                isRowFilled = true;
                CleanMapArrayTetrisShapeElements(i);
                MoveAboveRowsDown(i + 1);
                i--;
            }
        }
        SetHighestScoreAndRoundData();
        return isRowFilled;
    }

    private void CleanMapArrayTetrisShapeElements(int row)
    {
        for(int i = 0; i < MAX_COLUMNS; i++)
        {
            Destroy(map[i, row].gameObject);
            map[i, row] = null;
        }
    }

    private void MoveAboveRowsDown(int row)
    {
        //TODO How to improve the logic to save more time?
        for(int i = row; i < MAX_ROWS; i++)
        {
            MoveAboveRowBlockDown(i);
        }
    }

    private void MoveAboveRowBlockDown(int row)
    {
        for(int i = 0; i < MAX_COLUMNS; i++)
        {
            if(map[i, row] != null)
            {
                map[i, row - 1] = map[i, row];
                map[i, row] = null;
                map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public bool IsGameOverAfterTetrisShapeFalled()
    {
        for (int i = NORMAL_ROWS; i < MAX_ROWS; i++)
        {
            for (int j = 0; j < MAX_COLUMNS; j++)
            {
                if (map[j, i] != null) { return true; }
            }
        }
        return false;
    }

    public void GameRestartCalled()
    {
        curScore = 0;
        for (int i = 0; i < MAX_ROWS; i++)
        {
            for(int j = 0; j < MAX_COLUMNS; j++)
            {
                if(map[j,i] != null)
                {
                    Destroy(map[j, i].gameObject);
                    Destroy(map[j, i].transform.parent.gameObject);
                    map[j, i] = null;
                }
            }
        }
    }

    private void LoadLocalData()
    {
        //curScore = PlayerPrefs.GetInt("CurrentScore", 0);
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        gameRoundCounts = PlayerPrefs.GetInt("RoundCounts", 0);
        isMute = PlayerPrefs.GetInt("IsSoundMute", 0)==1?true:false;
    }

    public void DeleteAllLocalData()
    {
        highestScore = 0;
        gameRoundCounts = 0;
        SetHighestScoreAndRoundData();
    }

    public void SetHighestScoreAndRoundData()
    {
        PlayerPrefs.SetInt("HighestScore", highestScore);
        PlayerPrefs.SetInt("RoundCounts", GameRoundCounts);
    }

    public void SetIsSoundMutedOrNotData(bool isOff)
    {
        this.isMute = isOff;
        int isMute = this.isMute == true ? 1 : 0;
        PlayerPrefs.SetInt("IsSoundMute", isMute);
    }
}
