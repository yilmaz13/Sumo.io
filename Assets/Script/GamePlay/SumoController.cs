using UnityEngine;

public class SumoController : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public Animator anim;
    private float dragTimer;
    private float dragMaxTime = 0.5f;
    protected bool IsDrag;
    protected bool Moveable;
    protected AudioSource AudioSource;
    public AudioClip[] soundEffect;
    public float PushMultiplier { get; set; }
    protected float ForceConstant { get; set; }
    public bool IsDead { get; protected set; } = false;

    /// <summary>
    ///  If someone hits this object it will add force for dragging
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="pushMultiplier"></param>
    public void SumoAddForce(int direction, float pushMultiplier)
    {
        rigidbody.AddForce(transform.forward * (direction * pushMultiplier * ForceConstant), ForceMode.Impulse);
        IsDrag = true;
        anim.SetBool("Drag", true);
        //Particle effect is spawn from Object Pooler
        ObjectPooler.Instance.Spawn("Dust", transform.position, Quaternion.identity);
        PlayBoingSoundEffect(); // plays boing sound effect
    }

    /// <summary>
    /// If someone hits this object it will be dragged for a certain time
    /// </summary>
    protected virtual void Drag()
    {
        if (IsDrag)
        {
            dragTimer += Time.deltaTime;
            if (dragTimer >= dragMaxTime)
            {
                IsDrag = false;
                anim.SetBool("Drag", false);
                dragTimer = 0;
            }
        }
    }

    /// <summary>
    /// If sumo eats food, sumo grows
    /// </summary>
    /// <param name="growthConstant"></param>
    private void Growth(float growthConstant)
    {
        var scale = transform.localScale.x * growthConstant;
        transform.localScale = new Vector3(scale, scale, scale);
        PushMultiplier *= 1.2f;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            var growthConstant = other.gameObject.GetComponent<Food>().GrowthConstant;
            Growth(growthConstant);
            other.gameObject.SetActive(false);
            
            PlayEatSoundEffect(); // plays boing sound effect
        }
    }

    protected void PlayBoingSoundEffect()
    {
        AudioSource.PlayOneShot(soundEffect[0]);
    }
    protected void PlayEatSoundEffect()
    {
        AudioSource.PlayOneShot(soundEffect[1]);
    }
}