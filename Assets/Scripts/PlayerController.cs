using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float moveSpeed = 5f;
    public Collider2D bodyCollider;
    public GameObject tombstoneSprite;
    public bool isDead = false;
    public AudioSource footstepSource;
    public AudioClip footstepClip;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float lastHorizontal = 1f;
    private Vector2 lastPosition;
    private float idleDelayTimer = 0f;
    private const float idleDelayThreshold = 0.02f;

    private Vector2 currentInput = Vector2.zero;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        lastPosition = rb.position;
    }

    void Update()
    {
        if (isDead) return;

        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        else if (Input.GetKey(KeyCode.S)) moveY = -1f;

        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
            lastHorizontal = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
            lastHorizontal = -1f;
        }

        currentInput = new Vector2(moveX, moveY).normalized;

        if (lastHorizontal != 0)
        {
            spriteRenderer.flipX = lastHorizontal < 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // Detectar se está tentando se mover (input)
        bool hasInput = currentInput.magnitude > 0;

        // Detectar se está se movendo de fato
        bool positionChanged = Vector2.Distance(rb.position, lastPosition) > 0.001f;
        lastPosition = rb.position;

        if (hasInput && positionChanged)
        {
            idleDelayTimer = 0f;
        }
        else if (hasInput && !positionChanged)
        {
            idleDelayTimer += Time.deltaTime;
        }
        else
        {
            idleDelayTimer = idleDelayThreshold;
        }

        bool isActuallyMoving = idleDelayTimer < idleDelayThreshold;

        if (isActuallyMoving)
        {
            if (!footstepSource.isPlaying)
            {
                footstepSource.clip = footstepClip;
                footstepSource.loop = true;
                footstepSource.Play();
            }
        }
        else
        {
            if (footstepSource.isPlaying)
            {
                footstepSource.Stop();
            }
        }

        animator.SetBool("IsMoving", isActuallyMoving);
        animator.SetFloat("MoveX", lastHorizontal);
    }

    void FixedUpdate()
    {
        if (isDead) return;

        Vector2 newPosition = rb.position + currentInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    void Shoot()
    {
        Debug.Log("Tiro disparado!");
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector2 shootDirection = (mouseWorldPosition - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.direction = shootDirection;
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        animator.SetTrigger("Die");
        this.enabled = false;

        if (tombstoneSprite != null)
            tombstoneSprite.SetActive(true);

        if (bodyCollider != null)
        {
            bodyCollider.enabled = false;
            gameObject.AddComponent<BoxCollider2D>();
        }

        rb.bodyType = RigidbodyType2D.Static;

        Invoke("GoToGameOverScene", 2f);

        GameObject.FindFirstObjectByType<GameTimer>()?.StopTimer();
    }

    void GoToGameOverScene()
    {
        SceneManager.LoadScene("Game Over");
    }
}