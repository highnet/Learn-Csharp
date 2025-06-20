using UnityEngine;
using System.Collections.Generic;
using System;

public class controlScript : MonoBehaviour
{


    public GameObject playerShip = null;
    public GameObject activeEnemyShipEngagedReference;

    public String wizard_desiredShipName = "";
    public String wizard_desiredCaptainName = "";
    public String wizard_desiredUltimate = "";
    public String wizard_desiredFaction = "";


    public GameObject enemyTugboatPreFab;
    public GameObject playerBoatSloopPrefab;
    public GameObject playerBoatFrigatePreFab;
    public GameObject enemyFrigatePrefab;

    public Camera mainCamera;
    public string gameModeStatus;
    
    public List<GameObject> spawnedShips;

    
    void Start()
    {
        spawnedShips = new List<GameObject>();


    } // initialization


    void Update()
    {
        if (gameModeStatus.Equals("setup field"))
        {
            summonEnemyShips();
            summonPlayerShip();
            gameModeStatus = "sailing";
        }
    }  // Update

    void LateUpdate()
    {
         if (wizard_desiredShipName.Equals("sloop"))
        {
            mainCamera.transform.position = playerShip.transform.position + new Vector3(0f, 100f, -100f);
        }
        else if (wizard_desiredShipName.Equals("frigate"))
        {
            mainCamera.transform.position = playerShip.transform.position + new Vector3(0f, 100f , -100f);

        }
        else
        {
            mainCamera.transform.position = new Vector3(0, -47f, -213f);
        }
        if (playerShip != null)
        {
            mainCamera.transform.LookAt(playerShip.transform);
        }
        doCameraZoom();
     


    }

    void summonPlayerShip()
    {
        if (wizard_desiredShipName.Equals("sloop")) { 
        Instantiate(playerBoatSloopPrefab, new Vector3(0, 0, -0.65f), Quaternion.Euler(90,0,0));
            playerShip = GameObject.Find("Player Ship (Sloop)(Clone)");

        }
        else if (wizard_desiredShipName.Equals("frigate"))
        {
            Instantiate(playerBoatFrigatePreFab, new Vector3(0, 0, -0.65f), Quaternion.Euler(90, 0, 0));
            playerShip = GameObject.Find("Player Ship (Frigate)(Clone)");
        }

        playerShip.GetComponent<PlayerShipScript>().captainName = wizard_desiredCaptainName;
        playerShip.GetComponent<PlayerShipScript>().ultimateName = wizard_desiredUltimate;
        playerShip.GetComponent<PlayerShipScript>().factionName = wizard_desiredFaction;

        if (wizard_desiredCaptainName.Equals("Cpt. Blackhammer")){ // apply cpt blackhammer passives
            playerShip.GetComponent<PlayerShipScript>().cannonDamage += 50;
            playerShip.GetComponent<PlayerShipScript>().repairValue -= 10;
        }

        if (wizard_desiredCaptainName.Equals("Cpt. Sparrow")){ // apply cpt sparrow passives
            playerShip.GetComponent<PlayerShipScript>().repairValue += 20;
            playerShip.GetComponent<PlayerShipScript>().maxSpeedForward *= 0.8f;
            playerShip.GetComponent<PlayerShipScript>().maxSpeedRotation *= 0.8f;
        }
    }

    void summonEnemyShips()
    {
        int i = 0;
        while (i++ < 25)
        {
        var createdObject = Instantiate(enemyTugboatPreFab, new Vector3(UnityEngine.Random.Range(-450, 540), 0.11f , UnityEngine.Random.Range(-540, 450)), Quaternion.Euler(new Vector3(0,0,0)));
            createdObject.name = createdObject.name + i;
            spawnedShips.Add(createdObject);
        }


       i = 0;
        while (i++ < 25)
        {
            var createdObject = Instantiate(enemyFrigatePrefab, new Vector3(UnityEngine.Random.Range(-450, 540), 0.11f, UnityEngine.Random.Range(-540, 450)), Quaternion.Euler(new Vector3(0, 0, 0)));
            createdObject.name = createdObject.name + i;
            spawnedShips.Add(createdObject);
        }
    }

    void doCameraZoom()
    {

        if (gameModeStatus.Equals("fighting"))
        {
            mainCamera.fieldOfView = 60;
        }
        else if (gameModeStatus.Equals("sailing"))
        {

        var dir = Input.GetAxis("Mouse ScrollWheel");
        if (dir > 0f && mainCamera.fieldOfView < 100)
        {
            // scroll up
            mainCamera.fieldOfView++;
        }
        else if (dir < 0f && mainCamera.fieldOfView > 35)
        {
            mainCamera.fieldOfView--;
            // scroll down
        }

        }


    }
}
   