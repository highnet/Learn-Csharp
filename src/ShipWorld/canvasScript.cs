using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProgressBar;

public class canvasSCript : MonoBehaviour {

    public GameObject gameControl;
    private controlScript controlScript;
    public GameObject playerShip;
    private PlayerShipScript playerShipScript;
    private shipAIScriptTugboat enemyAIShipScript;
    public Text topDebugText;
    public Text playerStatsDisplayText;
    public Text youText;
    public Text yourenemyText;
    public Image pirateLogo;
    public Image gearworksSplash;
    public Button shootLeftButton;
    public Button shootRightButton;
    public Button repairButton;
    public Button ultimateButton;
    public Button bilgeButton;
    public Button confirmSelection;
    public Dropdown shipSelectionDropDown;
    public Dropdown factionSelectionDropDown;
    public Dropdown ultimateSelectionDropdown;
    public Dropdown captainSelectionDropdown;
    public GameObject ultimateProgressCircle;
    public GameObject shootLeftCooldownProgressCircle;
    public GameObject shootRightCooldownProgressCircle;
    public GameObject bilgeCooldownProgressCircle;
    public GameObject RepairCooldownProgressCircle;
    private AudioSource audioSource;
    public AudioClip cannonSound;
    public AudioClip bilgeSound;
    public AudioClip repairSound;
    public AudioClip ultimate_1_Sound;

    public float waterLevelDamageTimer = 0f;
    public float AIattackTimer = 0f;
    public float AibilgeTimer = 0f;
    public float AIrepairTimer = 0f;
    public float AIultimateTimer = 0f;

    void Start () {
        bindButtons();
        controlScript = gameControl.GetComponent<controlScript>();
        audioSource = GetComponent<AudioSource>();
    }

    void bindButtons()
    {
        shootLeftButton.GetComponent<Button>().onClick.AddListener(ShootLeftButton);

        shootRightButton.GetComponent<Button>().onClick.AddListener(ShootRightButton);

        repairButton.GetComponent<Button>().onClick.AddListener(RepairButton);

        ultimateButton.GetComponent<Button>().onClick.AddListener(UltimateButton);

        bilgeButton.GetComponent<Button>().onClick.AddListener(BilgeButton);

        confirmSelection.GetComponent<Button>().onClick.AddListener(ConfirmSelectionButton);
    }

    void lockWizardDesiredChoices()
    {
        if (shipSelectionDropDown.value.Equals(0))
        {
            controlScript.wizard_desiredShipName = "sloop";
        }

        else if (shipSelectionDropDown.value.Equals(1))
        {
            controlScript.wizard_desiredShipName = "frigate";
        }

        ///

        if (factionSelectionDropDown.value.Equals(0))
        {
            controlScript.wizard_desiredFaction = "pirate";
        }
        else if (factionSelectionDropDown.value.Equals(1))
        {
            controlScript.wizard_desiredFaction = "britain";

        }


        ///

        if (ultimateSelectionDropdown.value.Equals(0))
        {
            controlScript.wizard_desiredUltimate = "instant repair";
        }
        else if (ultimateSelectionDropdown.value.Equals(1))
        {
            controlScript.wizard_desiredFaction = "cannon burst";

        }



        ///

        if (captainSelectionDropdown.value.Equals(0))
        {
            controlScript.wizard_desiredCaptainName = "Cpt. Blackhammer";
        }
        else if (captainSelectionDropdown.value.Equals(1))
        {
            controlScript.wizard_desiredCaptainName = "Cpt. Sparrow";

        }
    }

    void ConfirmSelectionButton() // CONFIRM MAIN MENU WIZARD
    {
        lockWizardDesiredChoices();
        controlScript.gameModeStatus = "setup field";
       
    }

    void UltimateButton()
    {
        if (playerShipScript.ultimateValueCharge >= 100f)
        {
            audioSource.PlayOneShot(ultimate_1_Sound); 
            ultimateProgressCircle.GetComponent<ProgressRadialBehaviour>().DecrementValue(100f);
            playerShipScript.ultimateValueCharge = 0f;
            

            if (playerShipScript.ultimateName.Equals("instant repair")){

                repairPlayerShip(5000f);

            } else if(playerShipScript.ultimateName.Equals("cannon burst"))
            {
                ShootLeftButton();
                ShootRightButton();

            }

        }

    }

