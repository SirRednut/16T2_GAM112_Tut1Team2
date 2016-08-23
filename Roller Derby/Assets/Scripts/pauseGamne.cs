using UnityEngine;
using System.Collections;

public class pauseGamne : MonoBehaviour {

    public void OnClick()
    {
        if (Time.timeScale != 0)
        {
            GameController.Instance.Pause();
        }
    }
}
