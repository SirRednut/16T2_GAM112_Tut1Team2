using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public float timerStartValue;
    public float currentTime;

    public Text spedometer;
    public Text timer;

    public Canvas pauseMenuCanvas;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        currentTime = timerStartValue;
	}
	
	// Update is called once per frame
	void Update ()
    {
        LevelTimer();
	}

    public void LevelTimer()
    {
        currentTime -= Time.deltaTime;

        timer.text = currentTime.ToString();

        if (currentTime <= 0)
            LoseGame();
    }

    public void LoseGame()
    {
        PlayerPrefs.SetInt("Won", 0);
        SceneManager.LoadScene("End");
    }

    public void WinGame()
    {
        PlayerPrefs.SetInt("Won", 1);
        PlayerPrefs.SetFloat("EndTime", currentTime);
        SceneManager.LoadScene("End");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Instantiate(pauseMenuCanvas);
    }
}
