using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamMoon : MonoBehaviour
{
    [SerializeField] private PlayerCombo combo;

    // Start is called before the first frame update
    void Start()
    {
        combo = FindObjectOfType<PlayerCombo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            combo.isInCollider = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            combo.isInCollider = false ;
        }
    }
}
