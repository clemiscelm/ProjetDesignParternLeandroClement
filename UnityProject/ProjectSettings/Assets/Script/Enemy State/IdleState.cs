
using UnityEngine;

public class IdleState : IEnemyState 
{
    private Enemy enemy;
    private float timer;

    public void Enter(Enemy enemy) {
        this.enemy = enemy;
        timer = 0f;
        enemy.NavMeshAgent.isStopped = true;
    }

    public void Update() {
        timer += Time.deltaTime;
        if (timer > 2f) {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit() {
        enemy.NavMeshAgent.isStopped = false;
    }
}