using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotatingEnemy : Enemy
{
    public float shoot_interval = 1f;
    public int damage = 1;
    public float bullet_speed = 1.0f;
    public float rotationSpeed = 90;

    public int ways = 1;
    public bool clockwise = false;

    protected override void initializePatterns()
    {
        patterns.Add(new RotatingPattern(shoot_interval, damage, bullet_speed, clockwise, ways, rotationSpeed));
    }

    
}
