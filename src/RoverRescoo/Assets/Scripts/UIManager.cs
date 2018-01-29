using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Sprite NorthArrow, SouthArrow, EastArrow, WestArrow, Scan, Waiting, curSprite;

	public LevelManager lvlMan;
	int numRobots = 0;

	public Image[] uiImages; 
	int curImg = 0;

	public Text remainingRobots;


	void Start(){
		ResetCommands ();
	}

	//change Sprites basesd on input

	public void InputCommand(int passedInInt){
		switch (passedInInt) {

		//North
		case 1:
			if (IncrimentCurImg (1)) {
				uiImages [curImg].sprite = NorthArrow;
				if (curImg + 1 < 20) {
					curImg++;
				}
			}
			break;

		//South
		case 2:
			if (IncrimentCurImg (1)) {
				uiImages [curImg].sprite = SouthArrow;
				if (curImg + 1 < 20) {
					curImg++;
				}
			}
			break;
		
		//East
		case 3:
			if (IncrimentCurImg (1)) {
				uiImages [curImg].sprite = EastArrow;
				if (curImg + 1 < 20) {
					curImg++;
				}
			}
			break;

		//West
		case 4:
			if (IncrimentCurImg (1)) {
				uiImages [curImg].sprite = WestArrow;
				if (curImg + 1 < 20) {
					curImg++;
				}
			}
			break;

		//Scan
		case 5:
			if (IncrimentCurImg (1)) {
				uiImages [curImg].sprite = Scan;
				if (curImg + 1 < 20) {
					curImg++;
				}
			}
			break;
		
		//BackSpace
		case 6:
			if (IncrimentCurImg (-1)) {
				uiImages [curImg].sprite = Waiting;
				if (curImg - 1 > -1) {
					curImg--;
				}
			}
			break;
		}
	}



	bool IncrimentCurImg(int incriment){

		if (incriment < 0) {
			uiImages [curImg - 1].sprite = Waiting;

			if (curImg < 20 || curImg > -1) {
				uiImages [curImg].gameObject.GetComponent<CanvasGroup> ().alpha = 0;
			}
		} else if (curImg + incriment < 20) {
			uiImages [curImg + 1].gameObject.GetComponent<CanvasGroup> ().alpha = 1;
		}

		bool isOk;
		//check to see if you are outside the image array
		if (curImg + incriment < 20 || curImg + incriment > -1) {
			isOk = true;
		} else {
			Debug.Log ("No more Commands");
			isOk = false;
		}

		Debug.Log (curImg);

		return isOk;
	}


	public void ResetCommands(){
		foreach (Image img in uiImages){
			img.gameObject.GetComponent<CanvasGroup> ().alpha = 0;
			img.sprite = Waiting;
		}
	
		uiImages [0].gameObject.GetComponent<CanvasGroup> ().alpha = 1;
		curImg = 0;

		Debug.Log (lvlMan.remainingRobots);

		numRobots = lvlMan.remainingRobots;

		remainingRobots.text = "" + numRobots;


	}
}
