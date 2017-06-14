using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roamer : MonoBehaviour {

    public Transform player;
    static Animator anim;

    public GameObject[] wayPoints;
    public int currentPoint = 0;
    public float moveSpeed = 1;

    private Vector3 nextPoint;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        if (currentPoint >= wayPoints.Length)
            currentPoint = 0;
        if (wayPoints.Length >= 2 && currentPoint < wayPoints.Length)
            nextPoint = wayPoints[currentPoint].transform.position;
        
        // for gizmo
        for (int i = 0; i < wayPoints.Length; i++)
            wayPoints[i].GetComponent<MeshRenderer>().material.color = Color.magenta;
        wayPoints[currentPoint].GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (Vector3.Distance(player.position, transform.position) < 10 && angle < 45)
        {
            direction.y = 0;

            if (!anim.GetBool("isAttacking"))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);
            }


            anim.SetBool("isIdle", false);
            if (direction.magnitude > 5)
            {
                transform.Translate(0, 0, 0.05f);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }
            else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
            }

        }
        else
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);

            if (wayPoints.Length >= 2)
            {
                float dist = Vector3.Distance(transform.position, nextPoint);
                if (dist < 0.1f)
                {
                    currentPoint++;
                    if (currentPoint >= wayPoints.Length)
                        currentPoint = 0;
                    nextPoint = wayPoints[currentPoint].transform.position;

                    // for gizmo
                    wayPoints[currentPoint].GetComponent<MeshRenderer>().material.color = Color.yellow;
                    if (currentPoint == 0)
                        wayPoints[wayPoints.Length-1].GetComponent<MeshRenderer>().material.color = Color.magenta;
                    else
                        wayPoints[currentPoint-1].GetComponent<MeshRenderer>().material.color = Color.magenta;

                }
                Vector3 dir = nextPoint - transform.position;
                dir.y = 0f;

                print(transform.position.y);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), 0.1f);
                transform.position = Vector3.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime);
                //transform.position = Vector3.Lerp(transform.position, nextPoint, moveSpeed * Time.deltaTime / dist);
            }
        }
    }

}
