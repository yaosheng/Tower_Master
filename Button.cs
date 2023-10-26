using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Button : MonoBehaviour {

    public GameObject player;
    public GameObject monster1;
    public GameObject monster2;
    public GameObject monster3;
    public GameObject monster4;
    public GameObject monster5;
    public GameObject monster6;
    public Transform chParent;

    private GameObject monster;
    public string uiName;
    public UIManager bm;

    void Start()
    {
        uiName = this.name;
        Debug.Log("uiName :" + uiName);
        if(uiName == "btn_fight")
        {
            this.GetComponent<Image>().enabled = false;
        }
    }

    public void CheckUI()
    {
        if (Scene.gameStatus == Scene.GameStatus.Attack)
        {
            if (uiName == "btn_fight")
            {
                this.GetComponent<Image>().enabled = true;
            }
        }
    }

    public void CreatSomeThing()
    {
        switch (uiName)
        {
            case "btn_monster1":
                CreatMonster(monster1);
                break;
            case "btn_monster2":
                CreatMonster(monster2);
                break;
            case "btn_monster3":
                CreatMonster(monster3);
                break;
            case "btn_monster4":
                CreatMonster(monster4);
                break;
            case "btn_monster5":
                CreatMonster(monster5);
                break;
            case "btn_monster6":
                CreatMonster(monster6);
                break;
            case "player":
                CreatMonster(player);
                break;
            case "btn_weapon":
                if(Scene.isSetGround == true && Scene.gameStatus == Scene.GameStatus.Defense)
                {
                    Debug.Log("isSetTower");
                    Scene.setStatus = Scene.SetStatus.Tower;
                }
                break;
            case "btn_trap":
                if (Scene.isSetGround == true && Scene.gameStatus == Scene.GameStatus.Defense)
                {
                    Debug.Log("isSetTrap");
                    Scene.setStatus = Scene.SetStatus.Trap;
                }
                break;
            case "btn_ice":
                if (Scene.isSetGround == true && Scene.gameStatus == Scene.GameStatus.Defense)
                {
                    Debug.Log("isSetTrap");
                    Scene.setStatus = Scene.SetStatus.Ice;
                }
                break;
            case "btn_dropdown":
                if (Scene.isSetGround == true && Scene.gameStatus == Scene.GameStatus.Defense)
                {
                    Debug.Log("isSetDropDown");
                    Scene.setStatus = Scene.SetStatus.dropDown;
                }
                break;
            case "btn_stone":
                if (Scene.isSetGround == true && Scene.gameStatus == Scene.GameStatus.Defense)
                {
                    Debug.Log("isStone");
                    Scene.setStatus = Scene.SetStatus.Stone;
                }
                break;
            case "btn_reset":
                Reset();
                Scene.isReset = true;
                break;
            case "btn_defence":
                if (Scene.isSetGround == true && Scene.gameStatus == Scene.GameStatus.Defense)
                {
                    Debug.Log("attack");
                    Scene.gameStatus = Scene.GameStatus.Attack;
                    //if (Scene.cnstepNumber == 0 && Scene.tpstepNumber == 0)
                    //{

                    //}
                }

                break;
            case "btn_fight":

                break;
            case "btn_portal":
                Debug.Log("isSetPortal :" + Scene.isSetPortal);
                Scene.isSetPortal = true;
                break;
        }
    }

    public void CreatMonster(GameObject monster)
    {
        if(Scene.gameStatus == Scene.GameStatus.Attack)
        {
            if(uiName == "btn_monster1" && Scene.mon1_leftNumber > 0)
            {
                BurnMonster(monster);
                Scene.mon1_leftNumber--;
                bm.ShowText();
            }
            else if(uiName == "btn_monster2" && Scene.mon2_leftNumber > 0) {
                BurnMonster(monster);
                Scene.mon2_leftNumber--;
                bm.ShowText();
            }
            else if (uiName == "btn_monster3" && Scene.mon3_leftNumber > 0)
            {
                BurnMonster(monster);
                Scene.mon3_leftNumber--;
                bm.ShowText();
            }
            else if (uiName == "btn_monster4" && Scene.mon4_leftNumber > 0)
            {
                BurnMonster(monster);
                Scene.mon4_leftNumber--;
                bm.ShowText();
            }
            else if (uiName == "btn_monster5" && Scene.mon5_leftNumber > 0)
            {
                BurnMonster(monster);
                Scene.mon5_leftNumber--;
                bm.ShowText();
            }
        }
    }

    void BurnMonster(GameObject mon)
    {
        Transform bornedPoint = Scene.startPoint;
        GameObject monsterClone = Instantiate(mon) as GameObject;
        monsterClone.transform.parent = chParent;
        monsterClone.transform.position = bornedPoint.position;
        if(monsterClone.tag == "FlyEnemy" || monsterClone.tag == "Zombie")
        {
            monsterClone.transform.localScale = Vector3.one * 2;
        }
        else if (monsterClone.tag == "Enemy" || monsterClone.tag == "Player")
        {
            monsterClone.transform.localScale = Vector3.one;
        }

        Scene.enemies.Add(monsterClone);
    }

    void Reset()
    {
        int childInt = chParent.childCount;
        for (int i = 0; i < childInt; i++)
        {
            Destroy(chParent.GetChild(i).gameObject);
        }
    }
}
