using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

    int robotDeaths;

	public RobotMovement botControler;

	void Start ()
    {
        robotDeaths = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Hazard")
        {
            robotDeaths += 1;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			botControler.DoDeath();
        }
    }
}
