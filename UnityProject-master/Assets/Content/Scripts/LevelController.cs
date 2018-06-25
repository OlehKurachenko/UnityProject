using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController current;
    Vector3 startingPosition;

    void Awake()
    {
        current = this;
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void onRabbitDeath(RabbitBehaviour rabit)
    {
        rabit.transform.position = this.startingPosition;
    }

    public void addCoins(int amount)
    {
        // TODO write
    }

    public void addFruit()
    {
        // TODO write
    }

    public void addCrystal()
    {
        // TODO write
    }
}