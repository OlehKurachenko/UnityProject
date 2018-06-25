using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    //Стандартна функція, яка викличеться,
    //коли поточний об’єкт зіштовхнеться із іншим
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Намагаємося отримати компонент кролика
        RabbitBehaviour rabit = collider.GetComponent<RabbitBehaviour>();
        //Впасти міг не тільки кролик
        if (rabit != null)
        {
            //Повідомляємо рівень, про смерть кролика
            LevelController.current.onRabbitDeath(rabit);
        }
    }
}
