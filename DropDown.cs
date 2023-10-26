using UnityEngine;
using System.Collections;

public class DropDown : MonoBehaviour {

    public GameObject dropEffect;
    private Vector3 v1, v2;

    void Start()
    {
        v1 = new Vector3(this.transform.position.x, 0, this.transform.position.z);
    }

	void FixedUpdate ()
    {
        if(Scene.gameStatus == Scene.GameStatus.Attack)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }

        foreach (GameObject go in Scene.enemies)
        {
            if(go.tag != "Zombie" && go.tag != "FlyEnemy")
            {
                v2 = new Vector3(go.transform.position.x, 0, go.transform.position.z);
                if (Vector3.Distance(v2, v1) < 0.2f)
                {
                    Debug.Log("drop now");
                    GameObject df = Instantiate(dropEffect) as GameObject;
                    df.transform.position = go.transform.position;
                    go.SetActive(false);
                    this.gameObject.SetActive(false);
                }
            }
        }	
	}
}
