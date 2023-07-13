using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowFPS : MonoBehaviour
{

    public TextMeshProUGUI tmpo;
    private float timer = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.2f)
        {
            tmpo.text = $"{Time.deltaTime}";
            timer = 0;
        }

    }
}
