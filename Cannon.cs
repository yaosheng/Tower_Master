using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[DisallowMultipleComponent]
public class Cannon : MonoBehaviour {

    public float timeGap = 0.5f;
    //public Collider bullet;
    public Bullet bullet;
    public float distence = 3.5f;

    private Vector3 targetVector;
    private float rightTime;
    public Transform shootBase;
    private GameObject targetGameobject;
    public List<GameObject> readytoFightList = new List<GameObject>();
    private List<GameObject> tempList = new List<GameObject>();
    private Transform thisTransform;

    private int temp1 = 0;

    void Start() {
        rightTime = timeGap;
        thisTransform = this.transform;
    }

    void FixedUpdate() {
        CheckList();
        AddEnemyToFight();
        Fight();
    }

    void CheckList()
    {
        foreach (GameObject go in Scene.enemies)
        {
            if (go.activeSelf == false) tempList.Add(go);
        }
        foreach (GameObject go in readytoFightList)
        {
            if (go.activeSelf == false) tempList.Add(go);
        }
        foreach(GameObject go in tempList)
        {
            Scene.enemies.Remove(go);
            readytoFightList.Remove(go);
        }
    }

    void AddEnemyToFight()
    {
        if(Scene.enemies.Count > 0)
        {
            foreach (GameObject go in Scene.enemies)
            {
                if(go.activeSelf == false)
                {
                    continue;
                } 
                if (go.tag == "Enemy" || go.tag == "Player" || go.tag == "FlyEnemy" || go.tag == "Zombie")
                {
                    if (Vector3.Distance(thisTransform.position, go.transform.position) <= 2.5f)
                    {
                        foreach (GameObject go1 in readytoFightList)
                        {
                            if (go == go1) temp1++;
                        }
                        if (temp1 == 0)
                        {
                            readytoFightList.Add(go);
                        }
                        temp1 = 0;
                    }
                    else
                    {
                        foreach (GameObject go1 in readytoFightList)
                        {
                            if (go == go1) temp1++;
                        }
                        if (temp1 != 0)
                        {
                            readytoFightList.Remove(go);
                        }
                        temp1 = 0;
                    }
                }
                else continue;
            }
        }
    }

    void Fight()
    {
        rightTime += Time.deltaTime;
        if (rightTime >= timeGap)
        {
            if (readytoFightList.Count > 0)
            {
                foreach (GameObject go in readytoFightList)
                {
                    targetGameobject = go;
                    break;
                }
                //Debug.Log("target tag :" + targetGameobject.tag);
                Bullet cloneBullet = Instantiate(bullet) as Bullet;
                cloneBullet.transform.parent = thisTransform;
                cloneBullet.transform.localPosition = new Vector3(0, 0, 1.46f);
                cloneBullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Vector3 tempVector = targetGameobject.transform.position - shootBase.position;
                cloneBullet.shootDirection = new Vector3(tempVector.x, cloneBullet.transform.localPosition.y, tempVector.z);
            }

            rightTime = 0;
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Enemy" || other.tag == "Player" || other.tag == "FlyEnemy")
    //    {
    //        Debug.Log("trigger enter");
    //        readytoFightList.Add(other.gameObject);
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Enemy" || other.tag == "Player" || other.tag == "FlyEnemy")
    //    {
    //        Debug.Log("trigger exit");
    //        readytoFightList.Remove(other.gameObject);
    //    }
    //}

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Enemy" || other.tag == "Player" || other.tag == "FlyEnemy")
    //    {
    //        //trigStayList.Add(other.gameObject);
    //        //foreach (GameObject go in trigEnterList)
    //        //{
    //        //    if (go == other.gameObject) temp1++;
    //        //}
    //        //if (temp1 == 0)
    //        //{
    //        //    Debug.Log("on trigger stay and No trigger enter");
    //        //    trigStayList.Add(other.gameObject);
    //        //}
    //        //else
    //        //{
    //        //    temp1 = 0;
    //        //}

    //        if (rightTime >= timeGap)
    //        {
    //            if (readytoFightList.Count > 0)
    //            {
    //                foreach (GameObject cd in readytoFightList)
    //                {
    //                    targetGameobject = cd;
    //                    break;
    //                }
    //                //Debug.Log("target tag :" + targetGameobject.tag);
    //                Bullet cloneBullet = Instantiate(bullet) as Bullet;
    //                cloneBullet.transform.parent = this.transform;
    //                cloneBullet.transform.localPosition = new Vector3(0, 0, 1.46f);
    //                cloneBullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    //                cloneBullet.shootDirection = targetGameobject.transform.position - shootBase.position;
    //            }

    //            rightTime = 0;
    //        }
    //    }
    //}


}

