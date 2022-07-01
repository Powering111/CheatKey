using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectEnemy : Enemy
{
    public float attack_interval=1.0f;

    public int attackDamage = 3;
    public float bullet_speed = 1.0f;

    protected override void initializePatterns()
    {

        patterns.Add(new DirectToPlayerPattern(attack_interval,attackDamage,bullet_speed));
    }
    
}
