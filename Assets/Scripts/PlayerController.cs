using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Animator animator;
    private Vector2 velocity;
    private BoxCollider2D punchbox;
    private float speedmultiplier;

    private bool resting = false;
    private float sprintlimiter = 5.0f;

    public bool op = false;
    public float sprintmultiplier = 1.4f;
    public float speedbase = 1;
    public float punchingforce = 4;
    public int health = 100;
    public int damage = 30;

    public AudioClip punch;
    public AudioClip damaged;

    public Camera cam;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        punchbox = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        speedmultiplier = speedbase;
        if (Input.GetButton("Sprint")) 
        {
            if (sprintlimiter > 0.0f && !resting)
            {
                speedmultiplier = speedbase * sprintmultiplier;
                sprintlimiter -= Time.deltaTime;
                if (sprintlimiter < 0.1)
                {
                    resting = true;
                }
            }
        }
        else
        if (Input.GetButtonDown("Fire1"))
        { 
            animator.SetBool("PunchButton", true);
            punchbox.enabled = true;
            Invoke("DisableCollisionBox", 0.1f);
            AudioSource.PlayClipAtPoint(punch, transform.position);
        }
        else
        {
            animator.SetBool("PunchButton", false);
            sprintlimiter += Time.deltaTime;
            if (sprintlimiter > 3.0f)
            {
                resting = false;
            }
            if (sprintlimiter > 5.0f)
            {
                sprintlimiter = 5.0f;
            }
        }

        velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (velocity.magnitude > 1)
        {
            velocity.Normalize();
        }

        animator.SetFloat("Speed", speedmultiplier * velocity.magnitude);
	}

    void FixedUpdate()
    {
        RotateToMouse(speedmultiplier);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (op)
        {
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.name == "Guard")
        {
            Guard stats = coll.gameObject.GetComponent<Guard>();
            stats.health -= damage;
        }

        if (coll.gameObject.tag == "Door")
        {
            coll.gameObject.rigidbody2D.AddForce(this.gameObject.transform.rotation.eulerAngles * punchingforce);
        }
    }

    private void DisableCollisionBox()
    {
        punchbox.enabled = false;
    }

    private void RotateToMouse(float speedmult)
    {
        Vector2 mouseviewport = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 prisonerviewport = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 relmouse = new Vector2(mouseviewport.x - 0.5f, mouseviewport.y - 0.5f);
        Vector2 relprisoner = new Vector2(prisonerviewport.x - 0.5f, prisonerviewport.y - 0.5f) - relmouse;
        float angle = Vector2.Angle(Vector2.up, relprisoner);
        if (relprisoner.x > 0)
        {
            angle = 360 - angle;
        }
        angle += 180;
        Quaternion quat = Quaternion.identity;
        quat.eulerAngles = new Vector3(0, 0, angle);

        transform.rigidbody2D.velocity = velocity * speedmult;
        transform.rotation = quat;
    }
}
