using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class OppControl : MonoBehaviour
{

    //public Transform[] runPoint;
    //public int nextPoint;
    private Animator anim;
    private bool finish,winner;
    private Rigidbody rb;
    [HideInInspector]
    public NavMeshAgent agent;
    public Transform finishPoint;
    public float speedTime;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    void Start()
    {
        agent.speed = Random.Range(2, 6);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        finish = false;
        anim.SetBool("idle", true);
        winner = false;

    }


    void Update()
    {
        if (PlayerControl.ctrl.tap)
        {
            speedTime += Time.deltaTime;

            if (speedTime >=5)
            {
                agent.speed = Random.Range(2, 6);

                speedTime = 0;

            }

            //transform.position = Vector3.MoveTowards(transform.position, runPoint[nextPoint].position, speed * Time.deltaTime);

            if (!finish)
            {
                agent.SetDestination(finishPoint.position);


            }
        }
        AnimControl();



    }


    void AnimControl()
    {
        if (PlayerControl.ctrl.tap && !finish)
        {
            anim.SetBool("idle", false);
            anim.SetBool("run", true);

        }

        if (finish)
        {
            if (winner)
            {
                anim.SetBool("idle", false);
                anim.SetBool("run", false);
                anim.SetBool("dance", true);

            }
            else
            {
                anim.SetBool("idle", true);
                anim.SetBool("run", false);
                anim.SetBool("dance", false);

            }


        }


    }

    void FinishPosition()
    {
        if (finish)
        {
            if (PlayerControl.ctrl.number == 1)
            {
                transform.position = PlayerControl.ctrl.num1.transform.position;

            }
            if (PlayerControl.ctrl.number == 2)
            {
                transform.position = PlayerControl.ctrl.num2.transform.position;

            }
            if (PlayerControl.ctrl.number == 3)
            {
                transform.position = PlayerControl.ctrl.num3.transform.position;

            }

        }

    }



    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Finish"))
        {
            agent.enabled = false;
            rb.isKinematic = true;

            finish = true;
            PlayerControl.ctrl.number++;
            if (PlayerControl.ctrl.number == 1)
            {
                winner = true;
            }
            FinishPosition();
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
    }

}
