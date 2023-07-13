using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailActive : MonoBehaviour
{
    private Moon moon;
    private Rigidbody rbodyMoon;

    // Start is called before the first frame update
    void Start()
    {
        moon = FindObjectOfType<Moon>();
        rbodyMoon = moon.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rbodyMoon.velocity.magnitude < moon.speedToTrail)
            gameObject.SetActive(false);
    }
}
