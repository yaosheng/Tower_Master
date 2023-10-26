using UnityEngine;
using System.Collections;
[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    public Rigidbody playerRigid;
    private Vector3 speed;

    private Transform[] pillers;
    private Cannon[] cnArray;

    private Vector3 checkVector;
    private Quaternion rotation;

    private int startPoint = 0;
    public float rotateSpeed = 3.0f;
    public float walkSpeed;
    public float originalWalkSpeed;

    public ParticleSystem gunParticle;

    public int portalpointAmount = 4;
    public GameObject iceEffect;
    public GameObject trapEffect;
    public GameObject stoneEffect;

    //private int blood = 50;
    public int Blood = 0;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        originalWalkSpeed = walkSpeed;
        cnArray = Scene.cannonArray;
        pillers = Scene.pillersTransform;
        CheckWhereAmI();
    }

    void FixedUpdate()
    {
        PlayerWalkAndTurn();
        CheckBlood();
    }
    //檢查自身在路徑的何處
    void CheckWhereAmI()
    {
        Vector3 selfVector = this.transform.position;
        Vector3 wayPointVector1, wayPointVector2;
        float minDistence = Vector3.Distance(selfVector, pillers[0].localPosition);
        float checkDistence = 0;
        int checkPoint = 0;

        for (int j = 1; j < pillers.Length; j++)
        {
            checkDistence = Vector3.Distance(selfVector, pillers[j].localPosition);
            if(checkDistence < minDistence)
            {
                minDistence = checkDistence;
                checkPoint = j;
            }
        }
        if(checkPoint < pillers.Length - 1)
        {
            wayPointVector1 = pillers[checkPoint].localPosition - selfVector;
            wayPointVector2 = pillers[checkPoint + 1].localPosition - selfVector;

            if (Vector3.Dot(wayPointVector1, wayPointVector2) > 0)
            {
                startPoint = checkPoint;
            }
            else
            {
                startPoint = checkPoint + 1;
            }
        }
        else
        {
            startPoint = checkPoint;
        }
        playerRigid.transform.LookAt(new Vector3(pillers[startPoint].localPosition.x, playerRigid.transform.localPosition.y, pillers[startPoint].localPosition.z));
    }

    void PlayerWalkAndTurn()
    {
        if (this.tag == "FlyEnemy") {
            startPoint = Scene.groundNumber;
            playerRigid.transform.LookAt(new Vector3(pillers[startPoint].localPosition.x, playerRigid.transform.localPosition.y, pillers[startPoint].localPosition.z));
        }
        
        checkVector = new Vector3(pillers[startPoint].localPosition.x, playerRigid.transform.localPosition.y, pillers[startPoint].localPosition.z);
        if (Vector3.Distance(playerRigid.transform.localPosition, checkVector) > 0.3f)
        {
            speed = playerRigid.transform.localPosition + walkSpeed * playerRigid.transform.forward * Time.deltaTime;
            playerRigid.MovePosition(speed);
        }
        else
        {
            //Debug.Log(Vector3.Distance(playerRigid.transform.localPosition, checkVector));
            if (startPoint < pillers.Length - 1)
            {
                startPoint++;
            }
        }
        rotation = Quaternion.LookRotation(pillers[startPoint].localPosition - playerRigid.transform.localPosition, Vector3.up);
        rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        playerRigid.MoveRotation(rotation);
    }

    void CheckBlood()
    {
        if(Blood <= 0)
        {
            //Debug.Log(this.gameObject.tag + "  dead");
            for(int i = 0;i < cnArray.Length; i++)
            {
                cnArray[i].readytoFightList.Remove(this.gameObject);
            }
            this.gameObject.SetActive(false);
        }
    }
    IEnumerator HitBullet(ParticleSystem ps)
    {
        yield return new WaitForSeconds(0.5f);
        ps.gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            //Debug.Log("hit by bullet");
            ParticleSystem ps = Instantiate(gunParticle) as ParticleSystem;
            ps.transform.parent = this.transform;
            ps.transform.position = other.transform.position;
            ps.transform.localScale = Vector3.one;
            ps.Play();
            StartCoroutine(HitBullet(ps));
        }
        else if (other.tag == "Portal")
        {
            Transform pt = other.transform;
            int thisPoint = 0;

            for(int i = 0; i < pillers.Length;i++)
            {
                if (pt.position.x == pillers[i].position.x && pt.position.z == pillers[i].position.z)
                {
                    Debug.Log("transform :" + i);
                    thisPoint = i;
                }
            }
            if(thisPoint + portalpointAmount <= Scene.groundNumber)
            {
                this.transform.position = pillers[thisPoint + portalpointAmount].position;
            }
            else
            {
                this.transform.position = new Vector3(pillers[Scene.groundNumber].position.x, this.transform.position.y, pillers[Scene.groundNumber].position.z);
            }
            CheckWhereAmI();
        }
        else if (other.tag == "IceTrap" && this.gameObject.tag != "Zombie")
        {
            Debug.Log("hit Icetrap");
            GameObject ps = Instantiate(iceEffect) as GameObject;
            ps.transform.parent = this.transform;
            ps.transform.localPosition = new Vector3(0, 0.2f, 0);
            ps.transform.localScale = Vector3.one;
        }
        else if (other.tag == "Trap" && this.gameObject.tag != "Zombie")
        {
            Debug.Log("hit Trap");
            GameObject ps = Instantiate(trapEffect) as GameObject;
            ps.transform.parent = this.transform;
            ps.transform.localPosition = new Vector3(0, 0.2f, 0);
            ps.transform.localScale = Vector3.one;
        }
        else if (other.tag == "Stone" && this.gameObject.tag != "Zombie")
        {
            Debug.Log("hit Stone");
            GameObject ps = Instantiate(stoneEffect) as GameObject;
            ps.transform.parent = this.transform;
            ps.transform.position = this.transform.position;
            ps.transform.localScale = Vector3.one;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "IceTrap" && this.gameObject.tag != "Zombie")
        {
            for(int i = 0;i < this.transform.childCount; i++)
            {
                if(this.transform.GetChild(i).tag == "IceEffect")
                {
                    this.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}