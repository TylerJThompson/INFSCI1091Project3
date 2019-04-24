using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour
{
    public GameObject explosion;

    private MeshRenderer renderer;

    private void Start()
    {
        renderer = gameObject.GetComponentInChildren<MeshRenderer>();
    }

    public void TriggerExplosionHelper()
    {
        StartCoroutine("TriggerExplosion");
    }

    IEnumerator TriggerExplosion()
    {
        explosion.SetActive(true);
        renderer.material.shader = Shader.Find("Transparent/Diffuse");
        Color color = renderer.material.color;
        for (float i = 1f; i >= 0f; i -= (0.05f / 3f))
        {
            color.a = i;
            renderer.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.SetActive(false);
        explosion.SetActive(false);
    }
}
