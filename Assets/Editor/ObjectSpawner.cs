using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : EditorWindow
{
    private GameObject prefab;
    private GameObject parent;
    List<GameObject> spawnedObjects = new List<GameObject>();
    private string prefix = "spawned_";
    private int spawnCount = 5000;
    private int totalSpawnCount = 0;
    private int maxSpawnDistance = 1500;
    float minScaleVal = 5;
    float minScaleLimit = 1;
    float maxScaleVal = 10;
    float maxScaleLimit = 15;
    float minRotationVal = 0;
    float maxRotationVal = 350;

    [MenuItem("Window/Object Spawner Window")]
    public static void ShowWindow()
    {
        GetWindow<ObjectSpawner>("ObjectSpawnerWindow");
    }

    void OnGUI()
    {
        GUILayout.Label("What to spawn", EditorStyles.boldLabel);

        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab to spawn", prefab, typeof(GameObject), true);
        parent = (GameObject)EditorGUILayout.ObjectField("Parent in scene", parent, typeof(GameObject), true);

        EditorGUILayout.Space();

        GUILayout.Label("Spawn options", EditorStyles.boldLabel);

        prefix = EditorGUILayout.TextField("New Object Name Prefix", prefix);
        spawnCount = EditorGUILayout.IntField("Spawn count", spawnCount);
        maxSpawnDistance = EditorGUILayout.IntField("Maximum spawn distance", maxSpawnDistance);

        EditorGUILayout.LabelField("Minimun scale factor:", minScaleVal.ToString());
        EditorGUILayout.LabelField("Maximum scale factor:", maxScaleVal.ToString());
        EditorGUILayout.MinMaxSlider(ref minScaleVal, ref maxScaleVal, minScaleLimit, maxScaleLimit);

        EditorGUILayout.Space();

        if (GUILayout.Button("Spawn " + spawnCount + " objects"))
        {
            for (int i = 0; i < spawnCount; i++)
            {
                var position = new Vector3(Random.Range(-maxSpawnDistance, maxSpawnDistance), 0, Random.Range(-maxSpawnDistance, maxSpawnDistance));
                float randomScale = Random.Range(minScaleVal, maxScaleVal);
                var scale = new Vector3(randomScale, randomScale, randomScale);
                float randomRotation = Random.Range(minRotationVal, maxRotationVal);
                var rotation = new Vector3(randomRotation, randomRotation, randomRotation);

                GameObject prefabInstance = Instantiate(prefab, position, Quaternion.identity);

                prefabInstance.transform.localScale = scale;
                prefabInstance.name = prefix + prefabInstance.name;
                prefabInstance.transform.eulerAngles = rotation;

                if (parent != null)
                {
                    prefabInstance.transform.parent = parent.transform;
                }

                spawnedObjects.Add(prefabInstance);
                totalSpawnCount++;
            }
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Spawned count:", totalSpawnCount.ToString());

        if (GUILayout.Button("Reset spawned count"))
        {
            foreach(var obj in spawnedObjects)
            {
                DestroyImmediate(obj);
            }
            totalSpawnCount = 0;
        }
    }
}
