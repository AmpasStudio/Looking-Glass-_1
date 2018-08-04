using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController_2 : MonoBehaviour {
	private Score score;
	[SerializeField]
	private int arrowOn;

	[Header ("Panel Game")]
	public GameObject gameOverPanel;
	public GameObject exitPanel;

	[Header ("Variable Game")]
	public float timeDown;
	public float plusTime;
	public float minTime;
	public Slider sliderTime;
	public GameObject[] arrow;
	public GameObject grubArrow;
	void Start(){
		score = GetComponent<Score> ();
		arrowOn = 0;
		HideAllArrow();
		randomArrow();
		Time.timeScale = 1;
	}

	void Update(){
		//count down time in game
		sliderTime.value -= Time.deltaTime * timeDown;
		if(sliderTime.value == 0.0f){
			gameOverPanel.SetActive(true);
			Time.timeScale = 0;
			HideAllArrow();
		}
		//exit game panel
		if (Input.GetKeyDown(KeyCode.Escape)) {
			exitPanel.SetActive (true);
			Time.timeScale = 0;
			grubArrow.SetActive(false);
		}
	}

	public void retryGame(){
		Time.timeScale = 1;
		score.updateHS();
		score.resetScore();
		sliderTime.value = 1.0f;
		randomArrow();
	}

	public void resumeGame(){
		Time.timeScale = 1;
	}

	public void matchArrow(int arrow){
		if(arrow == arrowOn){
			score.AddScore(20);
			sliderTime.value += plusTime;
		}else{
			score.AddScore(-10);
			sliderTime.value -= minTime;
		}
		randomArrow();
	}

	void randomArrow(){
		int hasilRandom = Random.Range (0, arrow.Length);
		if (hasilRandom == 0) {
			arrowAtas ();
		} else if (hasilRandom == 1) {
			arrowKanan ();
		} else if (hasilRandom == 2) {
			arrowBawah ();
		} else if (hasilRandom == 3) {
			arrowKiri ();
		}
	}

	void arrowAtas(){
		arrow [0].SetActive (true);
		arrow [1].SetActive (false);
		arrow [2].SetActive (false);
		arrow [3].SetActive (false);
		arrowOn = 1;
	}

	void arrowKanan(){
		arrow [0].SetActive (false);
		arrow [1].SetActive (true);
		arrow [2].SetActive (false);
		arrow [3].SetActive (false);
		arrowOn = 2;
	}

	void arrowBawah(){
		arrow [0].SetActive (false);
		arrow [1].SetActive (false);
		arrow [2].SetActive (true);
		arrow [3].SetActive (false);
		arrowOn = 3;
	}

	void arrowKiri(){
		arrow [0].SetActive (false);
		arrow [1].SetActive (false);
		arrow [2].SetActive (false);
		arrow [3].SetActive (true);
		arrowOn = 4;
	}

	void HideAllArrow(){
		arrow [0].SetActive (false);
		arrow [1].SetActive (false);
		arrow [2].SetActive (false);
		arrow [3].SetActive (false);
		arrowOn = 0;
	}


}
