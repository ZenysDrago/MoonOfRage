using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    private PlayerController playerControl;
    public Rigidbody sphereBody;
    [SerializeField] private GameObject trailMoon;
    [SerializeField] private float velMinForSound = 0;
    [SerializeField] private float velMaxSound = 25;


    [HideInInspector] public  MoonSound sound;
    [HideInInspector] public bool isOnGround = false;
    [HideInInspector] public bool asHitSlam = false;
    public int speedToTrail = 25;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<MoonSound>();
        playerControl = FindObjectOfType<PlayerController>();
        sphereBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        SlowMoon();

        //Debug.Log(sphereBody.velocity.magnitude);

        if (sphereBody.velocity.magnitude > speedToTrail)
            trailMoon.SetActive(true);
    }



    private void SlowMoon()
    {
        if(isOnGround)
        {
            if (playerControl.player.state != PlayerState.CONTROLLING_MOON)
                sphereBody.velocity /= 1.005f;
            else if (playerControl.direction == Vector2.zero)
                sphereBody.velocity /= 1.005f;
        }
    }


    private float RemapSound(float val, float inputStart, float inputEnd, float outputStart, float outputEnd)
    {
        return outputStart + (val - inputStart) * (outputEnd - outputStart) / (inputEnd - inputStart);
    }

    private void OnCollisionEnter(Collision collision)
    {

        //Remap the sound volume between 0 and 1 by the magnitude between velMin and VelMax 
        //if magnitude > 1 or < 0 we clamp between 0 and 1
        sound.LaunchImpact(Mathf.Clamp01(RemapSound(sphereBody.velocity.magnitude , velMinForSound , velMaxSound , 0 , 1)));

        if (collision.collider.gameObject.layer == 14)
            asHitSlam = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isOnGround = true;
            if (sphereBody.velocity != Vector3.zero)
                sound.LaunchRoll();
            else
                sound.StopRoll();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isOnGround = false;
            sound.StopRoll();
        }
    }
}
