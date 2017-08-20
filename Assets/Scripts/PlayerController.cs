using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }

    [Header("Movement")]
    public float speed = 5.0f;
    public float jumpSpeed = 8.0f;

    Vector3 moveDir = Vector3.zero;
    Rigidbody rb;

    [Header("Misc")]
    public Light backLight;
    public Material backdrop;

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
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Lerp time scale down and adjust fixedDeltaTime to allow for physics to continue making 50 checks a second
        Time.timeScale = Mathf.Lerp(Time.timeScale, 0.05f, 0.2f);
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0); // Store movement value

        if (Input.GetButton("Horizontal")) // Set timescale to 1 when we start moving
            Time.timeScale = 1.0f;

        if (Input.GetButtonDown("Jump")) // Add force upwards when you jump and set the timescale to 1
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
            GetComponent<Animator>().Play("player_jump");
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f;
        }

        // Lerp background colour and light intensity based on time scale
        float colourVal = Mathf.Clamp(Time.timeScale, 0.5f, 0.9f);
        backLight.intensity = 5 - (Time.timeScale * 5);
        backdrop.color = Color.
            Lerp(backdrop.color,
                new Vector4(colourVal, colourVal, colourVal, 1.0f),
                20.0f * Time.deltaTime);

        // Change PostFX profile when entering and exiting slow mo
        if (Time.timeScale == 1.0f)
            camera.GetComponent<CameraController>().SetSlowMoPostFX(false);
        else
            camera.GetComponent<CameraController>().SetSlowMoPostFX(true);

        rb.transform.Translate(moveDir * speed * Time.deltaTime); // Apply movement
    }
}