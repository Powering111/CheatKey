using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TBullet
{
    public static GameObject prefab;

    public void Shoot(Vector2 position, Vector2 lookVector);
    
}
