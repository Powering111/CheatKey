using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMemberBind : MonoBehaviour
{
    public GameObject TNormalBullet_prefab;
    
    void Start()
    {
        TNormalBullet.prefab = TNormalBullet_prefab;
    }

}
