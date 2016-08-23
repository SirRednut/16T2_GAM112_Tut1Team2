using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Controller : MonoBehaviour {

    public float moveSpeed;
    float maxSpeed = 10000;
    float minSpeed = -250;
    public float accSpeed;
    public Slider Speed;
    public GameObject chassis;
    public GameObject[] wheels;
    public Vector3 chassisPos = new Vector3();
    public float angle;
    public float tiltSpeed;
    float moveAmount;
    float prevPos = 0;
    private Rigidbody rb;

    /*
    Wheels[0]: Left Back
    Wheels[1]: Left Front
    Wheels[2]: Right Back
    Wheels[3]: Right Front
    */

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        prevPos = transform.position.x;
    }

    // Update is called once per frame
    void Update() {
        moveAmount = Mathf.Round(transform.position.x * 100) - Mathf.Round(prevPos * 100);
        //GameController.Instance.spedometer.text = moveAmount + "Km/s";
        //Debug.Log (moveAmount);
        moveSpeed += Speed.value * 100;
        moveSpeed = Mathf.Clamp(moveSpeed, minSpeed, maxSpeed);

        foreach (GameObject wheel in wheels) {
            if ((moveAmount > 0 && Speed.value < 0) || (moveAmount < 0 && Speed.value > 0)) {
                wheel.GetComponent<WheelCollider>().brakeTorque = maxSpeed * 10;
                moveSpeed = 0;
                wheel.GetComponent<WheelCollider>().motorTorque = moveSpeed;
            }
            else {
                wheel.GetComponent<WheelCollider>().motorTorque = moveSpeed;
                //Debug.Log("MoveSpeed: " + moveSpeed + " BrakeTorque: " + wheel.GetComponent<WheelCollider>().brakeTorque);
                if (wheel.GetComponent<WheelCollider>().brakeTorque > 0) {
                    wheel.GetComponent<WheelCollider>().brakeTorque -= maxSpeed;
                }
            }
        }
        prevPos = transform.position.x;

        // Rotation mechanics
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float rotationDelta = Input.GetAxis("Mouse ScrollWheel") * tiltSpeed;

            rb.AddTorque(new Vector3(0, 0, rotationDelta), ForceMode.Acceleration);
        }
    }

}
