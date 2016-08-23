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

    // Manage gearchange SFX
    private AudioSource engineSFX;
    public Animator engineAnimator;
    public float speedFactor;
    public int currentGear = 0;
    public float gear1 = 0.25f;
    public float gear2 = 0.5f;
    public float gear3 = 0.75f;
    public float gear4 = 1f;

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
        
        // Set up the gear values

        gear1 = 0.25f * maxSpeed;
        gear2 = 0.5f * maxSpeed;
        gear3 = 0.75f * maxSpeed;
        gear4 = 1f * maxSpeed;
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
        prevPos = transform.position.x;

        // Rotation mechanics
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float rotationDelta = Input.GetAxis("Mouse ScrollWheel") * tiltSpeed;

            rb.AddTorque(new Vector3(0, 0, rotationDelta), ForceMode.Acceleration);
        }

        ManageAudio();
    }

    void ManageAudio()
    {
        // Get the current speedFactor of the engine
        speedFactor = moveSpeed / maxSpeed;
        if(speedFactor > 0 && speedFactor < gear1)
        {
            if(currentGear != 1)
            {
                ChangeGear();
                currentGear = 1;
            }  
        }
        else if (speedFactor > gear1 && speedFactor < gear2)
        {
            if (currentGear != 2)
            {
                ChangeGear();
                currentGear = 2;
            }
        }
        else if (speedFactor > gear2 && speedFactor < gear3)
        {
            if (currentGear != 3)
            {
                ChangeGear();
                currentGear = 3;
            }
        }
        else if (speedFactor > gear3 && speedFactor < gear4)
        {
            if (currentGear != 4)
            {
                ChangeGear();
                currentGear = 4;
            }
        }
    }

    void ChangeGear()
    {
        engineAnimator.Play("gearchange");
        //change gear
    }
}
