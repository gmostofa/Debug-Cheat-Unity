using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[System.Serializable]
public struct CommandEvent
{
    public string commandFormat;
    public UnityEvent OnCommandExecute;
}

[System.Serializable]
public struct CommandStruct
{
    public string commandName;
    public string commandID;
    public string commandDescription; 
    public string commandFormat;
    public bool isCommandComplete;
}

[CreateAssetMenu(fileName = "DebugCheatData", menuName = "ScriptableObjects/DebugCheat", order = 1)]
public class CommandScriptableObject : ScriptableObject
{
    public List<CommandStruct> commandList;
    
    public void OnValidate() 
    {
        Debug.Log(commandList.Count);
        if (EditModeDebug.Instance != null)
        {
            for (int i = 0; i < commandList.Count; i++)
            {
                if (commandList[i].isCommandComplete)
                {
                    EditModeDebug.Instance.AddEventAfterDataCheck();
                }
            }
        }
    }
}
