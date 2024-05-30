using UnityEngine;
using UnityEngine.AI;

public class ChickenBehavior : MonoBehaviour
{
    public float roamRadius = 10f;
    public float roamTime = 3f;
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public float health = 50f;

    private NavMeshAgent navMeshAgent;
    private float roamTimer;
    private bool isAttacked = false;
    private Transform player;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        roamTimer = roamTime;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isAttacked)
        {
            Roam();
        }
        else
        {
            Attack();
        }
    }

    private void Roam()
    {
        roamTimer += Time.deltaTime;

        if (roamTimer >= roamTime)
        {
            Vector3 newPos = RandomNavSphere(transform.position, roamRadius, -1);
            navMeshAgent.SetDestination(newPos);
            roamTimer = 0;
        }
    }

    private void Attack()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= attackRange)
        {
            // Attack logic here (e.g., reduce player health)
            Debug.Log("Chicken attacks player!");
        }
        else
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            isAttacked = true;
        }
    }

    private void Die()
    {
        // Death logic here
        Destroy(gameObject);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
