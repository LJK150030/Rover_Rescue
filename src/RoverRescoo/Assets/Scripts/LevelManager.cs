using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int remainingRobots = 10;


	void OnLevelWasLoaded(){
		Debug.Log ("Scene Loaded");
	}
}
