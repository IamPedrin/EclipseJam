using UnityEngine;

public class EnemyFollowPlayerMelee : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float lineOfSight = 8f;
    [SerializeField] private float attackRange = 1.5f;

    [Header("Ataque")]
    [SerializeField] private float attackCooldown = 1f;
    private float nextAttackTime;

    private Transform player;
    private bool isFacingRight = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer > attackRange)
        {
            FollowPlayer();
        }
        // Se estiver no alcance de ataque, atacar
        else if (distanceFromPlayer <= attackRange && Time.time > nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }
    }

    private void Attack()
    {
        Debug.Log("Ataque Inimigo");
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}