using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float deathAnimTime;
    public GameManager gameManager;
    public int health;
    public bool isRunning;
    public float minDistance;
    public AudioClip[] soundsArray = new AudioClip[3];
    public float speed;
    public Transform target;

    private NavMeshAgent ai;
    private Animator animator;
    private AudioSource audioSource;
    private int currentHealth;
    private Camera targetCamera;

    // Start is called before the first frame update
    void Start()
    {
        ai = gameObject.GetComponent<NavMeshAgent>();
        ai.destination = target.position;
        animator = gameObject.GetComponent<Animator>();
        if (isRunning) animator.SetBool("run", true);
        else animator.SetBool("walk", true);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = soundsArray[0];
        audioSource.Play();
        currentHealth = health;
    }

    private void OnEnable()
    {
        ai = gameObject.GetComponent<NavMeshAgent>();
        ai.destination = target.position;
        animator = gameObject.GetComponent<Animator>();
        if (isRunning) animator.SetBool("run", true);
        else animator.SetBool("walk", true);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = soundsArray[0];
        audioSource.Play();
        ai.speed = speed;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            ai.speed = 0;
            if (isRunning) animator.SetBool("run", false);
            else animator.SetBool("walk", false);
            animator.SetBool("dead", true);
            audioSource.clip = soundsArray[2];
            audioSource.Play();
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine("SetInactive");
            currentHealth = health;
        }
        else if (Vector3.Distance(transform.position, target.position) < minDistance)
        {
            ai.speed = 0;
            if (isRunning) animator.SetBool("run", false);
            else animator.SetBool("walk", false);
            animator.SetBool("attack", true);
        }
    }

    IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(deathAnimTime * 3);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("projectile")) currentHealth--;
        if (currentHealth > 0)
        {
            audioSource.clip = soundsArray[1];
            audioSource.Play();
        }
    }

    public void HurtPlayer()
    {
        gameManager.LoseHealth();
    }
}
