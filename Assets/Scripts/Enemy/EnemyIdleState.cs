using UnityEngine;

public class EnemyIdleState : IState
{
    private Enemy enemy;

    public EnemyIdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enemy entered Idle State");
    }

    public void Update()
    {
       
        if (Time.time > enemy.cooldown)
        {
            enemy.stateMachine.ChangeState(enemy.patrolState);
        }
    }

    public void Exit()
    {
        Debug.Log("Enemy exiting Idle State");
    }
}
