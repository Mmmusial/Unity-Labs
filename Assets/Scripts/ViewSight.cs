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
    public Ziemniak enemy;
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



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy)
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
                enemyNextAttack = Time.time + enemy.overallCooldown;
                int numberId = Random.Range(0, 2);
                Debug.Log(numberId);
                int attackValue = 0;
                float ShieldDefenceValue = witcher.ShieldBlockValue();
                float GuardDefenceValue = witcher.GuardDefenceValue();
                float attackReduction = witcher.DefenceValue();

                
                if (numberId == 0)
                {
                    attackValue = enemy.BlobAttack();

                }
                if (numberId == 1)
                {
                    attackValue = enemy.SplitAttack();
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
                    enemy.ChangeHealth(-attackValueW);
                    enemyReceivingDamage.setTextDamage(attackValueW);
                    enemyReceivingDamage.gameObject.SetActive(true);
                    
                }
                if (Input.GetKeyDown(KeyCode.F2) && Time.time > nextKickAttack)
                {
                    nextKickAttack = Time.time + witcher.kickAttackCooldown;
                    nextAttack = Time.time + witcher.overallCooldown;
                    attackValueW = witcher.KickAttack();
                    enemy.ChangeHealth(-attackValueW);
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
            if (enemy.health <= 0)
            {
                Debug.Log(cameraVecBeforeFight);
                camera.gameObject.transform.position = cameraVecBeforeFight;
                camera.gameObject.transform.eulerAngles = cameraEulBeforeFight;
                enemy.gameObject.SetActive(false);
                Destroy(enemy);
                enemy = null;
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
    }


    private void OnTriggerEnter(Collider other)
    {
        fightUI.gameObject.SetActive(true);
        if (other.name == "Ziemniak"){
            enemy = other.GetComponent<Collider>().gameObject.GetComponent<Ziemniak>();
        }
        Debug.Log(other.name);
        if (other.CompareTag("Monster"))//tutaj przeniesc do sceny walki
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

        }
        
       
    }

}
