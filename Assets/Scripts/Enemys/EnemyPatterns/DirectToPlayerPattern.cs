using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectToPlayerPattern : EnemyPattern
{
    public float shoot_interval=1f;
    private float shoot_index = 0f;
    public int damage = 1;
    public float bullet_speed = 1.0f;
    
    public DirectToPlayerPattern(float shoot_interval, int damage, float bullet_speed)
    {
        this.shoot_interval = shoot_interval;
        this.damage = damage;
        this.bullet_speed = bullet_speed;
        initializeBullets();
    }

    public override void update(Vector2 position)
    {
        shoot_index +=Time.deltaTime;
        if (shoot_index >= shoot_interval)
        {
            AttackToPlayer(position);
            shoot_index -= shoot_interval;
            
        }
    }
    private void AttackToPlayer(Vector2 position)
    {
        try
        {
            foreach (TBullet i in bullets)
            {
                GameObject player = PlayerController.Instance.gameObject;
                Vector3 lookVector = (Vector2)player.transform.position - position;
                lookVector.z = 0;
                i.Shoot(position, lookVector);
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e);
        }

    }

    public override void initializeBullets()
    {
        bullets.Add(new TNormalBullet(this.damage,this.bullet_speed));
    }

}
