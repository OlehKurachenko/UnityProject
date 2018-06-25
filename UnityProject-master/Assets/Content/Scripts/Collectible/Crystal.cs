using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable
{

    protected override void OnRabitHit(RabbitBehaviour rabit)
    {
        LevelController.current.addCrystal();
        this.CollectedHide();
    }
}