using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }
    TimeController tc;

    [Header("Movement")]
    public float speed = 5.0f;
    public float jumpSpeed = 8.0f;

    float moveDir = 0.0f;
    Rigidbody rb;

    [Header("Misc")]
    public Light backLight;

    [HideInInspector] public Vector3 spawnPoint;
    
    Camera camera;

    // Initialise singleton, throw error if there's more than one instance
    void Awake ()
    {
        if (instance != null)
            throw new System.Exception();

        instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
        spawnPoint = transform.position;
        tc = TimeController.instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveDir = Input.GetAxis("Horizontal"); // Store movement value

        if (Input.GetButton("Horizontal")) // Set timescale to 1 when we start moving
            tc.SetTimeScale(1.0f);

        if (Input.GetButtonDown("Jump")) // Set timescale to 1 and start jump animation
        {
            GetComponent<Animator>().Play("player_jump");
            tc.SetTimeScale(1.0f);
        }

        backLight.intensity = 5 - (Time.timeScale * 5);
    }

    // Used for all physics related calculations
    void FixedUpdate ()
    {
        if (Input.GetButtonDown("Jump")) // Add force upwards when jumping
        {
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, 0.0f);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        }

        rb.velocity = new Vector3(moveDir * speed * Time.deltaTime, rb.velocity.y, 0.0f); // Apply movement
    }
}