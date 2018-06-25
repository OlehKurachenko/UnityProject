using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable
{

    protected override void OnRabitHit(RabbitBehaviour rabit)
    {
        LevelController.current.addFruit();
        this.CollectedHide();
    }
}