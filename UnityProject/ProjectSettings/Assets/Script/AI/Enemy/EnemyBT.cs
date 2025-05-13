using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyBT : MonoBehaviour {
    public Transform Player;
    public float SightRange = 10f;
    public Transform[] PatrolPoints;
    public NavMeshAgent Agent { get; private set; }

    private Node root;

    void Start() {
        Agent = GetComponent<NavMeshAgent>();

        root = new Selector(new List<Node> {
            new Sequence(new List<Node> {
                new IsPlayerVisibleNode(this),
                new ChaseNode(this)
            }),
            new PatrolNode(this)
        });
    }

    void Update() {
        root.Evaluate();
    }
}