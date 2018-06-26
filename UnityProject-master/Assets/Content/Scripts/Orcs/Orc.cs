using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    public enum Mode { GoToA, GoToB, Attack, Dying }

    public Vector3 MoveBy;
    public float speed = 1f;

    public Vector3 pointA;
    public Vector3 pointB;

    public Mode mode = Mode.GoToB;

    public string debug = "";

    public void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = this.transform.position;
        if (MoveBy.x < 0)
            this.pointA += MoveBy;
        else
            this.pointB += MoveBy;

        GetComponent<Animator>().SetBool("walking", false);
        GetComponent<Animator>().SetBool("running", false);
    }

    public void Update()
    {
        if (mode == Mode.Dying)
            return;

        if (mode == Mode.GoToA || mode == Mode.GoToB)
        {
            GetComponent<Animator>().SetBool("walking", true);
            GetComponent<Animator>().SetBool("running", false);
        }
        GetComponent<Animator>().SetBool("walking", true);
        GetComponent<Animator>().SetBool("running", true);
    }

    public void FixedUpdate()
    {
        if (mode == Mode.Dying)
            return;

        if (rabbitIsHere())
            mode = Mode.Attack;
        else if (!rabbitIsHere() && mode == Mode.Attack)
            mode = Mode.GoToA;
        
        if (mode == Mode.Attack)
            Attack();
        else
        {
            // Patrooling

            if (mode == Mode.GoToA && transform.position.x < pointA.x)
                mode = Mode.GoToB;
            if (mode == Mode.GoToB && transform.position.x > pointB.x)
                mode = Mode.GoToA;

            float value = this.getDirection();

            Vector2 vel = this.GetComponent<Rigidbody2D>().velocity;
            vel.x = value * speed;
            this.GetComponent<Rigidbody2D>().velocity = vel;

            GetComponent<SpriteRenderer>().flipX = (value < 0) ? false : true;
        }
    }

    public virtual bool rabbitIsHere()
    {
        Vector3 rabbit_pos = RabbitBehaviour.lastRabbit.transform.position;

        if (rabbit_pos.x > Mathf.Min(pointA.x, pointB.x) && rabbit_pos.x < Mathf.Max(pointA.x, pointB.x))
            return true;

        return false;
    }

    float getDirection()
    {
        if (this.mode == Mode.GoToA)
            if (this.transform.position.x <= pointA.x)
                return 1;
            else
                return -1;
        if (this.mode == Mode.GoToB)
            if (this.transform.position.x <= pointB.x)
                return 1;
            else
                return -1;
        return 0;
    }

    protected virtual void Attack() { }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (mode == Mode.Dying)
            return;

        if (this.isActiveAndEnabled)
        {
            RabbitBehaviour rabbit = collision.gameObject.GetComponent<RabbitBehaviour>();
            if (rabbit != null)
            {
                if (rabbit.transform.position.y - this.transform.position.y >= this.GetComponent<BoxCollider2D>().bounds.size.x / 2)
                {
                    StartCoroutine(DeathCoroutine());
                }
                else
                {
                    rabbit.damage();
                    GetComponent<Animator>().SetTrigger("hit");
                }
            }
        }
    }

    private IEnumerator DeathCoroutine()
    {
        mode = Mode.Dying;
        GetComponent<Animator>().SetTrigger("dead");
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }
}