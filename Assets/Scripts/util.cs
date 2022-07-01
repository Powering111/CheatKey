using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class util
{
    public static Vector2 DegreeToVector(float rotation)
    {
        return new Vector2(Mathf.Cos(Mathf.Deg2Rad * rotation), Mathf.Sin(Mathf.Deg2Rad * rotation));
    }



    static int ray_layer_mask = LayerMask.GetMask("Player", "Wall");
    public static bool canSeePlayer(Vector2 position, float range)
    {
        try
        {
            GameObject player = PlayerController.Instance.gameObject;
            Vector2 lookVector = (Vector2)player.transform.position - position;

            RaycastHit2D hit;
            hit = Physics2D.Raycast(position, lookVector, range, ray_layer_mask);

            if (hit.transform.CompareTag("player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    
}
