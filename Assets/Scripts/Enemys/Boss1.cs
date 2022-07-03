using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Enemy
{
    public float shoot_interval = 1f;
    public int damage = 1;
    public float bullet_speed = 1.0f;
    public float rotationSpeed = 90;
    public float distr_shoot_accuracy = 0.1f;

    public int ways = 1;
    public bool clockwise = false;

    protected override void initializePatterns()
    {
        patterns.Add(new RotatingPattern(shoot_interval, damage, bullet_speed, clockwise, ways, rotationSpeed));
        patterns.Add(new DistributedToPlayerPattern(shoot_interval, damage, bullet_speed, distr_shoot_accuracy));
    }
}
