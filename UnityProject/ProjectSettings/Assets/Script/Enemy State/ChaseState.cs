using UnityEngine;
public class ChaseState : IEnemyState 
{
    private Enemy enemy;

    public void Enter(Enemy enemy) {
        this.enemy = enemy;
    }

    public void Update() {
        if (!enemy.PlayerInSight()) {
            enemy.ChangeState(new IdleState());
            return;
        }

        enemy.NavMeshAgent.destination = enemy.Player.position;
    }

    public void Exit() {}
}