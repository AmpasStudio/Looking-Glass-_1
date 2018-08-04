using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class SwipeDetector : MonoBehaviour {
	public GameObject gelas;
	private Vector3 offset;
	public float minSwipeDistY;
	public float minSwipeDistX;
	
	private Vector2 startPos;
	public int kondisiArrow;
	private int topbot;
	private int riglef;

	public AudioSource audioSwing;

	void Start(){
		
	}

	void Update(){
		if (kondisiArrow == 1) {
			gelas.transform.rotation = Quaternion.Slerp (gelas.transform.rotation, Quaternion.Euler (topbot, 0, 0), 2 * Time.deltaTime);
		} else if (kondisiArrow == 2) {
			gelas.transform.rotation = Quaternion.Slerp (gelas.transform.rotation, Quaternion.Euler (topbot, 0, 0), 2 * Time.deltaTime);
		} else if (kondisiArrow == 3) {
			gelas.transform.rotation = Quaternion.Slerp (gelas.transform.rotation, Quaternion.Euler (0, riglef, 0), 2 * Time.deltaTime);
		} else if (kondisiArrow == 4) {
			gelas.transform.rotation = Quaternion.Slerp (gelas.transform.rotation, Quaternion.Euler (0, riglef, 0), 2 * Time.deltaTime);
		} else {
			gelas.transform.rotation = Quaternion.Slerp (gelas.transform.rotation, Quaternion.Euler (0, 0, 0), 2 * Time.deltaTime);
		}
		if (Input.touchCount > 0) {
			Debug.Log (Input.touchCount);
			Touch touch = Input.touches[0];
			switch (touch.phase) {
			case TouchPhase.Began:
				startPos = touch.position;
				break;
			case TouchPhase.Ended:
				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
				if (swipeDistVertical > minSwipeDistY) {
					float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
					if (swipeValue > 0) {
						Debug.Log("Up Swipe");
						GetComponent<GameController_2>().matchArrow(1);
						kondisiArrow = 1;
						topbot += 150;
						audioSwing.Play ();
					}else if (swipeValue < 0){
						Debug.Log("Down Swipe");
						GetComponent<GameController_2>().matchArrow(3);
						kondisiArrow = 2;
						topbot -= 150;
						audioSwing.Play ();

					}
					Debug.Log (swipeValue);
				}
					
				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
				if (swipeDistHorizontal > minSwipeDistX) {
					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
					if (swipeValue > 0){
						Debug.Log("Right Swipe");
						GetComponent<GameController_2>().matchArrow(2);
						kondisiArrow = 3;
						riglef += 150;
						audioSwing.Play ();

					}//right swipe
					
					else if (swipeValue < 0){
						Debug.Log("Left Swipe");
						GetComponent<GameController_2>().matchArrow(4);
						kondisiArrow = 4;
						riglef -= 150;
						audioSwing.Play ();

					}
					Debug.Log (swipeValue);
				}
			break;
		}
	}
}
}
