using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : Orc
{
    private float runSpeed = 2f;

    protected override void Attack()
    {
        float value = (this.transform.position.x < RabbitBehaviour.lastRabbit.transform.position.x) ? 1 : -1;
        this.GetComponent<SpriteRenderer>().flipX = (value < 0) ? false : true;

        Vector2 vel = this.GetComponent<Rigidbody2D>().velocity;
        vel.x = value * runSpeed;
        this.GetComponent<Rigidbody2D>().velocity = vel;
    }
}