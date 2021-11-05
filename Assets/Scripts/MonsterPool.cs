using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public static MonsterPool instance = null;

    public static Dictionary<GameObject, GameObject> monsterID;
    GameObject[] monster;
    GameObject tempMons;

    public static Dictionary<GameObject, Queue<GameObject>> poolID;
    Queue<GameObject> spawnPool;
    Queue<GameObject> tempPool;
    [Range(10, 100)]
    public int poolSize = 50;

    public static Dictionary<Queue<GameObject>, Transform> poolPos;
    Transform spawnPoolPos;
    Transform tempTrans;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        monsterID = new Dictionary<GameObject, GameObject>();
        monster = Resources.LoadAll<GameObject>("Monsters/");
        poolID = new Dictionary<GameObject, Queue<GameObject>>();
        spawnPool = new Queue<GameObject>();
        poolPos = new Dictionary<Queue<GameObject>, Transform>();
        spawnPoolPos = transform.Find("SpawnPool");
    }

    void Start()
    {
        for (int i = 0; i < monster.Length; i++)
        {
            tempPool = new Queue<GameObject>();
            poolID.Add(monster[i], tempPool);
            tempTrans = transform.Find("WaitPool" + i);
            poolPos.Add(tempPool, tempTrans);

            while (poolID[monster[i]].Count < poolSize)
            {
                tempMons = Instantiate(monster[i]);
                monsterID.Add(tempMons, monster[i]);
                tempMons.SetActive(false);
                tempTrans = GetPoolPos(tempMons);
                Reposition(tempMons, tempTrans);
                GetPool(tempMons).Enqueue(tempMons);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Reload();
        if (Input.GetKeyDown(KeyCode.Space))
            Spawn();
    }


    void Reload()
    {
        while(spawnPool.Count < poolSize)
        {
            tempMons = monster[Random.Range(0, 3)];
            tempPool = poolID[tempMons];
            if (tempPool.Count != 0)
            {
                tempMons = tempPool.Dequeue();
                Reposition(tempMons, spawnPoolPos);
                spawnPool.Enqueue(tempMons);
            }
            else if (poolID[monster[0]].Count + poolID[monster[1]].Count + poolID[monster[2]].Count == 0)
            {
                Debug.Log("대기 풀이 비었다.");
                return;
            }
        }
    }

    void Spawn()
    {
        if (spawnPool.Count != 0)
        {
            tempMons = spawnPool.Dequeue();
            tempMons.SetActive(true);
            tempMons.transform.position = new Vector3(Random.Range(-13f, 13f), 1f, Random.Range(0f, 13f));
        }
        else
        {
            Debug.Log("스폰 풀이 비었다.");
        }
    }

    public static void Despawn(GameObject tempMons)
    {
        Transform tempTrans = GetPoolPos(tempMons);
        tempMons.SetActive(false);
        Reposition(tempMons, tempTrans);
        GetPool(tempMons).Enqueue(tempMons);
    }

    static void Reposition(GameObject tempMons, Transform tempTrans)
    {
        tempMons.transform.position = tempTrans.position;
        tempMons.transform.rotation = tempTrans.rotation;
        tempMons.transform.parent = tempTrans;
    }

    static Queue<GameObject> GetPool(GameObject tempMons)
    {
        return poolID[monsterID[tempMons]];
    }

    static Transform GetPoolPos(GameObject tempMons)
    {
        return poolPos[poolID[monsterID[tempMons]]];
    }
}
