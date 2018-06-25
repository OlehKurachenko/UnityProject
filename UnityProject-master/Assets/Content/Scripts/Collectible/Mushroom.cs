using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable
{

    protected override void OnRabitHit(RabbitBehaviour rabit)
    {
        rabit.changeSize(true);
        this.CollectedHide();
    }
}