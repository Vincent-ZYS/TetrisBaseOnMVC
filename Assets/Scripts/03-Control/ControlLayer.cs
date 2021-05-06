using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLayer : MonoBehaviour
{
    [HideInInspector]
    public ModelLayer modelGo;
    [HideInInspector]
    public ViewLayer viewGo;
    [HideInInspector]
    public CameraManager cameraManager;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public AudioManager audioManager;
    [HideInInspector]
    public FSMSystem fsmSystem;
    /// <summary>
    /// The initiation of Model and View layer.
    /// </summary>
    private void Awake()
    {
        modelGo = GameObject.FindGameObjectWithTag("Model").GetComponent<ModelLayer>();
        viewGo = GameObject.FindGameObjectWithTag("View").GetComponent<ViewLayer>();
        cameraManager = GetComponent<CameraManager>();
        gameManager = GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();
    }
    
    private void Start()
    {
        InitiateFSMSystem();
    }

    /// <summary>
    /// Initiate the FSM System game object, and set the MenuState as default state for initiation
    /// </summary>
    private void InitiateFSMSystem()
    {
        fsmSystem = new FSMSystem();
        FSMState[] fsmStates = GetComponentsInChildren<FSMState>();
        foreach(FSMState state in fsmStates)
        {
            fsmSystem.AddState(state,this);
        }
        MenuState menuState = GetComponentInChildren<MenuState>();
        fsmSystem.SetCurrentState(menuState);
    }
}
