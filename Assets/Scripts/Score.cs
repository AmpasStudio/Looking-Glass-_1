using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	private int score = 0;
	private int coin = 0;
	private int highscore = 0;

	public Text textScore;
	public Text HS;
	public Text textCoin;
	// Use this for initialization
	void Start () {
		textCoin.text = "coin : " + PlayerPrefs.GetInt ("coins");
		HS.text = "HighScore : "+ PlayerPrefs.GetInt ("highestScore");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddScore(int angka){
		if (angka < 0 && score < 1) {
		
		} else {
			score += angka;
		}
		textScore.text = "score : " + score;
	}

	public void resetScore(){
		score = 0;
		textScore.text = "score : " + score;
	}

	public void updateHS(){
		if (PlayerPrefs.GetInt ("highestScore") < score) {
			PlayerPrefs.SetInt ("highestScore", score);
			HS.text = "HighScore : "+highscore;
		}
	}

	public void addCoin(int angka){
		coin += angka;
		PlayerPrefs.SetInt ("coins", coin);
		textCoin.text = "coin : " + coin;
	}

	void OnDestroy(){
		PlayerPrefs.SetInt ("coins", coin);
		PlayerPrefs.SetInt ("highestScore", highscore);
	}
}
