using UnityEngine;
using UnityEditor;

public class MyWindow : EditorWindow
{
    [MenuItem("Window/Prefab Spawner")]
    public static void ShowWindow()
    {
        GetWindow<MyWindow>("PrefabSpawner");
    }

    void OnGUI()
    {
        
    }
}
