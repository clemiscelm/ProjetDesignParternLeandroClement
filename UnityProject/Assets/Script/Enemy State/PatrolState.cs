public class PatrolState : IEnemyState 
{
    private Enemy enemy;
    private int currentWaypoint = 0;

    public void Enter(Enemy enemy) {
        this.enemy = enemy;
        MoveToNextWaypoint();
    }

    public void Update() {
        if (enemy.PlayerInSight()) {
            enemy.ChangeState(new ChaseState());
            return;
        }

        if (!enemy.NavMeshAgent.pathPending && enemy.NavMeshAgent.remainingDistance < 0.5f) {
            currentWaypoint = (currentWaypoint + 1) % enemy.PatrolPoints.Length;
            MoveToNextWaypoint();
        }
    }

    private void MoveToNextWaypoint() {
        enemy.NavMeshAgent.destination = enemy.PatrolPoints[currentWaypoint].position;
    }

    public void Exit() {}
}
