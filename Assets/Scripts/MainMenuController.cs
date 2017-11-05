using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

		if (Input.GetKeyDown(KeyCode.Return))
		    StartCoroutine(LoadLevel());
	}

	IEnumerator LoadLevel ()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync("test");
		while (!operation.isDone)
		    yield return null;
	}
}
