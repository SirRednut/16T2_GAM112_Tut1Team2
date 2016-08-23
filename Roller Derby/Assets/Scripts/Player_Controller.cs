using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Controller : MonoBehaviour {

    public Slider Speed;
    public Terrain terrain;
    public GameObject chassis;
    public GameObject[] wheels;

    public float moveSpeed;
    public float accSpeed;
    public Vector3 chassisPos = new Vector3();
    public float angle;

    float moveAmount;
    float prevPos = 0.0f;
    float maxSpeed = 10000.0f;
    float minSpeed = -250.0f;
    float SpeedValue = 0.0f;
    int WheelsOnGround = 0;
    public bool onGround = true;

    /*
    Wheels[0]: Left Back
    Wheels[1]: Left Front
    Wheels[2]: Right Back
    Wheels[3]: Right Front
    */

    // Use this for initialization
    void Start() {
        prevPos = transform.position.x;
    }

    // Update is called once per frame
    void Update() {
        // Movement

        // Find movement amount to get accurate movement direction
        moveAmount = Mathf.Round(transform.position.x * 100) - Mathf.Round(prevPos * 100);
        moveSpeed += Speed.value * 100;
        moveSpeed = Mathf.Clamp(moveSpeed, minSpeed, maxSpeed);
        //WheelsOnGround = 0;
        foreach (GameObject wheel in wheels) {
            if ((moveAmount > 0 && Speed.value < 0) || (moveAmount < 0 && Speed.value > 0)) 
            {
                wheel.GetComponent<WheelCollider>().brakeTorque = maxSpeed * 10;
                moveSpeed = 0;
                wheel.GetComponent<WheelCollider>().motorTorque = moveSpeed;
            }
            else {
                wheel.GetComponent<WheelCollider>().motorTorque = moveSpeed;
                if (wheel.GetComponent<WheelCollider>().brakeTorque > 0) 
                {
                    wheel.GetComponent<WheelCollider>().brakeTorque -= maxSpeed;
                }
            }



        }

        // Detect whether player is on ground
        if (wheels[0].GetComponent<WheelCollider>().isGrounded || wheels[1].GetComponent<WheelCollider>().isGrounded || wheels[2].GetComponent<WheelCollider>().isGrounded || wheels[3].GetComponent<WheelCollider>().isGrounded) 
        {
            onGround = true;
        }
        else 
        {
            onGround = false;
        }

        prevPos = transform.position.x;

        // Off Ground Tilt
        if (!onGround && Speed.minValue != -1) 
        {
            Speed.minValue = -1;
            SpeedValue = Speed.value;
            Speed.value = 0;
        }
        else if (Speed.minValue == -1 && onGround) 
        {
            Speed.minValue = -0.5f;
            Speed.value = SpeedValue;
        }

        if (!onGround) 
        {
            transform.Rotate(Vector3.right * Speed.value);
            /*
            float xRotation = transform.rotation.eulerAngles.x;

            if (transform.rotation.eulerAngles.x >= 180) {
                xRotation = Mathf.Clamp(xRotation, 270f, 365f);
            }
            else 
            {
                xRotation = Mathf.Clamp(xRotation, -0.5f, 90.0f);
            }
            transform.rotation = Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            Debug.Log(xRotation);
            */
        }

    }

}
