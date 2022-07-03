using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyPattern
{
    public List<TBullet> bullets=new List<TBullet>();
    public abstract void update(Vector2 position);
    public abstract void initializeBullets();
}
