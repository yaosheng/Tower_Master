using UnityEngine;
using System.Collections;
[DisallowMultipleComponent]
public class Bullet : MonoBehaviour {

    public Vector3 shootDirection;
    public float bulletSpeed = 5.0f;
    public int strNumber = 10;
    private float timer = 0;

    void Start () {
	
	}

	void FixedUpdate() {
        timer += Time.deltaTime;
        transform.Translate(shootDirection * bulletSpeed * Time.deltaTime);
        if(timer > 2.0f)
        {
            this.gameObject.SetActive(false);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Enemy" || other.tag == "FlyEnemy" || other.tag == "Zombie")
        {
            other.gameObject.GetComponent<Player>().Blood -= strNumber;
            this.gameObject.SetActive(false);
        }
    }
}
