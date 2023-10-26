using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {

    private float moveSpeed = 2.0f;
    public Vector3 forward;
    private Quaternion rotation;
    private Vector3 movePoint1, movePoint2;
    private int thisPoint = 0;
    private int tempInt;

    void Start () {
        tempInt = thisPoint;
        SetForward();
        transform.LookAt(forward, Vector3.up);
    }

    void FixedUpdate ()
    {
        movePoint1 = this.transform.localPosition;
        if(tempInt == thisPoint)
        {
            transform.LookAt(movePoint2, Vector3.up);
        }
        transform.position = Vector3.MoveTowards(movePoint1, movePoint2, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(movePoint1, movePoint2) < 0.3f)
        {
            if(thisPoint > 0)
            {
                thisPoint--;
            }
            forward = new Vector3(Scene.pillersTransform[thisPoint].position.x, this.transform.position.y, Scene.pillersTransform[thisPoint].position.z);
            movePoint2 = forward;
        }
        if(thisPoint == 0)
        {
            this.gameObject.SetActive(false);
        }
        rotation = Quaternion.LookRotation(movePoint2 - this.transform.localPosition, Vector3.up);
        rotation = Quaternion.Lerp(transform.rotation, rotation, 5 * Time.deltaTime);
        this.transform.rotation = rotation;
    }


    void SetForward()
    {
        for (int i = 0; i < Scene.pillersTransform.Length; i++)
        {
            if (this.transform.position.x == Scene.pillersTransform[i].position.x && this.transform.position.z == Scene.pillersTransform[i].position.z)
            {
                Debug.Log("transform :" + i);
                thisPoint = i;
            }
        }
        if (thisPoint != 0)
        {
            forward = new Vector3(Scene.pillersTransform[thisPoint - 1].position.x, this.transform.position.y, Scene.pillersTransform[thisPoint - 1].position.z);
            movePoint2 = new Vector3(Scene.pillersTransform[thisPoint].position.x, Scene.pillersTransform[thisPoint].position.y + 0.3f, Scene.pillersTransform[thisPoint].position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Flyenemy" && other.tag != "Zombie")
        {
            Debug.Log("hit by stone");
            if (other.gameObject.activeSelf == true)
            {
                other.GetComponent<Player>().Blood -= 10;
            }
        }
    }
}
