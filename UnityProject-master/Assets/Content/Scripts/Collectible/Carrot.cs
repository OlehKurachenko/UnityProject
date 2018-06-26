using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Bomb {

    private float speed = 4f;
    private float direction;
    private float lifetime = 5f;

    void Start()
    {
        StartCoroutine(destroyLater());
    }

    public void launch(float direct)
    {
        direction = direct;
        if (direct < 0)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    void FixedUpdate()
    {
        this.transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
    }

    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(lifetime);
        this.CollectedHide();
    }
}
