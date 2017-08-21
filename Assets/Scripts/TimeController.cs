using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController instance { get; private set; }

    public Material backdrop;

    Camera camera;

    // Initialise singleton, throw error if there's more than one instance
    void Awake()
    {
        if (instance != null)
            throw new System.Exception();

        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Lerp time scale down and adjust fixedDeltaTime to allow for physics to continue making 50 checks a second
        Time.timeScale = Mathf.Lerp(Time.timeScale, 0.05f, 0.2f);
        Time.fixedDeltaTime = Time.timeScale * 0.02f;


        // Change PostFX profile when entering and exiting slow mo
        if (Time.timeScale == 1.0f)
            camera.GetComponent<CameraController>().SetSlowMoPostFX(false);
        else
            camera.GetComponent<CameraController>().SetSlowMoPostFX(true);

        // Lerp background colour and light intensity based on time scale
        float colourVal = Mathf.Clamp(Time.timeScale, 0.5f, 0.9f);

        backdrop.color = Color.
            Lerp(backdrop.color,
                new Vector4(colourVal, colourVal, colourVal, 1.0f),
                20.0f * Time.deltaTime);
    }

    public void SetTimeScale (float val)
    {
        Time.timeScale = val;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
