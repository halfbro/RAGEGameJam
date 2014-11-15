using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public float startTime;
    public float time;
    public PlayerController player;
    private GUIText element;
    private bool showtime = false;

	// Use this for initialization
	void Start ()
    {
        element = GetComponent<GUIText>();
        element.text = "WASD to move, Click to punch + begin";
        Time.timeScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!showtime) Invoke("showTime", 0.1f);
        
        if (Input.GetButtonDown("Fire1"))
        {
            Time.timeScale = 1.0f;
        }
        time -= Time.deltaTime;
        element.text = time.ToString("N1");
        if (time <= 0)
        {
            time = 0;
            element.text = "You were Captured! Try Again";
            player.animator.SetInteger("Health", 0);
            player.speedbase = 0.0f;
            Invoke("startNewGame", 5.0f);
        }


        if (Input.GetKey(KeyCode.Space))
        {
            Invoke("startNewGame", 0.0f);
        }
	}

    private void startNewGame()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    private void showTime()
    {
        showtime = true;
    }
}
