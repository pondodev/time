using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraController : MonoBehaviour
{
    public PostProcessingProfile[] postfxProfiles;

    GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = PlayerController.instance.gameObject;
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
            GetComponent<PostProcessingBehaviour>().profile = postfxProfiles[1];
        }
        else
        {
            GetComponent<PostProcessingBehaviour>().profile = postfxProfiles[0];
        }
    }
}
