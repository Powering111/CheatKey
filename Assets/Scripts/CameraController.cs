using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public static CameraController instance;
    private float lerpTimer=0;
    private float smooth=1f;
    private float targetFOV, lastFOV = 5;
    private bool changingFOV = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static CameraController Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Update()
    {
        if (changingFOV)
        {
            changeFOV();
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(player != null) { 
            Vector3 playerPos = player.transform.position;
            playerPos.z = -10;
            transform.position = playerPos;
        }
        else{

        }
    }

    public void zoomOut()
    {
        Debug.Log("zooming out");
        lastFOV = GetComponent<Camera>().orthographicSize;
        targetFOV = 7;
        changingFOV = true;
    }

    private void changeFOV()
    {
        lerpTimer = Time.deltaTime * smooth;
        /*if (lerpTimer >= 1.0f)
        {
            changingFOV = false;
            lerpTimer = 1.0f;
        }*/
        lastFOV=Mathf.Lerp(lastFOV, targetFOV, lerpTimer);
        if(Mathf.Abs(lastFOV - targetFOV) < 0.1f) {
            changingFOV = false;
        }
        GetComponent<Camera>().orthographicSize = lastFOV;
    }
}
