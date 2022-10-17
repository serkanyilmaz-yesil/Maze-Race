using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    public Transform finishPos, lookPos;
    public float lerpSpeed;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!PlayerControl.ctrl.finish && PlayerControl.ctrl.number < 3)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, player.position.z + offset.z);

        }

        if (!PlayerControl.ctrl.finish && PlayerControl.ctrl.number >=3)
        {
            transform.position = Vector3.MoveTowards(transform.position, finishPos.position, lerpSpeed * Time.deltaTime);
            transform.LookAt(lookPos);

        }

        if (PlayerControl.ctrl.finish)
        {
            transform.position = Vector3.MoveTowards(transform.position, finishPos.position, lerpSpeed * Time.deltaTime);
            transform.LookAt(lookPos);
        }
    }
}
