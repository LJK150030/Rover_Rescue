using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    RobotCommandManager commandScript;
    public RobotCommands currentCommand;

    public PointOfInterestManager currentPoint;

    public GameObject cameraManager;
    CameraManager cameraScript;

    public GameObject levelManager;
    LevelManager levelScript;
    LoadSceneOnClick sceneLoadScript;

    public GameObject uiManager;
    UIManager uiScript;
    public GameObject standbyOverlay;

    public Vector3 startLocation;

    public float robotSpeed = 1.0F;
    public float turnSpeed = 1.0F;
    public float commandDelay = 0.05F;

    bool isRobotRunning = false;
    bool isRotationDone = true;

    public AudioClip startMovementClip, stopMovementClip, currentMovementClip;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        levelScript = levelManager.GetComponent<LevelManager>();
        sceneLoadScript = levelManager.GetComponent<LoadSceneOnClick>();
        cameraScript = cameraManager.GetComponent<CameraManager>();
        uiScript = uiManager.GetComponent<UIManager>();

		startLocation = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Pressing the enter key activates the robot and executes its commmands
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ActivateRobot();
        }

        if (isRobotRunning && isRotationDone == true)
        {
            Debug.Log("choosing command");
            switch (currentCommand)
            {
                case RobotCommands.NORTH:
                    if(currentPoint.northPoint)
                    {
                        RotateRobot(currentPoint.northPoint);
                        
                        transform.position = Vector3.MoveTowards(transform.position, currentPoint.northPoint.transform.position, robotSpeed * Time.deltaTime);
                    }
                    break;
                case RobotCommands.SOUTH:
                    if (currentPoint.southPoint)
                    {
                        RotateRobot(currentPoint.southPoint);

                        transform.position = Vector3.MoveTowards(transform.position, currentPoint.southPoint.transform.position, robotSpeed * Time.deltaTime);
                    }
                    break;
                case RobotCommands.EAST:
                    if(currentPoint.eastPoint)
                    {
                        RotateRobot(currentPoint.eastPoint);

                        transform.position = Vector3.MoveTowards(transform.position, currentPoint.eastPoint.transform.position, robotSpeed * Time.deltaTime);
                    }
                    break;
                case RobotCommands.WEST:
                    if(currentPoint.westPoint)
                    {
                        RotateRobot(currentPoint.westPoint);

                        transform.position = Vector3.MoveTowards(transform.position, currentPoint.westPoint.transform.position, robotSpeed * Time.deltaTime);
                        
                    }
                    break;
                case RobotCommands.SCAN:
                    isRotationDone = false;
                    StartCoroutine(ScanCommand(turnSpeed));
                    break;
                case RobotCommands.NONE:
                    Debug.Log("none");
                    DoDeath();
                    break;
            }
        }
    }

    void ActivateRobot()
    {
        //cameraManager.GetComponent<CameraManager>().SwitchCameras();
        isRobotRunning = true;
        commandScript = gameObject.GetComponent<RobotCommandManager>();
        currentCommand = commandScript.commandList[0];
        standbyOverlay.SetActive(false);

        if (audioSource.clip)
            audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("CollisionDetected");
        if (other.gameObject.tag == "PointOfInterest" && other.gameObject != currentPoint.gameObject)
        {
            if (other.gameObject.GetComponent<PointOfInterestManager>().isWin)
            {
                Debug.Log("You win");
                sceneLoadScript.LoadByIndex(4);
            }

            currentPoint = other.GetComponent<PointOfInterestManager>();

            StartCoroutine("CommandAfterDelay");
        }
        else if (other.gameObject.tag == "Hazard")
            DoDeath();
    }

    IEnumerator CommandAfterDelay()
    {
        Debug.Log("CommandAfterDelay");
        yield return new WaitForSeconds(commandDelay);

        if (currentPoint)
        {
            Debug.Log("getting command");
            currentCommand = commandScript.GetCommand();
        }
    }

    void RotateRobot(Transform target)
    {
        Vector3 targetDir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(targetDir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0F, rotation.y, 0F);
    }

    IEnumerator ScanCommand(float duration)
    {
        float startRotation = transform.eulerAngles.y;
        float t = 0.0F;

        while (t <= duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(0.0F, 360.0F, t / duration);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, startRotation + yRotation, 
                transform.eulerAngles.z);

            if (yRotation >= 360.0F)
            {
                break;
            }
            else
            {
                yield return null;
            }
        }
        
        isRotationDone = true;
        currentCommand = commandScript.GetCommand();
    }

    public void DoDeath()
    {
        Debug.Log("Robot has died");
        transform.position = startLocation;
        isRobotRunning = false;

        commandScript.ResetCommandList();
		commandScript.ResetIndices ();

        uiScript.ResetCommands();
        //cameraScript.SwitchCameras();
        standbyOverlay.SetActive(true);

        // If the current level is not the tutorial, decrement remaining robots
        if(levelScript.remainingRobots != -1)
            levelScript.remainingRobots--;
    }
}
