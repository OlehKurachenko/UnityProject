using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    bool hideAnimation = false;

    protected virtual void OnRabitHit(RabbitBehaviour rabit)
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (!this.hideAnimation)
        {
            RabbitBehaviour rabit = collider.GetComponent<RabbitBehaviour>();
            if (rabit != null && !rabit.isDead)
            {
                this.OnRabitHit(rabit);
            }
        }
    }

    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }
}