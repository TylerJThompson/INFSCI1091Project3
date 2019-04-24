using System.Collections;
using UnityEngine;
using Valve.VR;

public class Arrow : MonoBehaviour
{
    public GameObject blood;
    public SteamVR_Action_Boolean grabAction;

    private bool isFired;
    private MeshRenderer renderer;

    private void Start()
    {
        isFired = false;
        renderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (isFired)
        {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }
    }

    public void wasFired()
    {
        isFired = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) AttachArrow();
        else if (!other.gameObject.tag.Equals("projectile"))
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            if (isFired == true)
            {
                if (!other.gameObject.tag.Equals("enemy")) audioSource.Play();
                else blood.SetActive(true);
                renderer.material.shader = Shader.Find("Transparent/Diffuse");
                StartCoroutine("FadeOut");
            }
            isFired = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) AttachArrow();
    }

    private void AttachArrow()
    {
        if (grabAction.stateDown)
        {
            ArrowManager.instance.AttachBowToArrow();
        }
    }

    IEnumerator FadeOut()
    {
        Color color = renderer.material.color;
        for (float i = 1f; i >= -0.05f; i -= 0.05f)
        {
            color.a = i;
            renderer.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.SetActive(false);
        color.a = 1f;
        renderer.material.color = color;
        blood.SetActive(false);
    }
}
