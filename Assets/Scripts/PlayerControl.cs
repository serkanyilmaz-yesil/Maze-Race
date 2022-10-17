using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public static PlayerControl ctrl;




    [SerializeField]
    Joystick Joystick;
    [SerializeField]
    Transform PlayerSprite;

    public float speed = 2;
    public float startSpeed;
    public bool tap,winner;
    private Animator anim;
    public bool finish;

    public int number;
    public GameObject num1, num2, num3;
    private Rigidbody rb;
    public GameObject joyStick,arrow,finishEffect;
    public Transform effectPoint;

    private void Awake()
    {
        ctrl = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        tap = false;
        anim = GetComponent<Animator>();
        startSpeed = speed;
        speed = 0;
        rb = GetComponent<Rigidbody>();
        winner = false;
        arrow.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            tap = true; 
            arrow.SetActive(false);

        }


        if (tap && !finish && number < 3)
        {
            Hareket();

        }

        AnimControl();

        if (number >= 3 || finish)
        {
            joyStick.SetActive(false);
        }
    }


    void AnimControl()
    {
        if (speed == 0 && !finish)
        {
            anim.SetBool("idle", true);
            anim.SetBool("run", false);

        }
        if (!finish && speed != 0)
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

            arrow.SetActive(true);

        }

    }

    void FinishPosition()
    {
        if (finish)
        {
            if (number == 1)
            {
                transform.position = num1.transform.position;

            }
            if (number == 2)
            {
                transform.position = num2.transform.position;

            }
            if (number == 3)
            {
                transform.position = num3.transform.position;

            }

        }

    }


    void Hareket()
    {
        if (Mathf.Abs(Joystick.Direction.x) > 0f || Mathf.Abs(Joystick.Direction.y) > 0f)
        {
            speed = startSpeed;
            PlayerSprite.position = new Vector3(Joystick.Horizontal * 1f + transform.position.x, 0.1f, Joystick.Vertical * 1f + transform.position.z);

            transform.LookAt(new Vector3(PlayerSprite.position.x, 0, PlayerSprite.position.z));
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        }
        else
            speed = 0;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            rb.isKinematic = true;
            Instantiate(finishEffect, effectPoint.transform.position, Quaternion.identity);
            speed = 0;
            finish = true;
            number++;
            if (number == 1)
            {
                winner = true;
            }
            FinishPosition();
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
    }
}
