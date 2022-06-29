using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float speed = 1.0f;
    private int destroyID;
    private Rigidbody2D rb;
    public int damage = 1;
    private bool moving = true;
    public float lifeReach = 5.0f;
    void Start()
    {
        destroyID = Animator.StringToHash("Destroyed");
        moving = true;
        //rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position += transform.up * speed * Time.deltaTime;
            lifeReach -= speed * Time.deltaTime;
            if (lifeReach < 0)
            {
                destroySelf();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (moving)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Damage(damage);
                destroySelf();
            }
            if (collision.gameObject.CompareTag("wall"))
            {
                destroySelf();
            }
        }
    }

    private void destroySelf()
    {
        gameObject.GetComponent<Animator>().SetBool(destroyID, true);
        moving = false;

    }
}
