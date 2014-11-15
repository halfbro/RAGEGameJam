using UnityEngine;
using System.Collections;

public class GoToObject : MonoBehaviour {

    public Transform target;
    public float speed;
    public float offset;
	
	void Update ()
    {
        Vector2 rel = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        Vector2 mouseviewport = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 relmouse = new Vector2(mouseviewport.x - 0.5f, mouseviewport.y - 0.5f);
        rel = rel + relmouse * offset;

        transform.Translate(rel * speed * Time.deltaTime);
	}
}
