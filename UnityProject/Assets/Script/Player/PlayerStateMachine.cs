using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Color = UnityEngine.Color;

public class PlayerStateMachine : MonoBehaviour
{
    #region PublicVariables

    #region InspectorVariables

    public bool DebugMode = true;
    public PlayerMovementParameters PlayerMovementParameters;

    #endregion
    #region NonInspectorVariables
    [HideInInspector]
    public Vector2 Velocity;
    [HideInInspector]
    public float attackCooldown = 0f;
    [HideInInspector]
    public InputsManager InputsManager { get; private set; }
    [HideInInspector]
    public Animator Animator;
    #endregion
    #endregion
    #region PrivateVariables
    private CharacterController _CharacterController => GetComponent<CharacterController>();

    #endregion
    #region States

    #region privateStates
    private IdlePlayerState _idleState { get; } = new IdlePlayerState();
    private RunningPlayerState _runningState { get; } = new RunningPlayerState();
    private DeathPlayerState _deathState { get; } = new DeathPlayerState();
    private AttackPlayerState _attackstate { get; } = new AttackPlayerState();
    #endregion

    #region Accessors
    public IdlePlayerState IdleState => _idleState;
    public RunningPlayerState RunningState => _runningState;
    public DeathPlayerState DeathState => _deathState;
    public AttackPlayerState AttackState => _attackstate;
    #endregion
    public PlayerState[] AllStates => new PlayerState[]
    {
        _idleState,
        _runningState,
        _deathState,
        _attackstate,
    };

    #endregion

    #region CurrentStates
    private PlayerState StartState => _idleState;
    public PlayerState CurrentState { get; set; }
    [HideInInspector]
    public PlayerState PreviousState { get; set; }

    #endregion

    #region Debug
    void OnGUI()
    {
        if (InputsManager == null || !DebugMode) return;

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 14;
        style.fontStyle = FontStyle.Bold;

        // Cr�er le fond
        GUI.backgroundColor = new Color(0, 0, 0, 0.7f);
        GUI.Box(new Rect(10, 10, 400, 160), "");

        string debugText = "";
        debugText += "Current Profile Name: " + PlayerMovementParameters.name + "\n";
        debugText += "Current State: " + CurrentState.GetType().Name + "\n";
        debugText += "Move X: " + InputsManager.MoveX + "\n";
        debugText += "Move Y: " + InputsManager.MoveY + "\n";
        debugText += "Velocity: " + Velocity.ToString("F2") + "\n";
        debugText += "Interacted: " + InputsManager.InputInteract + "\n";
        debugText += "Attack Cooldown: " + attackCooldown.ToString("F2") + "\n";
        GUI.Label(new Rect(20, 20, 200, 300), debugText, style);
    }
    #endregion

    private void Start()
    {
        Debug.developerConsoleVisible = true;

        InputsManager = InputsManager.instance;
        Animator = GetComponentInChildren<Animator>();
        _InitAllStates();
        _InitStateMachine();
    }

    private void _InitStateMachine()
    {
        ChangeState(StartState);
    }
    private void Update()
    {
        UpdateAnimator();
        UpdateCooldowns();
    }
    private void FixedUpdate()
    {
        CurrentState.StateUpdate();

        var cameraRoation = Camera.main.transform.rotation.eulerAngles;
        Vector3 VelocityXZ = new Vector3(Velocity.x, 0, Velocity.y);

        VelocityXZ = Quaternion.Euler(0, cameraRoation.y, 0) * VelocityXZ;
        _CharacterController.Move(VelocityXZ * Time.fixedDeltaTime);

        if (VelocityXZ.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(VelocityXZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * PlayerMovementParameters.RotationSpeed);
        }
    }

    private void _InitAllStates()
    {
        foreach (var state in AllStates)
        {
            state.Init(this);
        }
    }
    public void ChangeState(PlayerState state)
    {
        if (CurrentState != null)
        {
            CurrentState.StateExit(state);
        }
        PreviousState = CurrentState;
        CurrentState = state;
        if (CurrentState != null)
        {
            CurrentState.StateEnter(state);
        }
    }

    private void UpdateAnimator()
    {
        Animator.SetFloat("WalkSpeed", Velocity.magnitude / PlayerMovementParameters.maxSpeed);
    }

    private void UpdateCooldowns()
    {
        if (attackCooldown > 0)
        {
            attackCooldown = Mathf.Clamp(attackCooldown - Time.deltaTime, 0, PlayerMovementParameters.AttackSpeed);
        }
    }

}
