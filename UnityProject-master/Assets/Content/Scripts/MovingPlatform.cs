using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3 MoveBy;
    public float speed = 2;
    public float wait = 5;

    Vector3 pointA;
    Vector3 pointB;

    bool going_to_a;
    float time_to_wait;

    // Use this for initialization
    void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        this.going_to_a = false;
        this.time_to_wait = wait / 2;
    }

    void Update()
    {
        Vector3 my_pos = this.transform.position;
        Vector3 target;

        if (this.going_to_a)
        {
            target = this.pointA;
        }
        else
        {
            target = this.pointB;
        }

        Vector3 destination = target - my_pos;
        destination.z = 0;

        if ((this.time_to_wait -= Time.deltaTime) < 0)
        {
            if (isArrived(this.transform.position, target))
            {
                this.time_to_wait = wait;
                this.going_to_a = !this.going_to_a;
            }
            else
            {
                this.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }
    }

    private bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }
}
