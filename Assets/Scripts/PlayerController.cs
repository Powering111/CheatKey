using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    public GameObject arrowObject;
    public Animator animator;
    private int attacking_id;
    public float speed = 3.0f;
    public int max_HP=10;
    public float attack_interval = 0.5f;
    private float last_attack_time=0;
    public float reach = 5.0f, bullet_speed =1.0f;
    public int HP;
    public int bullet_damage=1;
    bool canmove = true;
    private Rigidbody2D rb;
    public float shootAccuracy=0.3f;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        attacking_id = Animator.StringToHash("ATTACKING");
        rb = gameObject.GetComponent<Rigidbody2D>();
        HP = max_HP;

    }
    public static PlayerController Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= last_attack_time + attack_interval)
            {
                // ShootOnMousePosition();
                ShootWithAccuracy();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool(attacking_id, true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool(attacking_id, false);
        }

    }
    private void FixedUpdate()
    {
        if (canmove)
        {
            Vector2 Move = new Vector2(0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                Move.y++;
            }
            if (Input.GetKey(KeyCode.S))
            {
                Move.y--;
            }
            if (Input.GetKey(KeyCode.D))
            {
                Move.x++;
            }
            if (Input.GetKey(KeyCode.A))
            {
                Move.x--;
            }
            rb.MovePosition(rb.position + Move.normalized * speed * Time.deltaTime);
        }
    }
    public void Damage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
        else
        {
            animator.Play("player_damaged");
        }
        UserInterface.Instance.updateUI();
    }
    private void ShootOnMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookVector = (mousePosition - transform.position);
        lookVector.z = 0;
        Shoot(lookVector);
    }
    private void ShootWithAccuracy()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookVector = (mousePosition - transform.position);
        lookVector.z = 0;
        lookVector = lookVector.normalized;
        lookVector += new Vector3(
            Random.Range(-1.0f, 1.0f)*shootAccuracy,
            Random.Range(-1.0f, 1.0f) * shootAccuracy,
            0);


        Shoot(lookVector);
    }
    private void Shoot(Vector3 lookVector)
    {

        Vector3 arrowPos = transform.position;
        arrowPos.z = -1;

        
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, lookVector.normalized);
        GameObject arrow = Instantiate(arrowObject, (Vector3)transform.position, lookRotation);
        arrow.GetComponent<arrow>().lifeReach = reach;
        arrow.GetComponent<arrow>().speed = bullet_speed;
        arrow.GetComponent<arrow>().damage = bullet_damage;
        last_attack_time = Time.time;

    }

    private void Die()
    {
        Debug.Log("You Died!");
        canmove = false;
        animator.SetBool(Animator.StringToHash("Die"), true);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        HP = 0;
        this.enabled = false;
        UserInterface.Instance.GetComponent<UserInterface>().gameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            this.Damage(collision.GetComponent<Enemy>().bodyDamage);
        }
    }
}
