using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {
	public GameObject[] wayPoints;
	public int currentPoint = 0;
	public float moveSpeed = 1;

	private Vector3 nextPoint;

	// Use this for initialization
	void Start () {
		if (currentPoint >= wayPoints.Length)
			currentPoint = 0;
		if (wayPoints.Length >= 2 && currentPoint < wayPoints.Length)
			nextPoint = wayPoints [currentPoint].transform.position;
//		GetComponent<Animator> ().SetBool ("isWalking", true);
	}
	
	// Update is called once per frame
	void Update () {
		if (wayPoints.Length >= 2) {
			float dist = Vector3.Distance (transform.position, nextPoint);
			if (dist < 0.1f) {
				currentPoint++;
				if (currentPoint >= wayPoints.Length)
					currentPoint = 0;
				nextPoint = wayPoints [currentPoint].transform.position;
			}
			Vector3 direction = nextPoint - transform.position;
			direction.y = 0f;

			print (transform.position.y);
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction, Vector3.up), 0.1f);
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime);
            //transform.position = Vector3.Lerp(transform.position, nextPoint, moveSpeed * Time.deltaTime / dist);
        }
	}
}
