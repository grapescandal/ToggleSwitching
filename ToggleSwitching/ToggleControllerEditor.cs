using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ToggleController))]
public class ToggleControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ToggleController myScript = (ToggleController)target;
        
        if(GUILayout.Button("Get Origin position"))
        {
            myScript.GetCurrentTextPosition(0);
        }
             
        if(GUILayout.Button("Get Final position"))
        {
            myScript.GetCurrentTextPosition(1);
        }

        GUILayout.Space(10);

        if(GUILayout.Button("Set to Origin position"))
        {
            myScript.SetToTextPosition(0);
        }

        if(GUILayout.Button("Set to Final position"))
        {
            myScript.SetToTextPosition(1);
        }
    }
}
