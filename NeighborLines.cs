using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborLines : MonoBehaviour
{

    public Bird selectedBird;
    public List<Bird> selectedBirdNeighborsCohesion;
    public List<Bird> selectedBirdNeighborsSeparation;
    public GameObject cohesionLinePrefab;
    public GameObject separationLinePrefab;
    public List<GameObject> cohesionLines;
    public List<GameObject> separationLines;
    // Start is called before the first frame update
    void Start()
    {
        cohesionLines = new List<GameObject>();
        for(int i = 0; i < 40; i++)
        {
            cohesionLines.Add(GameObject.Instantiate(cohesionLinePrefab));
            separationLines.Add(GameObject.Instantiate(separationLinePrefab));
        }
    }
    // Update is called once per frame

    public void ResetLines()
    {
        foreach(GameObject lineGO in cohesionLines)
        {
            lineGO.GetComponent<LineRenderer>().SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
        }
        foreach (GameObject lineGO in separationLines)
        {
            lineGO.GetComponent<LineRenderer>().SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
        }
    }

    private void DrawLines()
    {
        if (selectedBird != null)
        {
            selectedBirdNeighborsCohesion = selectedBird.neighborsCohesion;
            if (selectedBirdNeighborsCohesion.Count > 0)
            {
                for (int i = 0; i < selectedBirdNeighborsCohesion.Count; i++)
                {
                    cohesionLines[i].GetComponent<LineRenderer>().SetPositions(new Vector3[] { selectedBird.transform.position, selectedBirdNeighborsCohesion[i].transform.position });
                }
            }

            selectedBirdNeighborsSeparation = selectedBird.neighborsSeparation;
            if (selectedBirdNeighborsSeparation.Count > 0)
            {
                for (int i = 0; i < selectedBirdNeighborsSeparation.Count; i++)
                {
                    separationLines[i].GetComponent<LineRenderer>().SetPositions(new Vector3[] { selectedBird.transform.position, selectedBirdNeighborsSeparation[i].transform.position });
                }
            }

        }
    }
    void Update()
    {
        ResetLines();
        DrawLines();


    }
}
