using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChainController : MonoBehaviour
{
    [SerializeField] GameObject sphere;
    [SerializeField] GameObject player;
    [SerializeField] float maxLink = 10f;
    private PlayerAnimator anim;

    [SerializeField] private float triggerInputTimer;
    [SerializeField] private float timeBetweenTractAndGrabSphere;
    [SerializeField] private float recallTime;
    [SerializeField] private float DistPlayerSphereRecallEnd;

    private Vector3 distSpherePlayer;

    private bool isHoldingMoon = false;
    [HideInInspector] public bool isChained = false;


    private void Start()
    {
        StopAllCoroutines();
        triggerInputTimer = 0;
        
        anim = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if (isHoldingMoon)
            triggerInputTimer += Time.deltaTime;

    }

    public bool CreateChain()
    {
        if (isChained)
            return false;

        player.transform.LookAt(sphere.transform);
        player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
        distSpherePlayer = sphere.transform.position - player.transform.position;

        
        if (distSpherePlayer.magnitude > maxLink)
            return false;

        //return CreateHingeJoint();
        return CreateSpringJoint();
    }
    
    private bool CreateHingeJoint()
    {
        if (sphere.TryGetComponent<HingeJoint>(out HingeJoint joint))
            return false;

        HingeJoint sphereJoint = sphere.AddComponent<HingeJoint>();
        sphereJoint.connectedBody = player.GetComponentInChildren<Rigidbody>();
        return true;
    }

    private bool CreateSpringJoint()
    {
        if (player.TryGetComponent<SpringJoint>(out SpringJoint spring))
            return false;

        SpringJoint springJ = player.AddComponent<SpringJoint>();
        //if (distSpherePlayer.magnitude < 2.5f)
        //    springJ.maxDistance = 2.5f;
        //else
        //    springJ.maxDistance = distSpherePlayer.magnitude;

        springJ.maxDistance = 0.5f;
        springJ.damper = 1000;
        springJ.spring = 5000;
        springJ.enableCollision = true;
        springJ.axis = new Vector3(0, 1, 0);
        springJ.connectedBody = sphere.GetComponent<Rigidbody>();

        return true;
    }

    private IEnumerator DragMoonToPlayer()
    {
        float spherePlayerDist = (player.transform.position - sphere.transform.position).magnitude;
        float recallSpeed = spherePlayerDist / recallTime;
        Vector3 spherePlayerDirection = (player.transform.position -sphere.transform.position).normalized;
        Vector3 newVelocity;

        while (sphere.transform.position.y < player.transform.position.y + 1)
        {
            sphere.GetComponent<Rigidbody>().velocity = Vector3.up * recallSpeed;
            yield return null;
        }

        while (sphere.transform.position.x > player.transform.position.x + 2 || sphere.transform.position.x < player.transform.position.x - 2 ||
            sphere.transform.position.z > player.transform.position.z + 2 || sphere.transform.position.z < player.transform.position.z - 2)
        {
            spherePlayerDirection = (player.transform.position - sphere.transform.position).normalized;
            newVelocity = spherePlayerDirection * recallSpeed;
            sphere.GetComponent<Rigidbody>().velocity = newVelocity;
            yield return null;
        }

        sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
   
    public void DestroyLink()
    {
        isChained = false;
        sphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None ;

        //  Hinge
        //Destroy(sphere.GetComponent<HingeJoint>());

        //Spring
        Destroy(player.GetComponent<SpringJoint>());
    }

    public void LaunchLink(InputAction.CallbackContext context)
    {
        if (player.GetComponent<Player>().state == PlayerState.COMBO_MOON)
            return;

        isHoldingMoon = true;
        if (context.performed)
        {
            float value = context.ReadValue<float>();
            if (value >= 0.80f)
            {
                isChained = CreateChain();
            }
        }

        if(context.canceled)
        {
            if (triggerInputTimer <= timeBetweenTractAndGrabSphere)
            {
                sphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                StartCoroutine(DragMoonToPlayer());
                anim.StartTriggerRockCall();
            }

            triggerInputTimer = 0;
            isHoldingMoon = false;
        }
    }

}
