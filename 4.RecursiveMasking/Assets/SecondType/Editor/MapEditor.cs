using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MazeController))]
public class MapEditor : Editor
{

    public override void OnInspectorGUI()
    {
        MazeController map = target as MazeController;
        if (DrawDefaultInspector())
        {
            map.ToGenerateMap();
        }

        if (GUILayout.Button("Generate Map"))
        {
            map.ToGenerateMap();
        }




    }
}
