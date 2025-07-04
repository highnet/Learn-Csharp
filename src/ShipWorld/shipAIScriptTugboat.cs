using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipAIScriptTugboat : MonoBehaviour {

    private GameObject mainControl;
    public float maxHitPoints;
    public float hitPoints;
    public float waterLevel;
    public string shipClass;
    public string ultimateType;

    public string captainClass;
    public string faction;

    public float attackRate;
    public float bilgeRate;
    public float repairRate;
    public float ultimateRate;

    public float cannonDmg;
    public float repairValue;
    public float bilgeValue;


    // Use this for initialization
    void Start () {

        hitPoints = Random.Range(120f, 1000f);
        maxHitPoints = hitPoints;
        waterLevel = 30f;

        cannonDmg = Random.Range(30f, 100f);
 

        attackRate = Random.Range(5, 10);
        bilgeRate = Random.Range(10, 20);
        repairRate = Random.Range(20, 30);
        ultimateRate = Random.Range(30, 40);

        repairValue = Random.Range(25, 200);
        bilgeValue = Random.Range(5, 25);

        waterLevel = 100f;

 

        int x = Random.Range(0, 2);
        if (x == 0)
        {
            captainClass = "Cpt. Sparrow";
            faction = "britain";
            ultimateType = "instant repair";
        } else if (x == 1)
        {
            captainClass = "Cpt. Blackhammer";
            faction = "pirate";
            ultimateType = "cannon burst";
        }
        mainControl = GameObject.Find("Main Control");
 
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    private void FixedUpdate()
    {
        if (mainControl.GetComponent<controlScript>().gameModeStatus.Equals("sailing"))
        {
                transform.Rotate(0, Random.Range(-3, 3), 0);
                transform.Translate(-Random.Range(0, 0.3f), 0, 0);

        }
    }
}