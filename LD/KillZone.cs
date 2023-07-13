using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Death();
        }

        if (other.gameObject.layer == 7)
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Death();
        }
    }
}