    void repairEnemyShip(float repairValue)
    {

        enemyAIShipScript.hitPoints += repairValue;

        if (enemyAIShipScript.hitPoints > enemyAIShipScript.maxHitPoints)
        {
            enemyAIShipScript.hitPoints = enemyAIShipScript.maxHitPoints;
        }
    }

    void repairPlayerShip(float repairValue)
    {
            Debug.Log("repairing for: " + repairValue);
            playerShipScript.hitPoints += repairValue;

        if (playerShipScript.hitPoints > playerShipScript.maxHitPoints)
        {
            playerShipScript.hitPoints = playerShipScript.maxHitPoints;
        }
 
   
    }

    void RepairButton() // REPAIR FUNCTION
    {
        if (playerShipScript.repairCharge >= 100f)
        {
            audioSource.PlayOneShot(repairSound);
            ultimateProgressCircle.GetComponent<ProgressRadialBehaviour>().IncrementValue(playerShipScript.defensiveAbilityUltimateContribution);
            playerShipScript.ultimateValueCharge += playerShipScript.defensiveAbilityUltimateContribution;
            RepairCooldownProgressCircle.GetComponent<ProgressRadialBehaviour>().DecrementValue(playerShipScript.repairCharge);
            playerShipScript.repairCharge = 0f;
                repairPlayerShip(playerShipScript.repairValue);
   

            

        }

    }


    void ShootLeftButton() // SHOOT LEFT FUNCTION
    {
       

        if (playerShipScript.shootLeftCharge >= 100f)
        {
            audioSource.PlayOneShot(cannonSound);
            ultimateProgressCircle.GetComponent<ProgressRadialBehaviour>().IncrementValue(playerShipScript.aggressiveAbilityUltimateContribution);
            playerShipScript.ultimateValueCharge += playerShipScript.aggressiveAbilityUltimateContribution;
            shootLeftCooldownProgressCircle.GetComponent<ProgressRadialBehaviour>().DecrementValue(playerShipScript.shootLeftCharge);
            playerShipScript.shootLeftCharge = 0f;

            float luckRoll = Random.Range(0f, 1f);    

            if (luckRoll <= playerShipScript.cannonAccuracy)
            {
                enemyAIShipScript.hitPoints -= playerShipScript.cannonDamage;

            }
           
        }
   
    }
  
    void BilgeButton() // BILGE FUNCTION
    {
        if (playerShipScript.bilgeCharge >= 100f) {
            audioSource.PlayOneShot(bilgeSound);
            ultimateProgressCircle.GetComponent<ProgressRadialBehaviour>().IncrementValue(playerShipScript.defensiveAbilityUltimateContribution);
            playerShipScript.ultimateValueCharge += playerShipScript.defensiveAbilityUltimateContribution;
            bilgeCooldownProgressCircle.GetComponent<ProgressRadialBehaviour>().DecrementValue(playerShipScript.bilgeCharge);
            playerShipScript.bilgeCharge = 0f;

            playerShipScript.waterLevel -= playerShipScript.bilgeValue;
            if (playerShipScript.waterLevel < 0)
            {
                playerShipScript.waterLevel = 0;
            }
        }

    }

    void ShootRightButton() // SHOOT RIGHT

    {
        if (playerShipScript.shootRightCharge >= 100f)
        {
            audioSource.PlayOneShot(cannonSound);
            ultimateProgressCircle.GetComponent<ProgressRadialBehaviour>().IncrementValue(playerShipScript.aggressiveAbilityUltimateContribution);
            playerShipScript.ultimateValueCharge += playerShipScript.aggressiveAbilityUltimateContribution;
            shootRightCooldownProgressCircle.GetComponent<ProgressRadialBehaviour>().DecrementValue(playerShipScript.shootRightCharge);
            playerShipScript.shootRightCharge = 0f;

            float luckRoll = Random.Range(0f, 1f);

            if (luckRoll <= playerShipScript.cannonAccuracy)
            {
                enemyAIShipScript.hitPoints -= playerShipScript.cannonDamage;

            }
        }
  
    }

