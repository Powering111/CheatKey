using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1.0f;
    private int destroyID;
    private Rigidbody2D rb;
    public int damage = 1;
    private bool moving = true;
    void Start()
    {
        moving = true;
        destroyID = Animator.StringToHash("Destroying");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position = transform.position + transform.up * speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(damage);
            destroySelf();
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            destroySelf();
        }
    }
    private void destroySelf()
    {
        gameObject.GetComponent<Animator>().SetBool(destroyID, true);
        moving = false;
    }
}
