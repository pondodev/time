using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public Transform[] menuOptions;

	int menuSelection;
    
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.W))
            menuSelection = Mathf.Clamp(menuSelection - 1, 0, menuOptions.Length);
		else if (Input.GetKeyDown(KeyCode.S))
		    menuSelection = Mathf.Clamp(menuSelection + 1, 0, menuOptions.Length);

		transform.position = Vector3.Lerp(transform.position, menuOptions[menuSelection].position, 0.1f);
	}
}
