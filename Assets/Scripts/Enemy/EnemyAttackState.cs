using UnityEngine;

public class EnemyAttackState : IState
{
    private Enemy enemy;
    private float nextShootTime = 0f;

    public EnemyAttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enemy entered Attack State");
    }

    public void Update()
    {
        if (enemy.player == null) return;

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (enemy.CanSeePlayer)
        {
            // Chase player with NavMeshAgent
            if (distance > enemy.attackRange)
            {
                enemy.ChasePlayer();
            }
            else
            {
                enemy.StopChase();
                FacePlayer();

                // Shoot at player
                if (Time.time >= nextShootTime)
                {
                    enemy.enemyGun.Use();
                    nextShootTime = Time.time + enemy.enemyGun.gunData.fireRate;
                }
            }
        }
        else
        {
            enemy.StopChase();
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    private void FacePlayer()
    {
        Vector3 dir = (enemy.player.position - enemy.transform.position).normalized;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRot, Time.deltaTime * 5f);
        }
    }

    public void Exit()
    {
        enemy.StopChase();
        Debug.Log("Enemy exiting Attack State");
    }
}
