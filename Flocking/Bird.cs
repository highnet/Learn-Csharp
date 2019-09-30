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
    public GameObject worldController;
    public NeighborLines neighborLines;

    private void Awake()
    {
        worldController = GameObject.FindGameObjectsWithTag("World Controller")[0];
        neighborLines = worldController.GetComponent<NeighborLines>();
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
        foreach(Collider hit in hitColliders)
        {
            if (hit.gameObject.tag.Contains("bird"))
            {
                neighborsCohesion.Add(hit.gameObject.GetComponent<Bird>());
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
        Vector3 centreOfMass;
        centreOfMass = new Vector3();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, separationRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject != this.gameObject && hitColliders[i].tag == "bird")
            {
                centreOfMass += hitColliders[i].GetComponent<Bird>().transform.position;
                neighbourCount++;
            }


            if (neighbourCount != 0 )
            {
                centreOfMass /= neighbourCount;
                if (Vector3.Distance(transform.position, centreOfMass) < separationRadius)
                {
                    desiredHeading = -(centreOfMass - transform.position).normalized;
                }

            }

            i++;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("ouch");
        neighborLines.selectedBird = this;
    }

    private void Cohesion()
    {
        int neighbourCount = 0;
        Vector3 centreOfMass;
        centreOfMass = new Vector3();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, cohesionRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject != this.gameObject && hitColliders[i].tag == "bird")
            {
                centreOfMass += hitColliders[i].GetComponent<Bird>().transform.position;
                neighbourCount++;
            }


            if (neighbourCount != 0)
            {
                centreOfMass /= neighbourCount;
                desiredHeading = (centreOfMass - transform.position).normalized;
            }

            i++;
        }
    }

    private void Alignment()
    {
        int neighbourCount = 0;
        Vector3 nextMoveDirection;
        nextMoveDirection = new Vector3();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, alignmentRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject != this.gameObject && hitColliders[i].tag == "bird")
            {
                nextMoveDirection = nextMoveDirection + hitColliders[i].GetComponent<Bird>().moveDirection;
                neighbourCount++;
            }


            if (neighbourCount != 0)
            {
                nextMoveDirection /= neighbourCount;
                desiredHeading = nextMoveDirection.normalized;
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