using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private Transform sphere;
    [SerializeField] private Player player;
    [SerializeField] private LineRenderer line;
    private Vector3[] position = new Vector3[2];
    // Start is called before the first frame update
    void Start()
    {
        position[0] = Vector3.zero;
        position[1] = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(sphere);
        if (player.state == PlayerState.CONTROLLING_MOON)
        {
            position[1].z = (transform.position - sphere.position).magnitude;
        }
        else
        {
            position[1].z = 0;
        }
        line.SetPositions(position);
    }
}
