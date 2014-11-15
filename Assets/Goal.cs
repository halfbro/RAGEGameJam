using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public PlayerController player;
    public Timer time;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name != "Prisoner") { return; }
        time.gameObject.guiText.text = "You Escaped!";
        Destroy(time);
        Time.timeScale = 0.15f;
        Invoke("startNew", 1.05f);
    }

    private void startNew()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }
}
