using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyBT : MonoBehaviour {
    public Transform Player;
    public float SightRange = 15f;
    public float ShootingRange = 10f;
    public float MeleeRange = 2f;
    public Transform[] PatrolPoints;
    public Animator Animator;
    public NavMeshAgent Agent { get; private set; }

    private Node root;

    void Start() {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();

        root = new Selector(new List<Node> {
            new Sequence(new List<Node> {
                new IsPlayerVisibleNode(this),
                new Selector(new List<Node> {
                    new Sequence(new List<Node> {
                        new IsInMeleeRangeNode(this),
                        new MeleeAttackNode(this)
                    }),
                    new Sequence(new List<Node> {
                        new IsInShootingRangeNode(this),
                        new ShootNode(this)
                    }),
                    new ChaseNode(this)
                })
            }),
            new PatrolNode(this)
        });
    }

    void Update() {
        root.Evaluate();
        Animator.SetFloat("WalkSpeed", Agent.velocity.magnitude);
    }
}