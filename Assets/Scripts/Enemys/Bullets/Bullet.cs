using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    static public GameObject prefab;

    protected bool moving = true;
    protected Rigidbody2D rb;

    public int damage = 1;

    void Start()
    {
        moving = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
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
        gameObject.GetComponent<Animator>().SetBool(Animator.StringToHash("Destroying"), true);
        gameObject.GetComponent<Collider2D>().enabled = false;
        moving = false;
    }
}
