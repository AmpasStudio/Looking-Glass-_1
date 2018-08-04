using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public GameObject exitPanel;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			exitPanel.SetActive (true);
		}
	}
	public void playButton(){
		SceneManager.LoadScene ("PlayGame");
	}
	public void shopButton(){
		SceneManager.LoadScene ("Shop");
	}
	public void quitYes(){
		Application.Quit();
	}


}
