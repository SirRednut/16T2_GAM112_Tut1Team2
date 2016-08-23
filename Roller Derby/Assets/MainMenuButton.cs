using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuButton : MonoBehaviour {

	public void OnClick()
    {
        SceneManager.LoadScene("Main Menu");
    }
        
}
