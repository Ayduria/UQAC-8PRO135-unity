using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool 
    {
        public string tag; 
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static bulletPooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public Camera cam;
    public Transform attackPoint;
    public float shootForce;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools) 
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
               GameObject obj =  Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

   public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag)) { Debug.LogWarning("Pool with tag " + tag + "doesn't exist"); return null; }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit)) { targetPoint = hit.point; }
        else { targetPoint = ray.GetPoint(75); }

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectToSpawn.transform.forward = directionWithoutSpread.normalized;
        objectToSpawn.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);


        IPoolObject poolObj = objectToSpawn.GetComponent<IPoolObject>();

        if (poolObj != null)
        {
            poolObj.Shoot();  
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
