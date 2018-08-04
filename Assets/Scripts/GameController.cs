using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public Animator ancangAncangGO;
	public GameObject ulangPanel;
	public GameObject exitPanel;
	public GameObject[] arrow;

	public int arrowYangDipakai;//jenis arrow
	public float kecepatanArrow;//kecepatan arrow

	private int hasilRandom;
	private int jumlahRandom;
	public bool turn;
	public int banyakArrow;//jumlah arrow
	public Text console;
	public int[] stackArrow;
	private int iStack;
	private int iMatch;

	private Score score;
	private bool benar;
	public AudioSource getGold;
	public AudioSource arrowBlink;

	public AudioSource soundAncangAncang;
	public Sprite[] kataBenar;
	public AudioSource[] soundBenar;
	public Sprite[] kataSalah;
	public AudioSource[] soundSalah;
	public SpriteRenderer katakata;

	private int i = 1;
	private int j = 1;
	private int k = 1;
	// Use this for initialization
	void Start () {
		katakata = katakata.GetComponent<SpriteRenderer> ();
		ancangAncangGO = GetComponent<Animator> ();
		benar = false;
		iMatch = 0;
		iStack = 0;

		jumlahRandom = 0;
		turn = false;
		stackArrow = new int[10];
		score = GetComponent<Score> ();

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			exitPanel.SetActive (true);
		}
	}

	public void mulaiPertama(){
		StartCoroutine (ancangAncang ());
		StartCoroutine (RandomArrow ());
	}

	IEnumerator ancangAncang(){
		yield return new WaitForSeconds (1);
		ancangAncangGO.Play ("ancangAncang");
		soundAncangAncang.Play ();
	}

	IEnumerator acakMenang(){
		hasilRandom = Random.Range (0, 5);
		katakata.sprite = kataBenar [hasilRandom];
		soundBenar [hasilRandom].Play ();
		yield return new WaitForSeconds (2);
		katakata.sprite = null;
	}

	IEnumerator acakKalah(){
		hasilRandom = Random.Range (0, 5);
		katakata.sprite = kataSalah [hasilRandom];
		soundSalah [hasilRandom].Play ();
		yield return new WaitForSeconds (2);
		katakata.sprite = null;
	}

	public void Exit(){
		Application.Quit ();
	}

	void printStackArrow(){
		for (int i = 0; i < iStack; i++) {
			Debug.Log (stackArrow[i]);
		}
		stackArrow = new int[10];
		iStack = 0;
	}
		

	public void matchArrow(int angka){
		if (iMatch < iStack) {
			if (stackArrow [iMatch] == angka) {
				iMatch++;
				benar = true;
				score.AddScore (10);

			} else {
				//saat salah swipe
				
				turn = false;
				iMatch = 0;
				iStack = 0;
				//score.AddScore (-20);
				benar = false;
				ulangPanel.SetActive (true);
				resetLevel ();
				StartCoroutine (acakKalah());
			}
		}

		if (iMatch == iStack&&benar) {
			//saat benar swipe satu rounde
			
			turn = false;
			iMatch = 0;
			iStack = 0;
			score.AddScore (100);
			benar = false;
			StartCoroutine (RandomArrow());
			score.addCoin (1);
			gantiLevel ();
			getGold.Play ();

		}
	}

	private void AddStackArrow(int angka){
		stackArrow [iStack] = angka;
		iStack++;
	}
	public void ulangGame(){
		StartCoroutine(RandomArrow ());
		score.updateHS ();
		score.resetScore ();
	}

	public void keluarkeMainMenu(){
		SceneManager.LoadScene ("MainMenu");

	}

	IEnumerator RandomArrow(){
		yield return new WaitForSeconds (5.5f);
		while (jumlahRandom != banyakArrow) {
			hasilRandom = Random.Range (0, arrowYangDipakai);
			arrowBlink.Play();
			if (hasilRandom == 0) {
				arrowAtas ();
				AddStackArrow (hasilRandom);
			} else if (hasilRandom == 1) {
				arrowKanan ();
				AddStackArrow (hasilRandom);
			} else if (hasilRandom == 2) {
				arrowBawah ();
				AddStackArrow (hasilRandom);
			} else if (hasilRandom == 3) {
				arrowKiri ();
				AddStackArrow (hasilRandom);
			}
			yield return new WaitForSeconds (kecepatanArrow);
			HideAllArrow ();
			yield return new WaitForSeconds (0.3f);
			jumlahRandom += 1;
		}
		jumlahRandom = 0;
		turn = true;
		
	}

	void arrowAtas(){
		arrow [0].SetActive (true);
		arrow [1].SetActive (false);
		arrow [2].SetActive (false);
		arrow [3].SetActive (false);
	}

	void arrowKanan(){
		arrow [0].SetActive (false);
		arrow [1].SetActive (true);
		arrow [2].SetActive (false);
		arrow [3].SetActive (false);

	}

	void arrowBawah(){
		arrow [0].SetActive (false);
		arrow [1].SetActive (false);
		arrow [2].SetActive (true);
		arrow [3].SetActive (false);

	}

	void arrowKiri(){
		arrow [0].SetActive (false);
		arrow [1].SetActive (false);
		arrow [2].SetActive (false);
		arrow [3].SetActive (true);

	}

	void HideAllArrow(){
		arrow [0].SetActive (false);
		arrow [1].SetActive (false);
		arrow [2].SetActive (false);
		arrow [3].SetActive (false);
	}

	void OnDestroy(){
		score.resetScore ();
	}
	void resetLevel(){
		i=1;
		j=1;
		k=1;
		arrowYangDipakai = 2; //jenis arrow
		kecepatanArrow = 2f; //kecepatan arrow
		banyakArrow = 2; //jumlah arrow 
	}
	void gantiLevel(){
		//arrowYangDipakai; jenis arrow
		//kecepatanArrow; kecepatan arrow
		//banyakArrow; jumlah arrow

		if (j > 5) {
			if(arrowYangDipakai!=4)
				arrowYangDipakai+=1;
			j = 0;
			StartCoroutine (acakMenang());
		}
		if (i > 3) {
			if(banyakArrow!=10)
			banyakArrow+=1;
			i = 0;
		}
		if (k > 11) {
			if (kecepatanArrow > 1.0f) {
				kecepatanArrow -= 0.2f;
			}
			k = 0;
		}
		i++;
		j++;
		k++;
	}

	public void coba(){
		StartCoroutine (acakKalah());
	}

	public void coba2(){
		StartCoroutine (acakMenang());
	}
}
