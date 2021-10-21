using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewSight : MonoBehaviour
{
    public Witcher witcher;
    public Rigidbody rigbod;
    public Mover mover;
    public Camera camera;
    public Ziemniak ziemniakEnemy;
    public Paluch paluchEnemy;
    public WinnaBaba winnaBabaEnemy;
    public Elf elfProgrammer;
    public Vector3 cameraVecBeforeFight;
    public Vector3 cameraEulBeforeFight;
    public Vector3 witcherVecBeforeFight;
    private float nextPunchAttack = 0.0f;
    private float nextKickAttack = 0.0f;
    private float nextAttack = 0.0f;
    private float enemyNextAttack = 0.0f;
    private float nextDefence = 0.0f;
    private float nextGuardDefence = 0.0f;
    private float endGuardDefence = 99999.0f;
    private float endShowingEnemyReceivingDamage = 99999.0f;
    private float endShowingWitcherReceivingDamage = 99999.0f;
    public EndGameMenu endGameMenu;
    public FightUI fightUI;
    public WitcherReceivingDamage witcherReceivingDamage;
    public EnemyReceivingDamage enemyReceivingDamage;
    public string actualdialogue;
    public int elfChatIndex;
    public int witcherChatIndex;
    public bool witcherTalking;
    public int witcherDialogueOption;
    public int elfDialogueOption;



    void Start()
    {
        actualdialogue = "Jestem głodnym elfem programistą, popracuje dla cb za co nieco";
        elfChatIndex = 0;
        witcherChatIndex = 0;
        witcherTalking = false;
        witcherDialogueOption = 0;
}

    // Update is called once per frame
    void Update()
    {
        #region ziemniakEnemyFight
        if (ziemniakEnemy)
        {
            if (Time.time > endShowingWitcherReceivingDamage)
            {
                witcherReceivingDamage.gameObject.SetActive(false);
            }
            if (Time.time > endShowingEnemyReceivingDamage)
            {
                enemyReceivingDamage.gameObject.SetActive(false);
            }
            if (Time.time > enemyNextAttack)
            {
                enemyNextAttack = Time.time + ziemniakEnemy.overallCooldown;
                int numberId = Random.Range(0, 2);
                Debug.Log(numberId);
                int attackValue = 0;
                float ShieldDefenceValue = witcher.ShieldBlockValue();
                float GuardDefenceValue = witcher.GuardDefenceValue();
                float attackReduction = witcher.DefenceValue();

                
                if (numberId == 0)
                {
                    attackValue = ziemniakEnemy.BlobAttack();

                }
                if (numberId == 1)
                {
                    attackValue = ziemniakEnemy.SplitAttack();
                }
                Debug.Log(attackReduction);

                witcher.ChangeHealth((int)(-(attackValue - (attackValue * attackReduction))));
                witcherReceivingDamage.setTextDamage(attackValue -(attackValue * attackReduction));
                endShowingWitcherReceivingDamage = Time.time + 1.0f;
                witcherReceivingDamage.gameObject.SetActive(true);
            }
            if (Time.time > nextAttack)
            {
                int attackValueW = 0;
                if (Input.GetKeyDown(KeyCode.F1) && Time.time > nextPunchAttack)
                {
                    nextPunchAttack = Time.time + witcher.punchAttackCooldown;
                    nextAttack = Time.time + witcher.overallCooldown;
                    attackValueW = witcher.PunchAttack();
                    ziemniakEnemy.ChangeHealth(-attackValueW);
                    enemyReceivingDamage.setTextDamage(attackValueW);
                    enemyReceivingDamage.gameObject.SetActive(true);
                    
                }
                if (Input.GetKeyDown(KeyCode.F2) && Time.time > nextKickAttack)
                {
                    nextKickAttack = Time.time + witcher.kickAttackCooldown;
                    nextAttack = Time.time + witcher.overallCooldown;
                    attackValueW = witcher.KickAttack();
                    ziemniakEnemy.ChangeHealth(-attackValueW);
                    enemyReceivingDamage.setTextDamage(attackValueW);
                    enemyReceivingDamage.gameObject.SetActive(true);
                    
                }
                endShowingEnemyReceivingDamage = Time.time + 1.0f;
            }
            if(Time.time > endGuardDefence)
            {
                witcher._isGuardDefenceActive = false;
            }
            if (Time.time > nextDefence)
            {
                if (Input.GetKeyDown(KeyCode.D) && Time.time > nextGuardDefence)
                {
                    nextGuardDefence = Time.time + witcher.guardDefenceCooldown;
                    nextDefence = Time.time + witcher.guardDefenceCooldown;
                    witcher._isGuardDefenceActive = true;
                    endGuardDefence = Time.time + witcher.guardDefenceDuration;
                }
            }
            if (ziemniakEnemy.health <= 0)
            {
                Debug.Log(cameraVecBeforeFight);
                camera.gameObject.transform.position = cameraVecBeforeFight;
                camera.gameObject.transform.eulerAngles = cameraEulBeforeFight;
                ziemniakEnemy.gameObject.SetActive(false);
                Destroy(ziemniakEnemy);
                ziemniakEnemy = null;
                witcher.gameObject.transform.position = witcherVecBeforeFight;
                cameraVecBeforeFight = new Vector3(0, 0, 0);
                enemyReceivingDamage.gameObject.SetActive(false);
                witcherReceivingDamage.gameObject.SetActive(false);

            }
            if (witcher.health <=0)
            {
                endGameMenu.gameObject.SetActive(true);

            }
        }
        #endregion

        #region paluchEnemyFight
        if (paluchEnemy)
        {
            if (Time.time > endShowingWitcherReceivingDamage)
            {
                witcherReceivingDamage.gameObject.SetActive(false);
            }
            if (Time.time > endShowingEnemyReceivingDamage)
            {
                enemyReceivingDamage.gameObject.SetActive(false);
            }
            if (Time.time > enemyNextAttack)
            {
                enemyNextAttack = Time.time + paluchEnemy.overallCooldown;
                int numberId = Random.Range(0, 2);
                Debug.Log(numberId);
                int attackValue = 0;
                float ShieldDefenceValue = witcher.ShieldBlockValue();
                float GuardDefenceValue = witcher.GuardDefenceValue();
                float attackReduction = witcher.DefenceValue();


                if (numberId == 0)
                {
                    attackValue = paluchEnemy.SwordAttack();

                }
                if (numberId == 1)
                {
                    attackValue = paluchEnemy.QuickStabAttack();
                }
                Debug.Log(attackReduction);

                witcher.ChangeHealth((int)(-(attackValue - (attackValue * attackReduction))));
                witcherReceivingDamage.setTextDamage(attackValue - (attackValue * attackReduction));
                endShowingWitcherReceivingDamage = Time.time + 1.0f;
                witcherReceivingDamage.gameObject.SetActive(true);
            }
            if (Time.time > nextAttack)
            {
                int attackValueW = 0;
                if (Input.GetKeyDown(KeyCode.F1) && Time.time > nextPunchAttack)
                {
                    nextPunchAttack = Time.time + witcher.punchAttackCooldown;
                    nextAttack = Time.time + witcher.overallCooldown;
                    attackValueW = witcher.PunchAttack();
                    paluchEnemy.ChangeHealth(-attackValueW);
                    enemyReceivingDamage.setTextDamage(attackValueW);
                    enemyReceivingDamage.gameObject.SetActive(true);

                }
                if (Input.GetKeyDown(KeyCode.F2) && Time.time > nextKickAttack)
                {
                    nextKickAttack = Time.time + witcher.kickAttackCooldown;
                    nextAttack = Time.time + witcher.overallCooldown;
                    attackValueW = witcher.KickAttack();
                    paluchEnemy.ChangeHealth(-attackValueW);
                    enemyReceivingDamage.setTextDamage(attackValueW);
                    enemyReceivingDamage.gameObject.SetActive(true);

                }
                endShowingEnemyReceivingDamage = Time.time + 1.0f;
            }
            if (Time.time > endGuardDefence)
            {
                witcher._isGuardDefenceActive = false;
            }
            if (Time.time > nextDefence)
            {
                if (Input.GetKeyDown(KeyCode.D) && Time.time > nextGuardDefence)
                {
                    nextGuardDefence = Time.time + witcher.guardDefenceCooldown;
                    nextDefence = Time.time + witcher.guardDefenceCooldown;
                    witcher._isGuardDefenceActive = true;
                    endGuardDefence = Time.time + witcher.guardDefenceDuration;
                }
            }
            if (paluchEnemy.health <= 0)
            {
                Debug.Log(cameraVecBeforeFight);
                camera.gameObject.transform.position = cameraVecBeforeFight;
                camera.gameObject.transform.eulerAngles = cameraEulBeforeFight;
                paluchEnemy.gameObject.SetActive(false);
                Destroy(paluchEnemy);
                paluchEnemy = null;
                witcher.gameObject.transform.position = witcherVecBeforeFight;
                cameraVecBeforeFight = new Vector3(0, 0, 0);
                enemyReceivingDamage.gameObject.SetActive(false);
                witcherReceivingDamage.gameObject.SetActive(false);

            }
            if (witcher.health <= 0)
            {
                endGameMenu.gameObject.SetActive(true);

            }
        }
        #endregion

        #region winnaBabaEnemyFight
        if (winnaBabaEnemy)
        {
            if (Time.time > endShowingWitcherReceivingDamage)
            {
                witcherReceivingDamage.gameObject.SetActive(false);
            }
            if (Time.time > endShowingEnemyReceivingDamage)
            {
                enemyReceivingDamage.gameObject.SetActive(false);
            }
            if (Time.time > enemyNextAttack)
            {
                enemyNextAttack = Time.time + winnaBabaEnemy.overallCooldown;
                int numberId = Random.Range(0, 2);
                Debug.Log(numberId);
                int attackValue = 0;
                float ShieldDefenceValue = witcher.ShieldBlockValue();
                float GuardDefenceValue = witcher.GuardDefenceValue();
                float attackReduction = witcher.DefenceValue();


                if (numberId == 0)
                {
                    attackValue = winnaBabaEnemy.HandAttack();

                }
                if (numberId == 1)
                {
                    attackValue = winnaBabaEnemy.HighkickAttack();
                }
                Debug.Log(attackReduction);

                witcher.ChangeHealth((int)(-(attackValue - (attackValue * attackReduction))));
                witcherReceivingDamage.setTextDamage(attackValue - (attackValue * attackReduction));
                endShowingWitcherReceivingDamage = Time.time + 1.0f;
                witcherReceivingDamage.gameObject.SetActive(true);
            }
            if (Time.time > nextAttack)
            {
                int attackValueW = 0;
                if (Input.GetKeyDown(KeyCode.F1) && Time.time > nextPunchAttack)
                {
                    nextPunchAttack = Time.time + witcher.punchAttackCooldown;
                    nextAttack = Time.time + witcher.overallCooldown;
                    attackValueW = witcher.PunchAttack();
                    winnaBabaEnemy.ChangeHealth(-attackValueW);
                    enemyReceivingDamage.setTextDamage(attackValueW);
                    enemyReceivingDamage.gameObject.SetActive(true);

                }
                if (Input.GetKeyDown(KeyCode.F2) && Time.time > nextKickAttack)
                {
                    nextKickAttack = Time.time + witcher.kickAttackCooldown;
                    nextAttack = Time.time + witcher.overallCooldown;
                    attackValueW = witcher.KickAttack();
                    winnaBabaEnemy.ChangeHealth(-attackValueW);
                    enemyReceivingDamage.setTextDamage(attackValueW);
                    enemyReceivingDamage.gameObject.SetActive(true);

                }
                endShowingEnemyReceivingDamage = Time.time + 1.0f;
            }
            if (Time.time > endGuardDefence)
            {
                witcher._isGuardDefenceActive = false;
            }
            if (Time.time > nextDefence)
            {
                if (Input.GetKeyDown(KeyCode.D) && Time.time > nextGuardDefence)
                {
                    nextGuardDefence = Time.time + witcher.guardDefenceCooldown;
                    nextDefence = Time.time + witcher.guardDefenceCooldown;
                    witcher._isGuardDefenceActive = true;
                    endGuardDefence = Time.time + witcher.guardDefenceDuration;
                }
            }
            //Debug.Log("Winna baba HP: " + (winnaBabaEnemy.health).ToString());
            //Debug.Log("witcher baba HP: " + (witcher.health).ToString());

            if (winnaBabaEnemy.health <= 0)
            {
                Debug.Log(cameraVecBeforeFight);
                camera.gameObject.transform.position = cameraVecBeforeFight;
                camera.gameObject.transform.eulerAngles = cameraEulBeforeFight;
                winnaBabaEnemy.gameObject.SetActive(false);
                //Destroy(winnaBabaEnemy);
                winnaBabaEnemy = null;
                witcher.gameObject.transform.position = witcherVecBeforeFight;
                cameraVecBeforeFight = new Vector3(0, 0, 0);
                enemyReceivingDamage.gameObject.SetActive(false);
                witcherReceivingDamage.gameObject.SetActive(false);

            }
            if (witcher.health <= 0)
            {
                endGameMenu.gameObject.SetActive(true);

            }
        }
        #endregion

        #region Elf Trading
        if (elfProgrammer)
        {
            //dialog z elfem
            //Debug.Log("Chat Index: " + witcherChatIndex.ToString() + " Option index: " + witcherDialogueOption.ToString());
            string[,] elfdialogue = new string[4, 3] {
                {"Super! jestem twoj","Spokojnie, juz odchodze","Chce siedem win, trzy paczki paluszkow i trzy frytek"},
                {"", "Hmm... kuszaca propozycja, zgadzam sie!", "" },
                {"", "","" },
                {" ", "", "" } };
            string[,] witcherdialogue = new string[4, 3] {
                {" F1. Chetnie cie zatrudnie, proponuje pięć win, dwie paczki paluszków i cztery frytek  F2.Nie chce z toba wspolpracowac   F3. zatrudnie cie mow ile chcesz",
                "", "" },
                {"Super, zaczynamy od jutra", "",""}, //odp na F1
                {"No raczej!", "",""},//odp na F2
                {"Oki biore", "",""} };//odp na F3
            if (witcherChatIndex == 1 && witcherDialogueOption == 0)
            {
                if (Input.GetKeyDown(KeyCode.F1))
                {
                    elfDialogueOption = 0;
                    elfChatIndex = 0;
                    witcherDialogueOption = 1;
                    witcherChatIndex = 0;
                }
                if (Input.GetKeyDown(KeyCode.F2))
                {
                    elfDialogueOption = 0;
                    elfChatIndex = 1;
                    witcherDialogueOption = 2;
                    witcherChatIndex = 0;
                }
                if (Input.GetKeyDown(KeyCode.F3))
                {
                    elfDialogueOption = 0;
                    elfChatIndex = 2;
                    witcherDialogueOption = 3;
                    witcherChatIndex = 0;
                }

                if (Input.GetKeyDown(KeyCode.F2) || Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.F3))
                {
                    if (witcherTalking)
                    {
                        Debug.Log(witcherChatIndex);
                        actualdialogue = elfdialogue[elfDialogueOption, elfChatIndex];
                    }
                    else
                    {
                        actualdialogue = witcherdialogue[witcherDialogueOption, witcherChatIndex];
                    }
                    witcherTalking = !witcherTalking;
                }

            }
            if (Input.GetKeyDown(KeyCode.P)){

                if (witcherTalking)
                {
                    Debug.Log(witcherChatIndex);
                    actualdialogue = elfdialogue[elfDialogueOption,elfChatIndex];
                }
                else
                {
                    actualdialogue = witcherdialogue[witcherDialogueOption, witcherChatIndex++];
                }
                witcherTalking = !witcherTalking;
            }
            Debug.Log(actualdialogue);

        }

        #endregion
    }


    private void OnTriggerEnter(Collider other)
    {

        ziemniakEnemy = other.GetComponent<Ziemniak>();
        paluchEnemy = other.GetComponent<Paluch>();
        winnaBabaEnemy = other.GetComponent<WinnaBaba>();
        elfProgrammer = other.GetComponent<Elf>();

        IMonseterFighter monseterFighter = other.GetComponent<IMonseterFighter>();
        INegotitionReciever negotitionReciever = other.GetComponent<INegotitionReciever>();

        if (monseterFighter!=null)//tutaj przeniesc do sceny walki
        {
            if (cameraVecBeforeFight[0] == 0)
            {
                cameraVecBeforeFight = new Vector3(camera.gameObject.transform.position.x, camera.gameObject.transform.position.y, camera.gameObject.transform.position.z);
                cameraEulBeforeFight = new Vector3(camera.gameObject.transform.eulerAngles.x, camera.gameObject.transform.eulerAngles.y, camera.gameObject.transform.eulerAngles.z);
                witcherVecBeforeFight = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            }
            Debug.Log(other.GetComponent<Collider>().gameObject.GetComponent<Collider>());
            witcher.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z - 12);
            witcher.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            other.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            camera.gameObject.transform.position = new Vector3(witcher.gameObject.transform.position.x, witcher.gameObject.transform.position.y + 30, witcher.gameObject.transform.position.z - 30);
            camera.gameObject.transform.eulerAngles = new Vector3(20, 0, 0);


            if (FightUI.Instance.gameObject.activeSelf == false)
            {
                FightUI.Instance.beginFight(witcher, monseterFighter);
            }

            return;
        }

        if (negotitionReciever != null)//tutaj przeniesc do sceny negocjacji
        {
            if (cameraVecBeforeFight[0] == 0)
            {
                cameraVecBeforeFight = new Vector3(camera.gameObject.transform.position.x, camera.gameObject.transform.position.y, camera.gameObject.transform.position.z);
                cameraEulBeforeFight = new Vector3(camera.gameObject.transform.eulerAngles.x, camera.gameObject.transform.eulerAngles.y, camera.gameObject.transform.eulerAngles.z);
                witcherVecBeforeFight = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            }
            Debug.Log(other.GetComponent<Collider>().gameObject.GetComponent<Collider>());
            witcher.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z - 12);
            witcher.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            other.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            camera.gameObject.transform.position = new Vector3(witcher.gameObject.transform.position.x, witcher.gameObject.transform.position.y + 30, witcher.gameObject.transform.position.z - 30);
            camera.gameObject.transform.eulerAngles = new Vector3(20, 0, 0);

            NegotiationUI ui = NegotiationUI.Instance;
            Debug.Log(ui.ToString());
            if (ui.gameObject.activeSelf == false)
            {
                NegotiationUI.Instance.BeginNegotiation(negotitionReciever);
            }

            return;
        }




    }

}
