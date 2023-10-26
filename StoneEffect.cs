using UnityEngine;
using System.Collections;

public class StoneEffect : MonoBehaviour {

    public GameObject stoneEffect;
    private Vector3 v1, v2;

    void Start()
    {
        v1 = new Vector3(this.transform.position.x, 0, this.transform.position.z);
    }


    void FixedUpdate()
    {
        if (Scene.gameStatus == Scene.GameStatus.Attack)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }

        foreach (GameObject go in Scene.enemies)
        {
            if (go.tag != "Zombie" && go.tag != "FlyEnemy")
            {
                v2 = new Vector3(go.transform.position.x, 0, go.transform.position.z);
                if (Vector3.Distance(v2, v1) < 0.3f)
                {
                    Debug.Log("stone now");
                    GameObject df = Instantiate(stoneEffect) as GameObject;
                    df.transform.parent = this.transform.parent;
                    df.transform.localPosition = new Vector3(this.transform.localPosition.x, 7.0f, this.transform.localPosition.z);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
