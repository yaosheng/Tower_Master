using UnityEngine;
using System.Collections;

public class StoneRoll : MonoBehaviour {

    public float roataeSpeed = 300.0f;

    void Start () {
	
	}
	
	void FixedUpdate () {
        transform.Rotate(Vector3.right * roataeSpeed * Time.deltaTime, Space.Self);
    }
}
