using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class DebugController : MonoBehaviour
{
#if UNITY_EDITOR
   [MenuItem("GameObject/GM/Debug Cheat GM")]
   public static void AddDebugController()
   {
      GameObject obj = Instantiate(Resources.Load<GameObject>("DebugController"));
      obj.transform.SetParent(null, true);
   }

#endif

   public bool showConsole;
   private string input;

   public CommandScriptableObject commandScriptableObject;

   public List<object> commandList;

   private void OnEnable()
   {
      if (commandScriptableObject == null)
      {
         commandScriptableObject = Resources.Load<CommandScriptableObject>("DebugCheatData");
         if (commandScriptableObject == null)
         {
            Debug.LogWarning(
               "Create Debug Cheat Code Assets -> Create -> ScriptableObject -> DebugCheat ---> Keep this to Resources Folder!");
         }
         else
         {
            Debug.LogWarning("Debug Cheat Enabled on Game!");
         }
      }

      ////---------------//// Generate all Commands---------

      commandList = new List<object>();
      

      for (int i = 0; i < commandScriptableObject.commandList.Count; i++)
      {
         DebugCommand debugCommand = new DebugCommand(
            commandScriptableObject.commandList[i].commandID,
            commandScriptableObject.commandList[i].commandDescription,
            commandScriptableObject.commandList[i].commandFormat,
            () =>
            {
               
            }
         );

         
         commandList.Add(debugCommand);
      }
   }




private void OnGUI()
   {
      if (!showConsole)
      {
         return;
      }

      float y = 40f;
      GUI.Box(new Rect(0,y,Screen.width,70),"" );
      GUI.backgroundColor = new Color(0,0,0,0);
      
      GUIStyle myTextField = new GUIStyle(GUI.skin.textField);
      myTextField.fontSize = 50;
      input = GUI.TextField(new Rect(30f,y+5f,Screen.width-20f,100f), input,myTextField);
      
   }

   private void Update()
   {
      if (Input.GetMouseButton(1))
      {
         OnReturn();
      }
   }

   public void OnReturn()
   {
      if (showConsole)
      {
         HandleInput();
         input = "";
      }
   }

   void HandleInput()
   {
      for (int i = 0; i < commandList.Count; i++)
      {
         DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
         
         if(input.Contains(EditModeDebug.Instance.eventCommandList[i].commandFormat))
         {
            if (EditModeDebug.Instance.eventCommandList[i].commandFormat != null)
            {
               EditModeDebug.Instance.eventCommandList[i].OnCommandExecute.Invoke();
            }
         }
      }
   }
}
