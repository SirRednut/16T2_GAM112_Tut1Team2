using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject car;
    public float offsetX;
    public float offsetY;

	// Use this for initialization
	void Start () {
        offsetY = car.transform.position.y - transform.position.y;
        offsetY = car.transform.position.y - transform.position.y;

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(car.transform.position.x + offsetX, car.transform.position.y - offsetY, transform.position.z);
	}
}
