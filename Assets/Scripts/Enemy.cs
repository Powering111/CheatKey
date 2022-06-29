using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int max_HP;
    public int HP;
    public int attackDamage = 3;
    public int bodyDamage=1;
    public float attack_interval = 1;
    public float attackRange = 6.0f;
    public float speed = 0.5f;
    public bool move=true;

    public GameObject bullet;
    private Animator animator;
    private Rigidbody2D rb;

    private int destroy_anim_id, attacking_anim_id;
    private int ray_layer_mask;
    private Vector3 lookVector;

    private bool attacking = false, alive = true;
    float move_index=1; 
    float attack_index=0;


    float rotation = 0, rotationSpeed = 90;

    void Awake()
    {
        HP = max_HP;
        animator = gameObject.GetComponent<Animator>();
        destroy_anim_id = Animator.StringToHash("Destroy");
        attacking_anim_id = Animator.StringToHash("Attacking");
        attacking = false;
        rb = GetComponent<Rigidbody2D>();
        alive = true;
        ray_layer_mask = LayerMask.GetMask("Player","Wall");
    }

    public void FixedUpdate()
    {

        rotation += Time.deltaTime * rotationSpeed;
        //Start attacking player
        if (!attacking)
        {
            try
            {
                GameObject player = PlayerController.Instance.gameObject;
                Vector3 lookVector = (player.transform.position - transform.position);
                lookVector.z = 0;
                // Check if enemy can see player
                if(lookVector.magnitude < attackRange)
                {
                    RaycastHit2D hit;
                    hit = Physics2D.Raycast(rb.position, lookVector, attackRange, ray_layer_mask) ;
                    
                    if (hit.transform.CompareTag("player"))
                    {
                        this.lookVector = lookVector;
                        startAttacking();
                    }
                }
            }
            catch(System.Exception)
            {
            }
        }

        // Move enemy
        if (move && attacking && alive)
        {
            try
            {
                
                move_index+=Time.deltaTime;
                
                if (move_index >= 1 && move_index<2)
                {
                    Debug.Log("Moving");
                    //Move 
                    Vector2 newPos = rb.position + speed * Time.deltaTime * (Vector2)lookVector.normalized;
                    rb.MovePosition(newPos);
                    
                }
                else if (move_index >= 2)
                {
                    //Stop

                    //continue
                    if (move_index >= 3)
                    {
                        GameObject player = PlayerController.Instance.gameObject;
                        lookVector = (player.transform.position - transform.position);
                        lookVector.z = 0;
                        move_index = 1;
                    }
                }
            }
            catch (System.Exception)
            {

            }
        }
    }

    public void Damage(int damage)
    {
        HP -= damage;
        animator.Play("Enemy_damaged");
        if (HP <= 0)
        {
            HP = 0;
            // die
            GetComponent<CircleCollider2D>().enabled = false;
            animator.SetBool(destroy_anim_id, true);
            stopAttacking();
            alive = false;
            this.enabled = false;

        }
    }

    private void AttackToPlayer()
    {
        try
        {
            GameObject player = PlayerController.Instance.gameObject;
            Vector3 lookVector = (player.transform.position - transform.position);
            lookVector.z = 0;
            Shoot(lookVector);
            
        }
        catch(System.Exception)
        {
            
        }

    }

    
    private void AttackAutoRotate()
    {
        Vector2 lookVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad*rotation),Mathf.Sin(Mathf.Deg2Rad*rotation));
        Shoot(lookVector);
    }

    void Shoot(Vector3 lookVector)
    {
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, lookVector.normalized);
            GameObject bu = Instantiate(bullet, (Vector3)transform.position, lookRotation);
            bu.GetComponent<Bullet>().damage = attackDamage;
    }

    private void Update()
    {
        if (attacking)
        {
            attack_index += Time.deltaTime;
            if (attack_index >= attack_interval)
            {
                attack_index -= attack_interval;
                InvokeAttack();
            }
        }
    }

    private void InvokeAttack()  // checks player is in sight then attack
    {
        try
        {
            GameObject player = PlayerController.Instance.gameObject;
            Vector3 lookVector = (player.transform.position - transform.position);
            lookVector.z = 0;
            if (lookVector.magnitude < attackRange )
            {
                RaycastHit2D hit;
                hit = Physics2D.Raycast(rb.position, lookVector, attackRange, ray_layer_mask);
                
                if (hit.transform.CompareTag("player"))
                {
                    //AttackToPlayer();
                    AttackAutoRotate();
                }
                else
                {
                    // player is not detedted
                    stopAttacking();
                }
            }
            else
            {
                // player is far
                stopAttacking();
            }
        }
        catch (System.Exception)
        {
            //when player is died
            stopAttacking();
        }
    }

    void startAttacking()
    {
        attacking = true;
        animator.SetBool(attacking_anim_id, true);
    }
    void stopAttacking()
    {
        attacking = false;
        attack_index = 0;
        animator.SetBool(attacking_anim_id, false);
    }
}
