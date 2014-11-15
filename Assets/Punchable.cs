using UnityEngine;
using System.Collections;

public class Punchable : MonoBehaviour {

    private Animator animator;

    public int damage = 0;
    public int threshold;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        damage++;
        Debug.Log("I was hit");
        if (damage >= threshold)
        {
            Destroy(this.gameObject.collider2D);
        }
    }

}
