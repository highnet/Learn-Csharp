
using UnityEngine;
using UnityEngine.UI;


public class PlayerShipScript : MonoBehaviour
{

    private string gamemodeStatus;
    public float maxHitPoints;
    public float hitPoints;
    public float waterLevel;

    public string shipName;
    public string captainName;
    public string ultimateName;
    public string factionName;

    public float ultimateValueCharge = 0.0f;
    public float shootLeftCharge = 0.0f;
    public float shootRightCharge = 0.0f;
    public float bilgeCharge = 0.0f;
    public float repairCharge = 0.0f;

    public float defensiveAbilityUltimateContribution = 1f;
    public float aggressiveAbilityUltimateContribution = 5f;

    public float shootLeftRechargeRate;
    public float shootRightRechargeRate;
    public float bilgeRechargeRate;
    public float repairRechargeRate;

    public float cannonAccuracy;
    public float cannonDamage;

    public float repairValue;
    public float bilgeValue;

    public GameObject mainControl;
    public GameObject enemyBoatTrigger;

    private AudioSource audioSource;
    public AudioClip cannonSound;

    public float maxSpeedRotation;
    public float maxSpeedForward;



   
    // Use this for initialization
    void Start()
    {
       
        shootLeftRechargeRate = 50.0f;  // dev hax
        shootRightRechargeRate = 51.0f;  // dev hax
        bilgeRechargeRate = 50.0f;  // dev hax
        repairRechargeRate = 50.5f;  // dev hax
        waterLevel = 10f; // dev hax
        

        mainControl = GameObject.Find("Main Control");
        gamemodeStatus = mainControl.GetComponent<controlScript>().gameModeStatus;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame

    void Update()
    {
        gamemodeStatus = mainControl.GetComponent<controlScript>().gameModeStatus;
        updateAllStats();
    }

    void updateAllStats() // updates all ship variables based on ship inventory loadout.
    {

    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CombatTrigger")
        {
            mainControl.GetComponent<controlScript>().gameModeStatus = "fighting";

             mainControl.GetComponent<controlScript>().activeEnemyShipEngagedReference = collision.gameObject;

            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY; // freeze movement during combat

            audioSource.PlayOneShot(cannonSound);
        }

    }

 


    private void FixedUpdate()
    {
        if (gamemodeStatus.Equals("sailing"))
        {
             float fowardValue;
             float rotationValue;


            if (Input.GetKey(KeyCode.X))
            {
                fowardValue =  Input.GetAxis("Vertical") * Time.deltaTime * maxSpeedRotation * 2;
                rotationValue = Input.GetAxis("Horizontal") * Time.deltaTime * maxSpeedForward * 2;
            }
            else
            {
                fowardValue = Input.GetAxis("Vertical") * Time.deltaTime * maxSpeedRotation;
                rotationValue = Input.GetAxis("Horizontal") * Time.deltaTime * maxSpeedForward;
            }
          if (shipName.Equals("sloop")){ 
            transform.Rotate(0, 0, -rotationValue);
            transform.Translate(fowardValue, 0, 0);
            }
          if (shipName.Equals("frigate"))
            {
                transform.Rotate(0, 0, -rotationValue);
                transform.Translate(0, fowardValue,0 );
            }

        }
    }
}

