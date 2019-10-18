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
    public GameObject alignmentDot;
    public GameObject alignmentCircle;
    private Vector3 offScreen = new Vector3(100, 100, 100);
    public GameObject desiredHeadingDot;
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

            desiredHeadingDot.transform.position = selectedBird.desiredHeading + selectedBird.transform.position;

            if (selectedBird.doCohesion)
            {
                cohesionCircle.transform.position = birdPosition;
                cohesionCircle.transform.localScale = new Vector3(0.5f * selectedBird.cohesionRadius, 0.5f * selectedBird.cohesionRadius, 1);
            } else
            {
                cohesionCircle.transform.position = offScreen;
            }

            if (selectedBird.doSeparation)
            {
                separationCircle.transform.position = birdPosition;
                separationCircle.transform.localScale = new Vector3(0.5f * selectedBird.separationRadius, 0.5f * selectedBird.separationRadius, 1);
            }
            else
            {
                separationCircle.transform.position = offScreen;
            }

            if (selectedBird.doAlignment)
            {
                alignmentCircle.transform.position = birdPosition;
                alignmentCircle.transform.localScale = new Vector3(0.5f * selectedBird.alignmentRadius, 0.5f * selectedBird.alignmentRadius, 1);
            }
            else
            {
                alignmentCircle.transform.position = offScreen;
            }

            if (selectedBird.doCohesion && selectedBird.centreOfMassCohesion != Vector3.zero)
            {
                cohesionDot.transform.position = selectedBird.centreOfMassCohesion;
            } else
            {
                cohesionDot.transform.position = offScreen;
            }
            if (selectedBird.doSeparation && selectedBird.centreOfMassSeparation != Vector3.zero)
            {
                separationDot.transform.position = selectedBird.centreOfMassSeparation;
            } else
            {
                separationDot.transform.position = offScreen;
            }
            if (selectedBird.doAlignment && selectedBird.alignmentCentreOfMass != Vector3.zero)
            {
                alignmentDot.transform.position = selectedBird.alignmentCentreOfMass + selectedBird.transform.position;
            }
            else
            {
                alignmentDot.transform.position = offScreen;
            }


        } else
        {
            cohesionCircle.transform.position = offScreen;
            separationCircle.transform.position = offScreen;
            cohesionDot.transform.position = offScreen;
            separationDot.transform.position = offScreen;
            alignmentCircle.transform.position = offScreen;
            alignmentDot.transform.position = offScreen;
        }
    }
}
