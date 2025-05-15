using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour 
{
    public Transform[] PatrolPoints;
    public Transform Player;
    public float SightRange = 10f;
    public Animator Animator;

    public NavMeshAgent NavMeshAgent { get; private set; }

    private IEnemyState currentState;

    void Start() {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        ChangeState(new IdleState());
    }

    void Update() {
        currentState?.Update();
    }

    public void ChangeState(IEnemyState newState) {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public bool PlayerInSight() {
        return Vector3.Distance(transform.position, Player.position) <= SightRange;
    }
}

