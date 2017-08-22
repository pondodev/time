using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraController : MonoBehaviour
{
    public static CameraController instance { get; private set; }

    public PostProcessingProfile[] postfxProfiles;

    GameObject player;
    PostProcessingBehaviour postFXBehaviour;

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
        player = PlayerController.instance.gameObject;
        postFXBehaviour = GetComponent<PostProcessingBehaviour>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Slerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10.0f), 0.1f);
	}

    public void SetSlowMoPostFX (bool toggle)
    {
        if (toggle)
        {
            postFXBehaviour.profile = postfxProfiles[1];
        }
        else
        {
            postFXBehaviour.profile = postfxProfiles[0];
        }
    }
}
