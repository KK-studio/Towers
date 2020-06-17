using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("set towers"))
        {
            Player player = (Player) target;
            player.towers = new List<GameObject>();
            MeshRenderer[] gameObjects = player.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < gameObjects.Length; i++)
            {
                player.towers.Add(gameObjects[i].transform.parent.gameObject);
            }
        }
    }

}
