using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoMoon : MonoBehaviour
{
    private TutoControls playerControl;
    [HideInInspector] public Rigidbody sphereBody;
    [SerializeField] private float velMinForSound = 5;
    [SerializeField] private float velMaxSound = 40;

    [HideInInspector] public MoonSound sound;
    [HideInInspector] public bool isOnGround = false;
    [HideInInspector] public bool asHitSlam = false;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<MoonSound>();
        playerControl = FindObjectOfType<TutoControls>();
        sphereBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        SlowMoon();

    }



    private void SlowMoon()
    {
        if (isOnGround)
        {
            if (playerControl.player.state != PlayerState.CONTROLLING_MOON)
                sphereBody.velocity /= 1.005f;
            else if (playerControl.direction == Vector2.zero)
                sphereBody.velocity /= 1.005f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        sound.LaunchImpact(Mathf.Lerp(velMinForSound, velMaxSound, sphereBody.velocity.magnitude));

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
