using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;      
    }
    
    /// <summary>
    /// This Method is use to zoom the camera in, better to zoom the Tetris map.
    /// </summary>
    public void OnZoomIn()
    {
        mainCamera.DOOrthoSize(12.2f,0.5f);
    }

    /// <summary>
    /// This Method is use to zoom the camera out, better to zoom the Tetris map.
    /// </summary>
    public void OnZoomOut()
    {
        mainCamera.DOOrthoSize(18f, 0.5f);
    }
}
