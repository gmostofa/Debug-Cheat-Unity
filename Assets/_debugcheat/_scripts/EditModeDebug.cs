using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class EditModeDebug : MonoBehaviour
{
    public CommandScriptableObject commandScriptableObject;
    public List<CommandEvent> eventCommandList;

    public static EditModeDebug Instance;
    
    private void OnEnable()
    {
        CheckEvent();
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CheckEvent()
    {
        if (!Application.isPlaying)
        {
            if (commandScriptableObject == null)
            {
                commandScriptableObject = Resources.Load<CommandScriptableObject>("DebugCheatData");
                if (commandScriptableObject == null)
                {
                    Debug.LogWarning("Create Debug Cheat Code Assets -> Create -> ScriptableObject -> DebugCheat ---> Keep this to Resources Folder!");
                }
                else
                {
                    Debug.LogWarning("Debug Cheat Enabled on Game!");
                }
            }

            Debug.Log("EXECUTING ON EDIT MODE!! ----- ");
            if (eventCommandList.Count == 0)
            {
                eventCommandList = new List<CommandEvent>();
            }

            for (int i = 0; i < commandScriptableObject.commandList.Count; i++)
            {
                if (eventCommandList.Count == 0)
                {
                    CommandEvent commandEvent = new CommandEvent();
                    commandEvent.commandFormat = commandScriptableObject.commandList[i].commandFormat;
                    eventCommandList.Add(commandEvent);
                }
                else
                {
                    if(eventCommandList.Count != commandScriptableObject.commandList.Count)
                    {
                        string command_format = commandScriptableObject.commandList[i].commandFormat;
                        int ind = -1;
                        for (int j = 0; j < eventCommandList.Count; j++)
                        {
                            Debug.Log(command_format);
                            Debug.Log(eventCommandList[j].commandFormat);
                            if (!command_format.Equals(eventCommandList[j].commandFormat))
                            {
                                Debug.Log("Entered here");
                                ind = i;
                            }
                        }
                        CommandEvent commandEvent = new CommandEvent();
                        commandEvent.commandFormat = commandScriptableObject.commandList[ind].commandFormat;
                        eventCommandList.Add(commandEvent);
                    }
                }
            }
        }
    }

    public void AddEventAfterDataCheck()
    {
        int prevEvent = eventCommandList.Count;
        int curEvent = commandScriptableObject.commandList.Count;

        int diff = Mathf.Abs(curEvent - prevEvent);
        
        Debug.Log(diff);
        for (int i = prevEvent; i < curEvent; i++)
        {
            CommandEvent commandEvent = new CommandEvent();
            commandEvent.commandFormat = commandScriptableObject.commandList[i].commandFormat;
            eventCommandList.Add(commandEvent);
        }
    }
}
