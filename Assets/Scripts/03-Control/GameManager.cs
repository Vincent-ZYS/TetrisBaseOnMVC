using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGamePause = true; //the bool flag to recognize game is paused or not
    private TetrisShape currentShape = null; // current spawning tetris shape prefab
    private ControlLayer ctrl; // to acquire the control layer's operation
    private Transform blockContainerGo;

    public TetrisShape[] tetrisShapeArray; 
    public Color[] tetrisShapesColor;

    private void Awake()
    {
        ctrl = GetComponent<ControlLayer>();
        blockContainerGo = transform.Find("BlockContainer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGamePause) { return; }
        if (currentShape == null)
        {
            SpawnTetrisShapes();
        }
    }

    public void StartGame()
    {
        isGamePause = false;
        if(currentShape != null)
        {
            currentShape.TetrisShapeResume();
        }
    }

    public void PauseGame()
    {
        isGamePause = true;
        if(currentShape != null)
        {
            currentShape.TetrisShapePause();
        }
    }

    public void SpawnTetrisShapes()
    {
        int shapeGoIndex = Random.Range(0, tetrisShapeArray.Length);
        int shapeColorIndex = Random.Range(0, tetrisShapesColor.Length);
        currentShape = Object.Instantiate(tetrisShapeArray[shapeGoIndex], blockContainerGo);// initiated current spawning shape
        currentShape.InitCurrentShape(tetrisShapesColor[shapeColorIndex],ctrl,this);
    }

    public void DestroyCurTetrisShape()
    {
        currentShape.DestroyCurrentShapeBlock();
        currentShape = null;
    }

    public void TetrisShapeFalledAndNull()
    {
        currentShape = null;
    }

    public void ReloadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
