using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNormalBullet : TBullet
{
    public static GameObject prefab;
    public int damage = 1;
    public float speed = 1.0f;
    public void Shoot(Vector2 position, Vector2 lookVector)
    {
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, lookVector.normalized);
        GameObject instance = GameObject.Instantiate(prefab, (Vector3)position, lookRotation);
        instance.GetComponent<NormalBullet>().speed = speed;
        instance.GetComponent<NormalBullet>().damage = damage;

    }

    public TNormalBullet(int damage, float speed)
    {
        this.speed = speed;
        this.damage = damage;
    }
    
    
}
