using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrc : Orc {

    public float reloadTime = 5f;
    public float defenceRadius = 10f;
    private float last_shot = 0;

    public GameObject prefabCarrot;

    protected override void Attack()
    {
        this.GetComponent<Animator>().SetBool("walking", false);
        float value = (this.transform.position.x < RabbitBehaviour.lastRabbit.transform.position.x) ? 1 : -1;
        this.GetComponent<SpriteRenderer>().flipX = (value < 0) ? false : true;

        if (Time.time - last_shot > reloadTime)
        {
            this.launchCarrot(value);
            this.GetComponent<Animator>().SetTrigger("hit");
            last_shot = Time.time;
        }
    }

    void launchCarrot(float direction)
    {
        GameObject obj = Instantiate(this.prefabCarrot);
        obj.transform.position = this.transform.position + new Vector3(0, 1, 0);
        Carrot carrot = obj.GetComponent<Carrot>();
        carrot.launch(direction);
    }

    public override bool rabbitIsHere()
    {
        if (Mathf.Abs(RabbitBehaviour.lastRabbit.transform.position.x - this.transform.position.x) < this.defenceRadius)
            return true;
        return false;
    }
}
