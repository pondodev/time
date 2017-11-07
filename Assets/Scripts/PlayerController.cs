using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }
    TimeController tc;
    CameraController cc;

    [Header("Movement")]
    public float speed = 5.0f;
    public float jumpSpeed = 8.0f;

    float moveDir = 0.0f;
    bool jumping;
    Rigidbody rb;

    [Header("Misc")]
    public Light backLight;

    [HideInInspector] public Vector3 spawnPoint;
    
    Camera camera;
    AudioSource music;
    AudioHighPassFilter highPassFilter;
    AudioReverbFilter reverb;

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
        cc = CameraController.instance;
        music = camera.GetComponent<AudioSource>();
        highPassFilter = camera.GetComponent<AudioHighPassFilter>();
        reverb = camera.GetComponent<AudioReverbFilter>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveDir = Input.GetAxis("Horizontal"); // Store movement value

        if (Input.GetButton("Horizontal")) // Set timescale to 1 when we start moving
        {
            tc.SetTimeScale(1.0f);
            music.volume = Mathf.Lerp(music.volume, 1, 0.01f);
        }
        else
        {
            music.volume = Mathf.Lerp(music.volume, 0.3f, 0.01f);
        }

        if (Input.GetButtonDown("Horizontal"))
            cc.SetSlowMoPostFX(false);
        else if (Input.GetButtonUp("Horizontal"))
            cc.SetSlowMoPostFX(true);

        if (Input.GetButtonDown("Jump")) // Set timescale to 1 and start jump animation
        {
            GetComponent<Animator>().Play("player_jump");
            tc.SetTimeScale(1.0f);
            jumping = true;
        }

        backLight.intensity = 5 - (Time.timeScale * 5);
        //music.volume = Mathf.Clamp(Time.timeScale, 0.3f, 1.0f);
        if (Time.timeScale < 1.0f)
        {
            highPassFilter.enabled = true;
            reverb.enabled = true;
        }
        else
        {
            highPassFilter.enabled = false;
            reverb.enabled = false;
        }
    }

    // Used for all physics related calculations
    void FixedUpdate ()
    {
        if (jumping) // Add force upwards when jumping
        {
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, 0.0f);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
            jumping = false;
        }

        rb.velocity = new Vector3(moveDir * speed * Time.deltaTime, rb.velocity.y, 0.0f); // Apply movement
    }

    public void Respawn ()
    {
        cc.SetBGColour(Color.red);
        transform.position = spawnPoint;
        rb.velocity = Vector3.zero;
    }
}