using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        GUILayout.Space(10); // Add some spacing between existing features and the button
        
        if (GUILayout.Button("Run Specific Function"))
        {
            Test component = (Test)target;
            component.calculateCurve();
        }
    }
}
