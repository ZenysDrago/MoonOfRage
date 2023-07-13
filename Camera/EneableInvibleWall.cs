using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneableInvibleWall : MonoBehaviour
{
    [SerializeField] GameObject Wall;
    private bool Player;
    private bool Moon;
    private MeshCollider MeshCollider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Player = true;
            if (Wall.GetComponent<MeshCollider>().isTrigger && Player && Moon)
            {
                MeshCollider = Wall.GetComponent<MeshCollider>();
                MeshCollider.isTrigger = false;
                MeshCollider.convex = false;
            }

        }

        if (other.gameObject.tag == "Moon")
        {
            Moon = true;
            if (Wall.GetComponent<MeshCollider>().isTrigger && Player && Moon)
            {
                MeshCollider = Wall.GetComponent<MeshCollider>();
                MeshCollider.isTrigger = false;
                MeshCollider.convex = false;
            }
        }
    }
}
