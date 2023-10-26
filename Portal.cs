using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    public int setInt = 0;
    public float timer = 0;

	void FixedUpdate () {
        timer += Time.deltaTime;
        if(timer >= 4.0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
