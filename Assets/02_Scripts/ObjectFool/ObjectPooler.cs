using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            for(int i = 0; i < pool.size; ++i)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);        
        }
    }

    public GameObject spawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        float fallSpeed = Random.Range(3, 7);

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't excist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectToSpawn.GetComponent<Rigidbody>().drag = fallSpeed;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        //Main Branch를 list에 삽입.
        if (objectToSpawn.name == "MainBranch(Clone)")
        {
            Branch.Instance.mainBranchList.Add(objectToSpawn.transform.GetChild(0).gameObject.transform);
            Branch.Instance.mainBranchLeftList.Add(objectToSpawn.transform.GetChild(1).gameObject.transform);
            Branch.Instance.mainBranchRightList.Add(objectToSpawn.transform.GetChild(2).gameObject.transform);
        }

        //other Branch 들의 첫번째 자식 위치포지션을 list에 삽입.
        if (objectToSpawn.name == "ForsyBranch(Clone)")
        {
            Branch.Instance.mainBranchPosList.Add(objectToSpawn.transform.GetChild(0).gameObject.transform);
        }
        if (objectToSpawn.name == "LeafBranch(Clone)")
        {
            Branch.Instance.mainBranchPosList.Add(objectToSpawn.transform.GetChild(0).gameObject.transform);
        }
        if (objectToSpawn.name == "NormalBranch(Clone)")
        {
            Branch.Instance.mainBranchPosList.Add(objectToSpawn.transform.GetChild(0).gameObject.transform);
        }
        if (objectToSpawn.name == "BreakBranch(Clone)")
        {
            Branch.Instance.mainBranchPosList.Add(objectToSpawn.transform.GetChild(0).gameObject.transform);
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
