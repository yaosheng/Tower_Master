using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ice : MonoBehaviour {

    private float newSpeed;
    private float originalSpeed;
    private Dictionary<GameObject, float> enemySpeedDict1 = new Dictionary<GameObject, float>();
    private Dictionary<GameObject, float> enemySpeedDict2 = new Dictionary<GameObject, float>();

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            Debug.Log("hit ice");
            originalSpeed = other.gameObject.GetComponent<Player>().originalWalkSpeed;
            newSpeed = originalSpeed * 0.35f;
            if (!enemySpeedDict1.ContainsKey(other.gameObject))
            {
                enemySpeedDict1.Add(other.gameObject, originalSpeed);
            }
            if (!enemySpeedDict2.ContainsKey(other.gameObject))
            {
                enemySpeedDict2.Add(other.gameObject, newSpeed);
            }
            other.gameObject.GetComponent<Player>().walkSpeed = newSpeed;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (enemySpeedDict1.ContainsKey(other.gameObject))
        {
            foreach (KeyValuePair<GameObject, float> item in enemySpeedDict2)
            {
                item.Key.GetComponent<Player>().walkSpeed = item.Value;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (enemySpeedDict1.ContainsKey(other.gameObject))
        {
            foreach (KeyValuePair<GameObject, float> item in enemySpeedDict1)
            {
                item.Key.GetComponent<Player>().walkSpeed = item.Value;
            }
        }

    }
}
