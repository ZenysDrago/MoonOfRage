using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum Inputs
{
    JUMP,
    QUICK,
    HEAVY,
    LINK,
    HOLDHEAVY,
}

public enum Attack
{
    QUICK,
    HEAVY,
    MEDIUM,
    FINISHER,
}

public class BufferCombo : MonoBehaviour
{
    private PlayerController playerControll;
    private PlayerFightController playerFighter;

    private const int maxInputInBuffer = 5;
    private List<Attack> previousAttack;
    private float timer = 0f;
    private bool doneAttack = true;

    [HideInInspector] public PlayerCombo ComboMove;
    [HideInInspector] public  bool finisher = false;
    public float maxTimeForInput = 0.5f;
    public List<Inputs> buffer;
    public bool playerInCombo = false;
    public bool hasLitlleHitInMedium = false;


    // Start is called before the first frame update
    void Start()
    {
        ComboMove = GetComponent<PlayerCombo>();
        playerControll = GetComponent<PlayerController>();
        playerFighter = GetComponent<PlayerFightController>();
        buffer = new List<Inputs>();
        previousAttack = new List<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(!playerInCombo)
            CheckCurrentCombo();
        if (playerControll.player.state != PlayerState.COMBO_MOON)
            CheckFutureCombo();

        HandleInputs();   
    }

    public void ClearBuffer()
    {
        timer = 0;
        hasLitlleHitInMedium = false;
        playerInCombo = false;
        finisher = false;
        if (playerControll.player.state == PlayerState.COMBO_MOON)
            playerControll.player.state = PlayerState.CLASSIC;
        previousAttack.Clear();
        buffer.Clear();
    }

    private void HandleInputs()
    {
        if (buffer.Count != 0)
        {
            timer = 0;
        }
        else
        {
            if(timer > maxTimeForInput)
                ClearBuffer();
            return;
        }

        if(!ComboMove.isInCoroutine)
        {
            if (!playerInCombo)
            {
                CheckInputClassic();
            }
            else
            {
                if (!finisher)
                    CheckInputMedium();
                else
                    CheckInputFinisher();
            }
            if(doneAttack)
                CheckComboEnd();
        }
        
    }


    // Attacks
    private void CheckInputClassic()
    {
        switch(buffer[0])
        {
            case Inputs.QUICK:
                if(!playerFighter.isQAttack)
                {
                    playerFighter.QuickAttack(Attack.QUICK);
                    previousAttack.Add(Attack.QUICK);
                    doneAttack = true;
                }
                else
                {
                    doneAttack = false;
                }
                break;
            case Inputs.HEAVY:
                playerFighter.HeavyAttack(false , Attack.QUICK);
                previousAttack.Add(Attack.HEAVY);
                break;
            case Inputs.HOLDHEAVY:
                playerFighter.HeavyAttack(true , Attack.QUICK);
                previousAttack.Add(Attack.HEAVY);
                break;
            default:
                break;
        }

    }

    private void CheckInputMedium()
    {
        switch (buffer[0])
        {
            case Inputs.QUICK:
                if (!playerFighter.isQAttack)
                {
                    playerFighter.QuickAttack(Attack.MEDIUM);
                    doneAttack = true;
                    hasLitlleHitInMedium = true;
                }
                else
                {
                    doneAttack = false;
                }
                break;
            case Inputs.HEAVY:
                StartCoroutine(ComboMove.SpinningCombo(ComboMove.recallTime , ComboMove.spinSpeed , ComboMove.spinTime));
                break;
            case Inputs.LINK:
                StartCoroutine(ComboMove.SlamCombo(ComboMove.recallTime));
                break;
            case Inputs.HOLDHEAVY:
                playerFighter.HeavyAttack(true , Attack.MEDIUM);
                break;
            default:
                break;
        }
        previousAttack.Add(Attack.MEDIUM);

    }

    private void CheckInputFinisher()
    {
        switch (buffer[0])
        {
            case Inputs.QUICK:
                if (!playerFighter.isQAttack)
                {
                    playerFighter.QuickAttack(Attack.FINISHER);
                    doneAttack = true;
                }
                else
                {
                    doneAttack = false;
                }
                break;
            case Inputs.HEAVY:
                StartCoroutine(ComboMove.SpinningCombo(ComboMove.recallTime, ComboMove.spinSpeed * 2 , ComboMove.spinTime * 1.5f));
                break;
            case Inputs.LINK:
                StartCoroutine(ComboMove.SlamCombo(ComboMove.recallTime / 2));
                break;
            case Inputs.HOLDHEAVY:
                playerFighter.HeavyAttack(true, Attack.FINISHER , true);
                break;
            default:
                break;
        }
        previousAttack.Add(Attack.FINISHER);

    }


    //Verification for buffer and smoothness
    private void CheckComboEnd()
    {

        buffer.RemoveAt(0);
        if(playerInCombo && finisher)
        { 
            ClearBuffer();
        }
        int mediumHit = 0;
        foreach (Attack att in previousAttack)
        {
            if (att == Attack.MEDIUM)
                mediumHit++;
            if (mediumHit == 2)
            {
                finisher = true;
                break;
            }
        }
    }

    private void CheckCurrentCombo()
    {
        if(previousAttack.Count > 1)
        {
            if (previousAttack[0] == Attack.QUICK && previousAttack[1] == Attack.QUICK)
            {
                playerControll.player.state = PlayerState.COMBO_MOON;
                playerInCombo = true;
            }
            else
            {
                previousAttack[0] = previousAttack[1];
                previousAttack.RemoveAt(1);
            }
        }
        if (playerControll.player.state != PlayerState.COMBO_MOON)
            playerInCombo = false;
    }

    private void CheckFutureCombo()
    {
        bool lastAttackQuick = false;
        if (buffer.Count > 1)
        {
            foreach (Inputs inp in buffer)
            {
                if (inp == Inputs.QUICK)
                {
                    if (lastAttackQuick)
                        playerControll.player.state = PlayerState.COMBO_MOON;
                    else
                        lastAttackQuick = true;
                }
                else
                    lastAttackQuick = false;
            }
        }
    }
}
