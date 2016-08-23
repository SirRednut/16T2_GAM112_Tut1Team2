using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UpsideDownKill : MonoBehaviour {
    public float timeRemaining;
    public float timeUntilDeath = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStayEnter(Collider other)
    {
        // Start the timer
        if (other.gameObject.tag == "env")
        {
            timeRemaining = timeUntilDeath;
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Manage the timer;
        if(other.gameObject.tag == "env")
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
                SceneManager.LoadScene("End");
        }
    }
}
