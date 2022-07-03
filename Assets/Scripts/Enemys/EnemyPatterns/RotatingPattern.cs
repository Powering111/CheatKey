using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPattern : EnemyPattern
{
    public float shoot_interval=1f;
    public int damage = 1;
    public float bullet_speed = 1.0f;


    public int ways = 1;
    public bool clockwise = false;

    float rotation = 0;
    public float rotationSpeed = 90;

    float attack_index=0;
    public RotatingPattern(float shoot_interval, int damage, float bullet_speed, bool clockwise, int ways, float rotationSpeed)
    {
        this.shoot_interval = shoot_interval;
        this.damage = damage;
        this.bullet_speed = bullet_speed;
        this.clockwise = clockwise;
        this.ways = ways;
        this.rotationSpeed = rotationSpeed;
        initializeBullets();
    }

    public override void update(Vector2 position)
    {
        attack_index += Time.deltaTime;
        if (clockwise)
        {
            rotation -= Time.deltaTime * rotationSpeed;

        }
        else
        {
            rotation += Time.deltaTime * rotationSpeed;
        }

        if (attack_index >= shoot_interval)
        {
            attack_index -= shoot_interval;
            AttackAutoRotate(position);
        }
    }
    private void AttackAutoRotate(Vector2 position)
    {
        for (int way = 0; way < ways; way++)
        {
            Vector2 lookVector = util.DegreeToVector(rotation + (360.0f / ways) * way);
            bullets[0].Shoot(position, lookVector);

        }

    }

    public override void initializeBullets()
    {
        bullets.Add(new TNormalBullet(this.damage,this.bullet_speed));
    }

}
