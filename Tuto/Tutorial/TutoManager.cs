using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public bool jumpOn;
    public bool comboOn;
    public bool finisherOn;
    public bool moonOn;
    public bool movingOn;
    public bool recallOn;

    // Start is called before the first frame update
    void Start()
    {
        jumpOn = false;
        comboOn = false;
        moonOn = false;
        movingOn = true;
        recallOn = false;
        finisherOn = false;
    }

    public void ActivateJump()
    {
        jumpOn = true;
    }

    public void ActivateCombo()
    {
        comboOn = true;
    }

    public void ActivateMoon()
    {
        moonOn = true;
    }

    public void ActivateMove()
    {
        movingOn = true;
    }

    public void ActivateRecall()
    {
        recallOn = true;
    }

    public void ActivateFinisher()
    {
        finisherOn = true;
    }

    public void DesactivateMove()
    {
        movingOn = false;
    }
}
