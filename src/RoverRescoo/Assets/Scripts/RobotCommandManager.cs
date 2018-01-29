using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RobotCommands
{
    NORTH,
    SOUTH,
    EAST,
    WEST,
    SCAN,
    NONE
};

public class RobotCommandManager : MonoBehaviour
{
    public RobotCommands[] commandList = new RobotCommands[20];

    public GameObject uiManager;
    UIManager uiScript;

    int commandEntryIndex = 0, commandExecutionIndex = 0;

    void Start()
    {
        ResetCommandList();

        uiScript = uiManager.GetComponent<UIManager>();
    }

    public RobotCommands GetCommand()
    {
        if (commandExecutionIndex < commandList.Length - 1)
        {
            commandExecutionIndex++;
            return commandList[commandExecutionIndex];
        }
        else
        {
            return RobotCommands.NONE;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(commandEntryIndex < commandList.Length - 1)
                commandList[commandEntryIndex++] = RobotCommands.NORTH;

            if (uiScript)
                uiScript.InputCommand(1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (commandEntryIndex < commandList.Length - 1)
                commandList[commandEntryIndex++] = RobotCommands.SOUTH;

            if (uiScript)
                uiScript.InputCommand(2);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (commandEntryIndex < commandList.Length - 1)
                commandList[commandEntryIndex++] = RobotCommands.EAST;

            if (uiScript)
                uiScript.InputCommand(3);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (commandEntryIndex < commandList.Length - 1)
                commandList[commandEntryIndex++] = RobotCommands.WEST;

            if (uiScript)
                uiScript.InputCommand(4);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (commandEntryIndex < commandList.Length - 1)
                commandList[commandEntryIndex++] = RobotCommands.SCAN;

            if (uiScript)
                uiScript.InputCommand(5);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if(commandEntryIndex > 0)
                commandList[--commandEntryIndex] = RobotCommands.NONE;

            if (uiScript)
                uiScript.InputCommand(6);
        }
    }

    public void ResetCommandList()
    {
        //uiScript.ResetCommands();

        for (int i = 0; i < commandList.Length; i++)
        {
            commandList[i] = RobotCommands.NONE;
        }
    }

	public void ResetIndices()
	{
		Debug.Log ("Reset index");
		commandEntryIndex = 0;
		commandExecutionIndex = 0;
	}
}
