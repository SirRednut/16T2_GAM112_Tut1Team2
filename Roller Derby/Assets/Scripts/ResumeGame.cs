using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResumeGame : MonoBehaviour {
    public Canvas pauseMenu;
    public void OnClick()
    {
        Time.timeScale = 1;
        Destroy(pauseMenu);
    }
}
