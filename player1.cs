using UnityEngine;
using System.Collections;

public class player1 : MonoBehaviour
{
	private Animator anim;
	private Rigidbody playerRigid;
	public float walkSpeed = 0.5f;
	public float rotateSpeed = 0.5f;
	// if the distance is smaller than the threshold, find the next pillar 
	public float pillarThreshold = 0.3f;
	// always try to move toward the target pillar
	private Transform targetPillar;

	void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
		// Move forward
        Vector3 movement = transform.forward * walkSpeed * Time.deltaTime;
        playerRigid.MovePosition(transform.position + movement);
		if (targetPillar != null)
		{
			// Handle rotation
			Quaternion rotation = Quaternion.LookRotation(targetPillar.position - transform.position, Vector3.up);
			rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
			playerRigid.MoveRotation(rotation);

			// Move toward the next pillar when it's too close
			if (Vector3.Distance(transform.position, targetPillar.position) < pillarThreshold)
			{
				FindNextPillar();
			}
		}

		//anim.Play("Move");
		//anim.SetFloat("speed", 1);
		//AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
	}

	void FindNextPillar()
	{
		// FIXME
		// targetPillar = targetPillar.next;
	}
}