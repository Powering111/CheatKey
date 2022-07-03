using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    public int max_HP;
    public int HP;
    public float sight = 6.0f;
    public float speed = 0.5f;
    public bool move=true;

    private Animator animator;
    private Rigidbody2D rb;

    private int destroy_anim_id, attacking_anim_id;
    
    private Vector3 lookVector;

    private bool attacking = false, alive = true;
    float move_index=1; 

    protected List<EnemyPattern> patterns = new List<EnemyPattern>();

    void Awake()
    {
        HP = max_HP;
        animator = gameObject.GetComponent<Animator>();
        destroy_anim_id = Animator.StringToHash("Destroy");
        attacking_anim_id = Animator.StringToHash("Attacking");
        attacking = false;
        rb = GetComponent<Rigidbody2D>();
        alive = true;
        initializePatterns();
    }


    protected void FixedUpdate()
    {

        
        if(!attacking&&util.canSeePlayer(transform.position, this.sight))
        {
            startAttacking();
        }

        if(attacking && !util.canSeePlayer(transform.position, this.sight))
        {
            stopAttacking();
        }
        
        
        //TODO : il ban hwa
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

        //Attack
        if (attacking)
        {
            foreach(EnemyPattern i in patterns)
            {
                i.update(transform.position);
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



    protected abstract void initializePatterns();


    void startAttacking()
    {
        attacking = true;
        animator.SetBool(attacking_anim_id, true);
    }
    void stopAttacking()
    {
        attacking = false;
        animator.SetBool(attacking_anim_id, false);
    }

    
}