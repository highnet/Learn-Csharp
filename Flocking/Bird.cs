using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    public Vector3 moveDirection;
    public Vector3 desiredHeading;
    public float moveSpeed;
    [Range(0.1F, 10.0F)]
    public float alignmentRadius;
    [Range(0.1F, 10.0F)]
    public float cohesionRadius;
    [Range(0.1F, 10.0F)]
    public float separationRadius;
    public float turnSpeed;
    public bool doRandomizeSpeed = false;
    public bool doAlignment = true;
    public bool doCohesion = true;
    public bool doSeparation = true;
    public List<Bird> neighborsCohesion;
    public List<Bird> neighborsSeparation;
    public GameObject worldController;
    public NeighborLines neighborLines;
    public NeighborRadius neighborRadius;
    public Vector3 centreOfMassSeparation;
    public Vector3 centreOfMassCohesion;
    public Vector3 alignmentCentreOfMass;

    private void Awake()
    {
        worldController = GameObject.FindGameObjectsWithTag("World Controller")[0];
        neighborLines = worldController.GetComponent<NeighborLines>();
        neighborRadius = worldController.GetComponent<NeighborRadius>();
    }

    private void Start()
    {
        desiredHeading = GetComponent<Transform>().up.normalized;
        moveDirection = desiredHeading;
        StartCoroutine("SteerToHeading");
        StartCoroutine("RandomizeSpeed");
        desiredHeading = new Vector3(0, -1, 0);
    }

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World); // move

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, cohesionRadius);
        neighborsCohesion = new List<Bird>();

        foreach (Collider hit in hitColliders)
        {
            if (hit.gameObject.tag.Contains("bird"))
            {
                neighborsCohesion.Add(hit.gameObject.GetComponent<Bird>());
            }
        }

        hitColliders = Physics.OverlapSphere(transform.position, separationRadius);
        neighborsSeparation = new List<Bird>();
        foreach (Collider hit in hitColliders)
        {
            if (hit.gameObject.tag.Contains("bird"))
            {
                neighborsSeparation.Add(hit.gameObject.GetComponent<Bird>());
            }
        }

    }

    private void LateUpdate()
    {
        transform.up = ((transform.position + moveDirection) - transform.position);

        if (doCohesion)
        {
            Cohesion();
        }
        if (doAlignment)
        {
            Alignment();
        }
        if (doSeparation)
        {
            Separation();
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag.Contains("wall"))
        {

            BounceFromWall();
        }
    }

    private void Separation()
    {
        int neighbourCount = 0;
        centreOfMassSeparation = new Vector3();
        int i = 0;
        while (i < neighborsSeparation.Count)
        {
            if (neighborsSeparation[i].gameObject != this.gameObject && neighborsSeparation[i].tag == "bird")
            {
                centreOfMassSeparation += neighborsSeparation[i].GetComponent<Bird>().transform.position;
                neighbourCount++;
            }


            if (neighbourCount != 0 )
            {
                centreOfMassSeparation /= neighbourCount;
                if (Vector3.Distance(transform.position, centreOfMassSeparation) < separationRadius)
                {
                    desiredHeading = -(centreOfMassSeparation - transform.position).normalized;
                }

            }

            i++;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("ouch");
        neighborLines.selectedBird = this;
        neighborRadius.selectedBird = this;
    }

    private void Cohesion()
    {
        int neighbourCount = 0;
        centreOfMassCohesion = new Vector3();
        int i = 0;
        while (i < neighborsCohesion.Count)
        {
            if (neighborsCohesion[i].gameObject != this.gameObject && neighborsCohesion[i].tag == "bird")
            {
                centreOfMassCohesion += neighborsCohesion[i].GetComponent<Bird>().transform.position;
                neighbourCount++;
            }


            if (neighbourCount != 0)
            {
                centreOfMassCohesion /= neighbourCount;
                desiredHeading = (centreOfMassCohesion - transform.position).normalized;
            }

            i++;
        }
    }

    private void Alignment()
    {
        int neighbourCount = 0;
        alignmentCentreOfMass = new Vector3();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, alignmentRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject != this.gameObject && hitColliders[i].tag == "bird")
            {
                alignmentCentreOfMass += hitColliders[i].GetComponent<Bird>().moveDirection;
                neighbourCount++;
            }


            if (neighbourCount != 0)
            {
                alignmentCentreOfMass /= neighbourCount;
                desiredHeading = alignmentCentreOfMass.normalized;
            }

            i++;
        }


    }

    private void BounceFromWall()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.up);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            switch (hit.collider.gameObject.name)
            {
                case "Wall Vertical Right":
                    moveDirection.x = -1;
                    break;
                case "Wall Horizontal Up":
                    moveDirection.y = -1;
                    Debug.Log("TEST");
                    break;
                case "Wall Vertical Left":
                    moveDirection.x = 1;
                    break;
                case "Wall Horizontal Down":
                    moveDirection.y = 1;
                    break;
                default:

                    break;
            }
            desiredHeading = moveDirection;
        }
    }


    IEnumerator RandomizeSpeed()
    {
        for (; ; )
        {
            if (doRandomizeSpeed)
            {
                moveSpeed += Random.Range(-0.01f, 0.01f);
            }
            yield return new WaitForSeconds(.01f);
        }

    }

    IEnumerator SteerToHeading()
    {
        for (; ; )
        {
            moveDirection = Vector3.RotateTowards(moveDirection, desiredHeading, turnSpeed * Time.deltaTime, 0f);
            moveDirection.z = 0;
            moveDirection.Normalize();
            yield return new WaitForSeconds(.01f);
        }
    }
}