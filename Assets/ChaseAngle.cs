using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAngle : MonoBehaviour {

	public Transform player;
	static Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 direction = player.position - transform.position;
		float angle = Vector3.Angle(direction,transform.forward);
		if(Vector3.Distance(player.position, transform.position) < 10 && angle < 45)
		{

			direction.y = 0;

            if (!anim.GetBool("isAttacking"))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
					Quaternion.LookRotation(direction, Vector3.up), 0.1f);
            }


            anim.SetBool("isIdle",false);
			if(direction.magnitude > 5)
			{
				transform.Translate(0,0,0.05f);
				anim.SetBool("isWalking",true);
				anim.SetBool("isAttacking",false);
			}
			else
			{
				anim.SetBool("isAttacking",true);
				anim.SetBool("isWalking",false);
			}

		}
		else 
		{
			anim.SetBool("isIdle", true);
			anim.SetBool("isWalking", false);
			anim.SetBool("isAttacking", false);
		}

	}







	// Rotates the character (Y axis only) to face the given position 
	void RotateTowards(Vector3 position)
	{
		Vector3 direction = position - transform.position;

		// Get rid of the Y axis variations
		direction.y = 0.0f;

		// Check if the character's rotation is 
		// already facing the given position. Include a little damping.
		if (direction.magnitude < 0.1f)
		{
			return;
		}

		float rotateSpeed = 180f;
		transform.rotation = Quaternion.Slerp(transform.rotation,
			Quaternion.LookRotation(direction), 
			rotateSpeed * Time.deltaTime);

		transform.eulerAngles = new Vector3(0, 
			transform.eulerAngles.y, 0); 
	}
}
