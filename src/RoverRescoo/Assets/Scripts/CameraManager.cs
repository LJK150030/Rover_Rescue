using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera overheadCam;
    public Camera robotCam;

	// Use this for initialization
	void Start ()
    {
        //overheadCam.enabled = true;
        robotCam.enabled = true;
	}
	
    public void SwitchCameras()
    {
        //overheadCam.enabled = !overheadCam.enabled;
        //robotCam.enabled = !robotCam.enabled;
    }
}
