using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*public class RotatingEnemy : Enemy
{*/
    /*public int ways = 1;
    public bool clockwise = false;
    float rotation = 0;
    public float rotationSpeed = 90;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (clockwise)
        {
            rotation -= Time.deltaTime * rotationSpeed;

        }
        else
        {
            rotation += Time.deltaTime * rotationSpeed;
        }
    }

    public bool AttackDirect = false;
    public int AttackDirectInterval = 20;
    int DirectIndex = 0;
    protected override void Attack(Vector2 position, Vector2 lookVector)
    {
        DirectIndex++;
        if (AttackDirect && DirectIndex>=AttackDirectInterval)
        {
            Vector3 lookVector2 = new Vector2(
            Random.Range(-1.0f, 1.0f) * 0.1f,
            Random.Range(-1.0f, 1.0f) * 0.1f);
            Enemy.Shoot(position, lookVector2);
            DirectIndex = 0;
        }
        AttackAutoRotate(position);
    }


    private void AttackAutoRotate(Vector2 position)
    {
        Debug.Log("shooting!");
        for(int way = 0; way < ways; way++)
        {
            Vector2 lookVector = util.DegreeToVector(rotation+(360.0f/ways)*way);
            Shoot(position, lookVector);

        }

    }

    protected override void initializePatterns()
    {
        //patterns.Add(new RotatingPattern());
    }*/
//}
