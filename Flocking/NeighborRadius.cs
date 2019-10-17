using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborRadius : MonoBehaviour
{
    public Bird selectedBird;
    public GameObject cohesionCircle;
    public GameObject cohesionDot;
    public GameObject separationDot;
    public GameObject separationCircle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedBird != null)
        {
        Vector3 birdPosition = selectedBird.transform.position;
        cohesionCircle.transform.position = birdPosition;
        separationCircle.transform.position = birdPosition;
        cohesionCircle.transform.localScale = new Vector3(0.5f * selectedBird.cohesionRadius,0.5f *selectedBird.cohesionRadius,1);
        separationCircle.transform.localScale = new Vector3(0.5f * selectedBird.separationRadius, 0.5f * selectedBird.separationRadius, 1);
        cohesionDot.transform.position = selectedBird.centreOfMassCohesion;
        separationDot.transform.position = selectedBird.centreOfMassSeparation;
        } else
        {
            cohesionCircle.transform.position = new Vector3(100, 100, 100);
            separationCircle.transform.position = new Vector3(100, 100, 100);
            cohesionDot.transform.position = new Vector3(100, 100, 100);
            separationDot.transform.position = new Vector3(100, 100, 100);
        }
    }
}
