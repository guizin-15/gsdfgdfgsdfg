using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float detectionRadius = 4f;
    public float wanderSpeed = 0.3f;
    public float wanderChangeInterval = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector2 movement;
    private Vector2 wanderDirection;
    private float wanderTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        SetRandomWanderDirection();
    }

    void Update()
    {
        if (player == null) return;

        // Checa se o player está morto
        PlayerController pc = player.GetComponent<PlayerController>();
        bool playerIsDead = pc != null && pc.isDead;

        // Se o player estiver morto, vagar
        if (playerIsDead)
        {
            Wander();
            return;
        }

        // Player está vivo → checar distância
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // Perseguir o player
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction * moveSpeed;

            // Flipar no eixo X
            spriteRenderer.flipX = movement.x < 0;

            animator.SetBool("IsMoving", true);
        }
        else
        {
            // Vagar
            Wander();
        }
    }

        void Wander()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0f)
        {
            SetRandomWanderDirection();
        }

        movement = wanderDirection * wanderSpeed;

        if (movement.x != 0)
            spriteRenderer.flipX = movement.x < 0;

        animator.SetBool("IsMoving", true);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    void SetRandomWanderDirection()
    {
        // Gera direção aleatória com leve tendência horizontal
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-0.2f, 0.2f); // quase nada no Y
        wanderDirection = new Vector2(x, y).normalized;
        wanderTimer = wanderChangeInterval;
    }

        void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.CompareTag("Player"))
        {
            // Acessa o script PlayerController e chama a função de morte
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
