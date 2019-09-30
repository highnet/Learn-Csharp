using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborLines : MonoBehaviour
{

    public Bird selectedBird;
    public List<Bird> selectedBirdNeighborsCohesion;
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (selectedBird != null )
        {
            selectedBirdNeighborsCohesion = selectedBird.neighborsCohesion;
            Vector3[] positions = new Vector3[selectedBirdNeighborsCohesion.Count + 1];

            if (selectedBirdNeighborsCohesion.Count > 0)
            {
                lineRenderer.positionCount = 1 + selectedBirdNeighborsCohesion.Count;
                positions[0] = selectedBird.transform.position;
                for (int i = 1; i < positions.Length; i++)
                {
                    positions[i] = selectedBirdNeighborsCohesion[i - 1].transform.position;
                }

                lineRenderer.SetPositions(positions);
            }

        }
    }
}
