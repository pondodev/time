using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public RectTransform[] menuOptions;
	public RectTransform selector, title;
	public Slider loadingBar;

	int menuSelection;
	bool loading;
	RectTransform loadingBarTransform;

	void Start ()
	{
		loadingBarTransform = loadingBar.gameObject.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!loading)
		{
            if (Input.GetKeyDown(KeyCode.W))
                ChangeSelection(-1);
		    else if (Input.GetKeyDown(KeyCode.S))
		        ChangeSelection(1);
    
		    if (Input.GetKeyDown(KeyCode.Return))
		    {
				switch (menuSelection)
				{
					case 0:
		    	        loading = true;
				        selector.gameObject.GetComponent<AudioSource>().Play();
		                StartCoroutine(LoadLevel());
						break;

					case 1:
					    Application.Quit();
						break;
				}
		    }
		}
		else
		{
			loadingBarTransform.anchoredPosition =
			    Vector2.Lerp(loadingBarTransform.anchoredPosition, Vector2.zero, 0.1f);
			title.anchoredPosition = Vector2
			    .Lerp(title.anchoredPosition, new Vector2(-200, title.anchoredPosition.y), 0.1f);
            foreach (RectTransform i in menuOptions)
			{
                i.anchoredPosition = new Vector2(Mathf.Lerp(i.anchoredPosition.x, 600, 0.1f), i.anchoredPosition.y);
			}
		}
		
		selector.anchoredPosition = Vector3.Lerp(selector.anchoredPosition, menuOptions[menuSelection].anchoredPosition, 0.1f);
	}

	void ChangeSelection (int modifier)
	{
		selector.gameObject.GetComponent<AudioSource>().Play();
		menuSelection = Mathf.Clamp(menuSelection + modifier, 0, menuOptions.Length - 1);
	}

	IEnumerator LoadLevel ()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync("test");
		yield return new WaitForSeconds(5);
		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			loadingBar.value = progress;
		    yield return null;
		}
	}
}
