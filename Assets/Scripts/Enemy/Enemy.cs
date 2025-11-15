using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public StateMachine stateMachine;

    // States
    public IState idleState;
    public IState patrolState;
    public IState attackState;

    [Header("Enemy Stats")]
    public int health = 100;
    public int cooldown = 5;
    public float visionRange = 10f;
    public float attackRange = 12f;

    [Header("References")]
    public Transform player;      
    public Gun enemyGun;          
    private NavMeshAgent agent;   

    public bool CanSeePlayer { get; private set; }

    private void Awake()
    {
        stateMachine = new StateMachine();
        agent = GetComponent<NavMeshAgent>();

        idleState = new EnemyIdleState(this);
        patrolState = new EnemyPatrolState(this);
        attackState = new EnemyAttackState(this);

        stateMachine.ChangeState(idleState);
    }

    private void Update()
    {
        DetectPlayer();
        stateMachine.Update();
    }

    private void DetectPlayer()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        CanSeePlayer = dist <= visionRange;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        Debug.Log($"{gameObject.name} took {dmg} damage. HP: {health}");
        
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(ScoreManager.Instance.damageScore);

        if (health <= 0)
        {
            Die();
            return;
        }

        stateMachine.ChangeState(attackState);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(ScoreManager.Instance.killScore);
            
        int enemiesLeft = FindObjectsOfType<Enemy>().Length - 1;
        EventManager.EnemyKilled(enemiesLeft);
            
        Destroy(gameObject);
    }

    public void ChasePlayer()
    {
        if (agent != null && player != null)
            agent.SetDestination(player.position);
    }

    public void StopChase()
    {
        if (agent != null && agent.remainingDistance < 0.1f)
            agent.ResetPath();
    }
}
