using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float lastSpawn = 0;
    public Pooler goblinPool;
    public Pooler wolfPool;
    public Pooler hobgoblinPool;
    public Pooler trollPool;
    public float spawnInterval;

    private bool allowSpawn = false;
    private float initialSpawnInterval;

    private void Start()
    {
        initialSpawnInterval = spawnInterval;
        spawnInterval = Random.Range(1f, 3f);
    }

    private void Update()
    {
        if (allowSpawn) objectSpawn();
    }

    private void objectSpawn()
    {
        GameObject goblin = goblinPool.getPooledObject();
        GameObject wolf = wolfPool.getPooledObject();
        GameObject hobgoblin = hobgoblinPool.getPooledObject();
        GameObject troll = trollPool.getPooledObject();
        if ((Time.fixedTime - lastSpawn) > spawnInterval)
        {
            int rand = Random.Range(0, 100);
            if (troll != null && rand < 10)
            {
                troll.transform.position = transform.position;
                troll.SetActive(true);
                lastSpawn = Time.fixedTime;
                spawnInterval = Random.Range(initialSpawnInterval - (initialSpawnInterval / 2), initialSpawnInterval + (initialSpawnInterval / 2));
            }
            else if (hobgoblin != null && rand < 25)
            {
                hobgoblin.transform.position = transform.position;
                hobgoblin.SetActive(true);
                lastSpawn = Time.fixedTime;
                spawnInterval = Random.Range(initialSpawnInterval - (initialSpawnInterval / 2), initialSpawnInterval + (initialSpawnInterval / 2));
            }
            else if (wolf != null && rand < 50)
            {
                wolf.transform.position = transform.position;
                wolf.SetActive(true);
                lastSpawn = Time.fixedTime;
                spawnInterval = Random.Range(initialSpawnInterval - (initialSpawnInterval / 2), initialSpawnInterval + (initialSpawnInterval / 2));
            }
            else if (goblin != null)
            {
                goblin.transform.position = transform.position;
                goblin.SetActive(true);
                lastSpawn = Time.fixedTime;
                spawnInterval = Random.Range(initialSpawnInterval - (initialSpawnInterval / 2), initialSpawnInterval + (initialSpawnInterval / 2));
            }
        }
    }

    public void AllowSpawn(bool allow)
    {
        allowSpawn = allow;
    }

    public bool GetAllowSpawn()
    {
        return allowSpawn;
    }
}
