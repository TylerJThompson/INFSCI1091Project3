using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ArrowManager : MonoBehaviour
{
    public static ArrowManager instance;

    public Pooler arrowPooler;
    public GameObject arrowStart;
    public GameObject bowString;
    public SteamVR_Action_Boolean grabAction;
    public GameObject stringStart;
    public SteamVR_TrackedObject trackedObj;

    private GameObject currentArrow;
    private bool isArrowAttached;
    
    void Start()
    {
        if (instance == null) instance = this;
        isArrowAttached = false;
    }

    void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    void Update()
    {
        AttachArrow();
        PullString();
    }

    private void AttachArrow()
    {
        if (currentArrow == null)
        {
            currentArrow = arrowPooler.getPooledObject();
            if (currentArrow != null)
            {
                currentArrow.SetActive(true);
                currentArrow.transform.parent = trackedObj.transform;
                currentArrow.transform.localPosition = new Vector3(0f, 0f, 0.342f);
                currentArrow.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            }
        }
    }

    public void AttachBowToArrow()
    {
        if (currentArrow != null)
        {
            currentArrow.transform.parent = bowString.transform;
            currentArrow.transform.localPosition = arrowStart.transform.localPosition;
            currentArrow.transform.rotation = arrowStart.transform.rotation;
            isArrowAttached = true;
        }
    }

    private void PullString()
    {
        if (isArrowAttached)
        {
            float dist = Mathf.Min((stringStart.transform.position - trackedObj.transform.position).magnitude, 0.64f);
            bowString.transform.localPosition = stringStart.transform.localPosition + (new Vector3(dist * 7.5f, 0f, 0f));
            if (grabAction.stateUp)
            {
                Fire(dist);
            }
        }
    }

    private void Fire(float dist)
    {
        currentArrow.transform.parent = null;
        Rigidbody rb = currentArrow.GetComponent<Rigidbody>();
        rb.velocity = currentArrow.transform.forward * (dist * dist * 100f);
        rb.useGravity = true;
        currentArrow.GetComponent<Arrow>().wasFired();
        currentArrow = null;
        isArrowAttached = false;
        bowString.transform.position = stringStart.transform.position;
    }
}
