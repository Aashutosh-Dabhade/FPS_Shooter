using UnityEngine;

public class EnemyPatrolState : IState
{
    private Enemy enemy;

    public EnemyPatrolState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enemy entered Patrol State");
    }

    public void Update()
    {
        
        if (enemy.CanSeePlayer)
        {
            enemy.stateMachine.ChangeState(enemy.attackState);
        }
    }

    public void Exit()
    {
        Debug.Log("Enemy exiting Patrol State");
    }
}
