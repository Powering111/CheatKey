using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int max_HP;
    public int HP;
    public int strength = 3;
    public float attack_interval = 1;
    public float attackRange = 6.0f;
    public float speed = 0.5f;
    public GameObject bullet;
    public bool move=true;
    private Animator animator;
    private int destroy_anim_id, attacking_anim_id;
    private Coroutine attackCoroutine;
    private bool attacking = false, alive = true;
    private Rigidbody2D rb;
    private int ray_layer_mask;

    private Vector3 lookVector;
    float move_index=1; 
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
                        Debug.Log("coroutine start");
                        attackCoroutine = StartCoroutine(RepeatAttack(attack_interval));
                        attacking = true;
                    }
                }
            }
            catch(System.Exception e)
            {
            }
        }

        // Move enemy
        if (move && attacking && alive)
        {
            try
            {
                rb.MovePosition(rb.position);
                move_index+=Time.deltaTime;
                
                if (move_index >= 1 && move_index<2)
                {
                    //Move 
                    rb.MovePosition(rb.position + (Vector2)lookVector.normalized * speed * Time.deltaTime);

                    if (move_index >= 2) move_index = 2;
                    
                }
                else if (move_index >= 2)
                {
                    //Stop

                    //continue
                    if (move_index >= 3)
                    {
                        GameObject player = PlayerController.Instance.gameObject;
                        Vector3 lookVector = (player.transform.position - transform.position);
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
        }
    }

    public void Attack()
    {
        animator.SetBool(attacking_anim_id, false);
        try
        {
            GameObject player = PlayerController.Instance.gameObject;
            Vector3 lookVector = (player.transform.position - transform.position);
            lookVector.z = 0;
            
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, lookVector.normalized);
            GameObject bu = Instantiate(bullet, (Vector3)transform.position, lookRotation);
            bu.GetComponent<Bullet>().damage = strength;
            
        }
        catch(System.Exception)
        {
            
        }

    }
    private void StartAttack()
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
                    //Debug.Log(hit.transform.gameObject.CompareTag("player"));
                    animator.SetBool(attacking_anim_id, true);
                }
                else
                {
                    stopAttacking();
                }
            }
            else
            {
                stopAttacking();
            }
        }
        catch (System.Exception)
        {
            stopAttacking();
        }
    }
    IEnumerator RepeatAttack(float time)
    {
        while (true) {
            yield return new WaitForSeconds(time);
            StartAttack();
        }
    }

    void stopAttacking()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            attacking = false;
            Debug.Log("coroutine stop");
        }
    }
}
