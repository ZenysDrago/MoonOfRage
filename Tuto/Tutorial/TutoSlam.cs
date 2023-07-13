using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoSlam : MonoBehaviour
{
    [SerializeField] private TutoCombo combo;

    // Start is called before the first frame update
    void Start()
    {
        combo = FindObjectOfType<TutoCombo>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            combo.isInCollider = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            combo.isInCollider = false;
        }
    }
}
