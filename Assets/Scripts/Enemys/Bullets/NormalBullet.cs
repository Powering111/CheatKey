using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    public float speed = 1.0f;
    public NormalBullet(int damage, float speed)
    {
        this.speed = speed;
        this.damage = damage;
    }
    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position = transform.position + transform.up * speed * Time.deltaTime;
        }
    }
    
}
