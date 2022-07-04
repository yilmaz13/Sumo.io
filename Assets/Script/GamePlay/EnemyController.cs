using UnityEngine;
using UnityEngine.AI;

public class EnemyController : SumoController
{
    public Transform target;
    public NavMeshAgent agent;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
        PushMultiplier = 1.0f;
        ForceConstant = 10;
        agent = GetComponent<NavMeshAgent>();
        anim.SetBool("Moving", false);
        InvokeRepeating(nameof(RunDust), 0, 0.4f);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameActive)
            return;

        Drag(); // Controls whether the sumo is dragged
        NearestEnemy();
        if (agent.enabled && Moveable)
        {
            Navigate();
        }
    }

    protected override void Drag()
    {
        base.Drag();
        if (IsDrag)
        {
            Moveable = false;
            agent.isStopped = true;
        }
        else
        {
            Moveable = true;
            agent.isStopped = false;
        }
    }

    public void Navigate()
    {

        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
        anim.SetBool("Moving", true);
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    gameObject.transform.LookAt(target.transform.position);
                    agent.isStopped = true;
                }
            }
        }
    }

    private void NearestEnemy()
    {
        target = LevelManager.Instance.NearestEnemy(transform);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("DeathArea"))
        {
            if (IsDrag)
            {
                //player add score
                LevelManager.Instance.AddScore(150);
            }
            IsDead = true;
            agent.enabled = false;
            gameObject.SetActive(false);
        }
    }
}