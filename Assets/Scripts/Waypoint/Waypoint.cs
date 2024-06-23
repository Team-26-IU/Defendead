using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint Waypoint => target as Waypoint;
    private void OnSceneGUI()
    {
        Handles.color = Color.cyan;
        for (int i = 0; i < Waypoint.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i]; 
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, 
                Quaternion.identity, 14f, 
                new Vector3(6f, 6f, 6f), Handles.SphereHandleCap);
            
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.white;
            Vector3 textAlligment = Vector3.down * 7f + Vector3.right * 7f;
            Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[i] + textAlligment,
                $"{i + 1}", textStyle);
            EditorGUI.EndChangeCheck();
            if (EditorGUI.EndChangeCheck())
            { 
                Undo.RecordObject(target, "Free Move Handle");
                Waypoint.Points[i] = newWaypointPoint - Waypoint.CurrentPosition;
            }
        }
    }
}