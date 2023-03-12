using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CurveMovement))]
public class CurveMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        GUILayout.Space(10); // Add some spacing between existing features and the button
        
        if (GUILayout.Button("Run Specific Function"))
        {
            CurveMovement component = (CurveMovement)target;
            component.calculateCurve();
        }
    }
}
