using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBehaviour : MonoBehaviour
{
    public static RabbitBehaviour lastRabbit = null;

    public float speed = 4;
    Rigidbody2D myBody = null;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 1f;
    public float JumpSpeed = 4f;

    float deadAnimationTime = 1f;
    float deadTime = 0f;

    public bool isDead = false;
    public bool isBig = false;

    public float bigScale = 0.4f;

    Transform heroParent = null;

    void Start()
    {
        this.myBody = this.GetComponent<Rigidbody2D>();
        LevelController.current.setStartPosition(transform.position);
        this.heroParent = this.transform.parent;
        this.isDead = false;
        this.isBig = false;

        GetComponent<Animator>().SetBool("dead", false);
    }

    void Awake()
    {
        lastRabbit = this;
    }

    void Update()
    {
        if (isDead)
            return;

        //[-1, 1]
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }

        Animator animator = GetComponent<Animator>();
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }

        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
    }

    void FixedUpdate()
    {
        if (this.isDead == true)
        {
            if ((this.deadTime -= Time.deltaTime) <= 0)
            {
                this.isDead = false;
                GetComponent<Animator>().SetBool("dead", false);
                LevelController.current.onRabbitDeath(this);
            }
            return;
        }

        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            //Перевіряємо чи ми опинились на платформі
            if (hit.transform != null)
            {
                //Приліпаємо до платформи
                SetNewParent(this.transform, hit.transform);
            }
            isGrounded = true;
        }
        else
        {
            SetNewParent(this.transform, this.heroParent);
            isGrounded = false;
        }
        //Намалювати лінію (для розробника)
        Debug.DrawLine(from, to, Color.red);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
    }

    public void damage()
    {
        if (isDead)
            return;

        if (this.isBig)
            changeSize(false);
        else
        {
            this.isDead = true;
            this.deadTime = this.deadAnimationTime;
            GetComponent<Animator>().SetBool("dead", true);
        }
    }

    public void changeSize(bool toBig)
    {
        if (toBig && !this.isBig)
        {
            this.transform.localScale += new Vector3(bigScale, bigScale, 0);
            this.isBig = true;
        }
        if (!toBig && this.isBig)
        {
            this.transform.localScale -= new Vector3(bigScale, bigScale, 0);
            this.isBig = false;
        }
    }

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
            //Після зміни батька координати кролика зміняться
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }
}