    void fetchShipReferences()
    {
        if (playerShip == null)
        { // canvas waits until the player ship spawns

            playerShip = GameObject.Find("Player Ship (Sloop)(Clone)");
            if (playerShip == null)
            {
                playerShip = GameObject.Find("Player Ship (Frigate)(Clone)");
            }
            if (playerShip != null)
            {
                playerShipScript = playerShip.GetComponent<PlayerShipScript>();
            }
        }

        if (controlScript.activeEnemyShipEngagedReference != null)
        {

            enemyAIShipScript = controlScript.activeEnemyShipEngagedReference.GetComponent<shipAIScriptTugboat>();
        }
    } // fetch all ship references

    void doIntroSkip()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            gearworksSplash.CrossFadeAlpha(0, 3, false);
        }
    } // do intro skip

    void setAllStates_mainmenu()
    {

        bilgeButton.gameObject.SetActive(false);
        shootLeftButton.gameObject.SetActive(false);
        shootRightButton.gameObject.SetActive(false);
        repairButton.gameObject.SetActive(false);
        ultimateButton.gameObject.SetActive(false);

        ultimateProgressCircle.SetActive(false);

        youText.enabled = false;
        yourenemyText.enabled = false;
        confirmSelection.gameObject.SetActive(true);


        shipSelectionDropDown.gameObject.SetActive(true);
        captainSelectionDropdown.gameObject.SetActive(true);
        ultimateSelectionDropdown.gameObject.SetActive(true);
        factionSelectionDropDown.gameObject.SetActive(true);

        shootLeftCooldownProgressCircle.SetActive(false);
        shootRightCooldownProgressCircle.SetActive(false);
        bilgeCooldownProgressCircle.SetActive(false);
        RepairCooldownProgressCircle.SetActive(false);

    } // SET MAIN MENU STATES

    void setAllStates_sailing()
    {
        shipSelectionDropDown.gameObject.SetActive(false);
        captainSelectionDropdown.gameObject.SetActive(false);
        ultimateSelectionDropdown.gameObject.SetActive(false);
        factionSelectionDropDown.gameObject.SetActive(false);

        confirmSelection.gameObject.SetActive(false);

        youText.enabled = false;
        yourenemyText.enabled = false;


        bilgeButton.gameObject.SetActive(false);
        shootLeftButton.gameObject.SetActive(false);
        shootRightButton.gameObject.SetActive(false);
        repairButton.gameObject.SetActive(false);
        ultimateButton.gameObject.SetActive(false);

        ultimateProgressCircle.SetActive(false);

        shootLeftCooldownProgressCircle.SetActive(false);
        shootRightCooldownProgressCircle.SetActive(false);
        bilgeCooldownProgressCircle.SetActive(false);
        RepairCooldownProgressCircle.SetActive(false);
    } // SET SAILING STATES

    void setAllstates_fighting()
    {
        playerStatsDisplayText.enabled = false;
        yourenemyText.enabled = true;
        youText.enabled = true;
        bilgeButton.gameObject.SetActive(true);
        shootLeftButton.gameObject.SetActive(true);
        shootRightButton.gameObject.SetActive(true);
        repairButton.gameObject.SetActive(true);
        ultimateButton.gameObject.SetActive(true);
        ultimateProgressCircle.SetActive(true);
        shootLeftCooldownProgressCircle.SetActive(true);
        shootRightCooldownProgressCircle.SetActive(true);
        bilgeCooldownProgressCircle.SetActive(true);
        RepairCooldownProgressCircle.SetActive(true);
    } // SET FIGHTING STATES

    void checkGameOver()
    {
        if (playerShipScript.hitPoints <= 0)
        {
            //TBD: GAME OVER
            controlScript.gameModeStatus = "gameover";
        }
    } // check if player lost a fight

    void checkFightWon()
    {
        if (enemyAIShipScript.hitPoints <= 0)
        {
            controlScript.gameModeStatus = "sailing";
            Object.Destroy(controlScript.activeEnemyShipEngagedReference);
        }
    } // check if player won a fight
    

  
    void Update () { /// CONTROL CANVAS EVENTS

        fetchShipReferences();
        doIntroSkip();
        topDebugText.text = "Gamemode Status = " + controlScript.gameModeStatus;

        if (controlScript.gameModeStatus.Equals("main menu"))
        {
            setAllStates_mainmenu();
        }


        if (controlScript.gameModeStatus.Equals("sailing"))
        {
            setAllStates_sailing();
            playerStatsDisplayText.text = playerShipScript.captainName + "'s " + playerShipScript.shipName;

        }

        if (controlScript.gameModeStatus.Equals("fighting"))
        {

            checkGameOver();
            checkFightWon();
            setAllstates_fighting();
            youText.text = "You: " + playerShipScript.hitPoints.ToString() + "HP \b   WaterLVL = " + playerShipScript.waterLevel.ToString();
            yourenemyText.text = "Your Enemy: " + enemyAIShipScript.hitPoints + "HP - WaterLVL = " + enemyAIShipScript.waterLevel.ToString();


            float time = Time.deltaTime;

            AIrepairTimer += time; // AI repairs
            if (AIrepairTimer > enemyAIShipScript.repairRate)
            {
                AIrepairTimer = 0;
                repairEnemyShip(enemyAIShipScript.repairValue);
                Debug.Log("AI REPAIRS");

            }



            AIultimateTimer += time; // AI uses an ultimte
            if (AIultimateTimer > enemyAIShipScript.ultimateRate)
            {
                Debug.Log("AI USES ULTIMATe");
                AIultimateTimer = 0;
                if (enemyAIShipScript.ultimateType.Equals("instant repair"))
                {
                    repairEnemyShip(5000f);
                }
                else if (enemyAIShipScript.ultimateType.Equals("cannon burst"))
                {
                    playerShipScript.hitPoints -= 2f * enemyAIShipScript.cannonDmg;
                }
            }




            // AI Bilges (lowers his water lvl) tbd: sound and animation
            AibilgeTimer += time;
            if (AibilgeTimer > enemyAIShipScript.bilgeValue)
            {
                Debug.Log("AI BILGES");
                AibilgeTimer = 0f;
                enemyAIShipScript.waterLevel -= enemyAIShipScript.bilgeRate;
                if (enemyAIShipScript.waterLevel < 0)
                {
                    enemyAIShipScript.waterLevel = 0;
                }
            }



            /// AI fires cannon (you take cannon dmg from AI) tbd: sound and animation
            AIattackTimer += time;
            if (AIattackTimer > enemyAIShipScript.attackRate)
            {
                Debug.Log("AI SHOOTS");
                AIattackTimer = 0;
                playerShipScript.hitPoints -= enemyAIShipScript.cannonDmg;
                playerShipScript.waterLevel += Random.Range(0f, 30f);
            }


            /// GLOBAL you and AI take water dmg tbd: sound and animation, more water comes in

            waterLevelDamageTimer += time;

            if (waterLevelDamageTimer > 15)
            {
                Debug.Log("WATER DAMAGE IS TAKEN");
                waterLevelDamageTimer = 0;
                playerShipScript.hitPoints -= playerShipScript.waterLevel;
                enemyAIShipScript.hitPoints -= enemyAIShipScript.waterLevel;

                playerShipScript.waterLevel += playerShipScript.waterLevel * 0.8f;
                enemyAIShipScript.waterLevel -= enemyAIShipScript.waterLevel * 0.8f;

            }


            ////////////

            //////////////////////////
            /// recharge cooldowns!
            /// 
   
            RepairCooldownProgressCircle.GetComponent<ProgressRadialBehaviour>().IncrementValue(time * playerShipScript.repairRechargeRate);
            playerShipScript.repairCharge += time * playerShipScript.repairRechargeRate;
            ////
            shootLeftCooldownProgressCircle.GetComponent<ProgressRadialBehaviour>().IncrementValue(time * playerShipScript.shootLeftRechargeRate);
            playerShipScript.shootLeftCharge += time * playerShipScript.shootLeftRechargeRate;
            ////
            shootRightCooldownProgressCircle.GetComponent<ProgressRadialBehaviour>().IncrementValue(time * playerShipScript.shootRightRechargeRate);
            playerShipScript.shootRightCharge += time * playerShipScript.shootRightRechargeRate;
            /////
            bilgeCooldownProgressCircle.GetComponent<ProgressRadialBehaviour>().IncrementValue(time * playerShipScript.bilgeRechargeRate);
            playerShipScript.bilgeCharge += time * playerShipScript.bilgeRechargeRate;
            ////////////////////////////


}

    }   // Update
}