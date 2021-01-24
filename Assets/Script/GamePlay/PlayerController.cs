using UnityEngine;

public class PlayerController : SumoController
{
    public InputManager inputManager;
    public float speed;
    public float gravity;
    private Vector3 moveDirection;
    public int score;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
        inputManager = InputManager.Instance;
        PushMultiplier = 1.0f;
        ForceConstant = 10;
        Moveable = true;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameActive)
            return;

        Drag(); // Controls whether the player is dragged
        if (Moveable)
        {
            Move();
        }
    }

    /// <summary>
    /// if Player is moveable, player moves
    /// </summary>
    private void Move()
    {
        Vector2 direction = InputManager.Instance.direction;
        moveDirection = new Vector3(direction.x, 0, direction.y);

        Quaternion targetRotation = moveDirection != Vector3.zero
            ? Quaternion.LookRotation(moveDirection)
            : transform.rotation;
        gameObject.transform.rotation = targetRotation;

        moveDirection = moveDirection * speed;
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        transform.position += moveDirection * Time.deltaTime;
    }

    /// <summary>
    /// Controls whether the player is dragged. If player is dragged, moveable is false
    /// </summary>
    protected override void Drag()
    {
        base.Drag();
        if (IsDrag)
            Moveable = false;
        else
            Moveable = true;
    }

    /// <summary>
    /// Sumo dies
    /// </summary>
    private void Die()
    {
        LevelManager.Instance.GameOver();
        anim.SetBool("Dead", true);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Food"))
        {
            LevelManager.Instance.AddScore(150);
        }

        if (other.gameObject.CompareTag("DeathArea"))
        {
            Die();
        }
    }
}