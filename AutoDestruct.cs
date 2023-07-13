using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{
    private float timer = 0f;

    public float timeBeforeDestruct;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBeforeDestruct)
            Destroy(gameObject);
    }
}
