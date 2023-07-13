using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public AudioSource soundBreak;
    public bool isBreakableByPlayer = false;
    public float timeForDeleteBox = 1.5f;
    [SerializeField] private float VelocityNeededToBreak;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isBreakableByPlayer)
        {
            if (collision.gameObject.layer == 12)
            {
                DestroyBox();
            }
        }

        if (collision.gameObject.layer == 9)
        {
            if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= VelocityNeededToBreak)
            {
                DestroyBox();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBreakableByPlayer)
        {
            if (other.gameObject.layer == 12)
            {
                DestroyBox();
            }
        }
    }

    private void DestroyBox()
    {
        GameObject box = gameObject.transform.GetChild(0).gameObject;
        GameObject boxBroken = gameObject.transform.GetChild(1).gameObject;

        Destroy(gameObject.GetComponent<BoxCollider>());
        Destroy(box);
        boxBroken.SetActive(true);
        AutoDestruct boxDestroy = boxBroken.AddComponent<AutoDestruct>();
        boxDestroy.timeBeforeDestruct = timeForDeleteBox;
    }
}
