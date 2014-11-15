using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

    private Animator animator;
    private Vector2 velocity;

    public PlayerController player;
    public float speed = 4.5f;
    public int health = 100;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0)
        {
            animator.SetInteger("Health", 0);
            Destroy(this);
        }
	}

    void FixedUpdate()
    {
        float dist = (transform.position - player.transform.position).magnitude;
        if (dist < 15)
        {
            RotateToPoint(player.transform.position);
            transform.rigidbody2D.velocity = transform.up * speed;
            animator.SetFloat("DistanceToPlayer", dist);
            if (dist < 0.5f)
            {
                if (!IsInvoking("Punch"))
                {
                    InvokeRepeating("Punch", 0f, 3f);
                }
            }
        }
        else
        {
            transform.rigidbody2D.velocity = new Vector2 (0,0);
        }
    }

    private void Punch()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void RotateToPoint(Vector3 target)
    {
        Vector2 targetviewport = target;
        Vector2 reltarget = new Vector2(targetviewport.x - 0.5f, targetviewport.y - 0.5f);
        Vector2 relprisoner = new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f) - reltarget;
        float angle = Vector2.Angle(Vector2.up, relprisoner);
        if (relprisoner.x > 0)
        {
            angle = 360 - angle;
        }
        angle += 180;
        Quaternion quat = Quaternion.identity;
        quat.eulerAngles = new Vector3(0, 0, angle);

        transform.rotation = quat;
    }
}
