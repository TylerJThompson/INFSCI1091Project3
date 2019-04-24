using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public GameManager gameManager;
    public Arrow pooledArrow;
    public Enemy pooledEnemy;
    public int poolSize;
    public Transform target;

    List<GameObject> pool = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        createPool();
    }

    public void createPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (pooledArrow != null)
            {
                Arrow obj = Instantiate(pooledArrow);
                obj.gameObject.SetActive(false);
                pool.Add(obj.gameObject);
            }
            else
            {
                Enemy obj = Instantiate(pooledEnemy);
                obj.gameObject.SetActive(false);
                obj.gameManager = gameManager;
                obj.target = target;
                pool.Add(obj.gameObject);
            }
            
        }
    }

    public GameObject getPooledObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i] != null && !pool[i].activeSelf)
            {
                return pool[i];
            }
        }
        return null;
    }
}
