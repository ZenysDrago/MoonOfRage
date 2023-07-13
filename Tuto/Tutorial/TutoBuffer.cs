using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TutoBuffer : MonoBehaviour
{
    private TutoControls playerControll;
    private TutoFight playerFighter;

    private const int maxInputInBuffer = 5;
    private List<Attack> previousAttack;
    private float timer = 0f;
    private bool finisher = false;
    private bool doneAttack = true;
    private bool endCombo = false;

    [HideInInspector] public TutoCombo ComboMove;
    [HideInInspector] public bool currentActionFinished = false;
    public float maxTimeForInput = 0.5f;
    public List<Inputs> buffer;
    public bool playerInCombo = false;

    private TutoManager manager;
    public bool jabDone = false;
    public bool spinDone = false;
    public bool slamDone = false;
    public bool uppercutDone = false;
    public bool yoyoDone = false;
    public bool superSpinDone = false;
    public bool suppercutDone = false;

    private int yoyoComboState = 0;
    private int spinComboState = 0;
    private int supperComboState = 0;


    // Start is called before the first frame update
    void Start()
    {
        ComboMove = GetComponent<TutoCombo>();
        playerControll = GetComponent<TutoControls>();
        playerFighter = GetComponent<TutoFight>();
        buffer = new List<Inputs>();
        previousAttack = new List<Attack>();
        manager = GetComponent<TutoManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!playerInCombo)
            CheckCurrentCombo();
        if (playerControll.player.state != PlayerState.COMBO_MOON)
            CheckFutureCombo();

        HandleInputs();
    }

    private void ClearBuffer()
    {
        timer = 0;
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
            if (timer > maxTimeForInput)
                ClearBuffer();
            return;
        }

        if (!ComboMove.isInCoroutine)
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
            if (doneAttack)
                CheckComboEnd();
        }

    }


    // Attacks
    private void CheckInputClassic()
    {
        switch (buffer[0])
        {
            case Inputs.QUICK:
                if (!playerFighter.isQAttack)
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
                playerFighter.HeavyAttack(false, Attack.QUICK);
                previousAttack.Add(Attack.HEAVY);
                break;
            case Inputs.HOLDHEAVY:
                playerFighter.HeavyAttack(true, Attack.QUICK);
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
                    if (yoyoComboState == 1)
                        yoyoComboState = 2;
                    jabDone = true;

                    doneAttack = true;
                    playerFighter.QuickAttack(Attack.MEDIUM);
                }
                else
                {
                    doneAttack = false;
                }
                break;
            case Inputs.HEAVY:
                if (spinComboState == 0)
                    spinComboState = 1;
                if (spinComboState == 1)
                    spinComboState = 2;
                spinDone = true;

                StartCoroutine(ComboMove.SpinningCombo(ComboMove.recallTime, ComboMove.spinSpeed, ComboMove.spinTime));
                break;
            case Inputs.LINK:
                slamDone = true;
                if (yoyoComboState == 0)
                    yoyoComboState = 1;
                if (supperComboState == 0)
                    supperComboState = 1;

                StartCoroutine(ComboMove.SlamCombo(ComboMove.recallTime));
                break;
            case Inputs.HOLDHEAVY:
                if (supperComboState == 1)
                    supperComboState = 2;
                uppercutDone = true;

                playerFighter.HeavyAttack(true, Attack.MEDIUM);
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
                if (spinComboState == 2)
                    superSpinDone = true;
                else
                    spinComboState = 0;

                StartCoroutine(ComboMove.SpinningCombo(ComboMove.recallTime, ComboMove.spinSpeed * 2, ComboMove.spinTime * 1.5f));
                break;
            case Inputs.LINK:
                if (yoyoComboState == 2)
                    yoyoDone = true;
                else
                    yoyoComboState = 0;

                StartCoroutine(ComboMove.SlamCombo(ComboMove.recallTime / 2));
                break;
            case Inputs.HOLDHEAVY:
                if (supperComboState == 2)
                    suppercutDone = true;
                else
                    supperComboState = 0;

                playerFighter.HeavyAttack(true, Attack.FINISHER, true);
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
        if (playerInCombo && finisher)
        {
            ClearBuffer();
        }
        int mediumHit = 0;

        foreach (Attack att in previousAttack)
        {
            if (att == Attack.MEDIUM && manager.comboOn)
            {
                if (mediumHit < 1 || manager.finisherOn)
                    mediumHit++;
                else
                    endCombo = true;
            }
            if (mediumHit == 2)
            {
                finisher = true;
                break;
            }
        }

        if (endCombo)
        {
            ClearBuffer();
            endCombo = false;
        }
    }

    private void CheckCurrentCombo()
    {
        if (previousAttack.Count > 1)
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
