using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    public int trapNumber = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            //Debug.Log("hit trap");
            other.gameObject.GetComponent<Player>().Blood -= trapNumber;
        }
    }
}
