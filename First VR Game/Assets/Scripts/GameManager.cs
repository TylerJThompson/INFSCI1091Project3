using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public RawImage blackScreen;
    public Spawner[] spawners = new Spawner[8];
    public Rock[] rocks = new Rock[3];
    public SpriteRenderer[] healthyHearts = new SpriteRenderer[5];
    public SpriteRenderer[] woundedHearts = new SpriteRenderer[5];

    private int health = 10;
    private int maxHealth = 10;
    private float timeElapsed;

    void Start()
    {
        StartCoroutine("FadeOut");
        for (int i = 0; i < 8; i++) spawners[i].AllowSpawn(false);
        for (int i = 0; i < 2; i++)
        {
            spawners[i].spawnInterval = 5;
            spawners[i].AllowSpawn(true);
        }
        timeElapsed = Time.fixedTime;
    }

    void Update()
    {
        if (!spawners[2].GetAllowSpawn() && Time.fixedTime - timeElapsed >= 30)
        {
            for (int i = 0; i < 4; i++)
            {
                spawners[i].spawnInterval = 10;
                spawners[i].AllowSpawn(true);
            }
            rocks[0].TriggerExplosionHelper();
        }
        else if (!spawners[4].GetAllowSpawn() && Time.fixedTime - timeElapsed >= 90)
        {
            for (int i = 0; i < 6; i++)
            {
                spawners[i].spawnInterval = 15;
                spawners[i].AllowSpawn(true);
            }
            rocks[1].TriggerExplosionHelper();
        }
        else if (!spawners[6].GetAllowSpawn() && Time.fixedTime - timeElapsed >= 310)
        {
            for (int i = 0; i < 8; i++)
            {
                spawners[i].spawnInterval = 20;
                spawners[i].AllowSpawn(true);
            }
            rocks[2].TriggerExplosionHelper();
        }
        if (health % 2 == 0) maxHealth = Mathf.Max(health, 0);
        if (health <= 0) StartCoroutine("FadeIn");
    }

    public void LoseHealth()
    {
        if (maxHealth >= 2)
        {
            if (health % 2 == 0) healthyHearts[maxHealth / 2 - 1].enabled = false;
            else woundedHearts[maxHealth / 2 - 1].enabled = false;
        }
        health--;
    }

    IEnumerator FadeOut()
    {
        Color color = blackScreen.material.color;
        for (float i = 1f; i >= -0.05f; i -= 0.05f)
        {
            color.a = i;
            blackScreen.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeIn()
    {
        Color color = blackScreen.material.color;
        for (float i = 0f; i <= 1f; i += 0.05f)
        {
            color.a = i;
            blackScreen.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
