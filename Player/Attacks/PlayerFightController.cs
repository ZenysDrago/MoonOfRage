using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFightController : MonoBehaviour
{
    [SerializeField] GameObject quickAttackHitbox;
    [SerializeField] GameObject heavyAttackHitbox;
    [SerializeField] GameObject uppercutAttackHitbox;
    private PlayerController player;
    private float strengthMultiplicatorHeavy = 1f;
    private float cooldownHeavyAttack = 0.2f;
    private float timerInput = 0;
    private float timerAttack = 0;
    private float timerHeavyAttack = 0;
    private float timerUppercut = 0f;
    private float timerFinisher = 0f;
    private float timeAtt;

    private bool isFinisher = false;
    private bool firstFinisher = false;

    public float jumpUppercut = 750f;
    public float forceStomp = 1000;
    [HideInInspector] public bool isQAttack = false;
    [HideInInspector] public bool isHeavyHold;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimers();
        AttackUpdate();
    }

    //Instantiate the attacks when the condition are done
    private void AttackUpdate()
    {
        UppercutTimer();


        //If the attack made is a finisher the heavy attack will do an uppercut then a stomp
        if(isFinisher)
        {
            //Prevent the multiple use of uppercut for one finisher
            if(!firstFinisher)
            {
                timerUppercut += Time.deltaTime;
                //Launch the variables for the uppercut to work
                LaunchUppercut();
                GameObject attack = Instantiate(uppercutAttackHitbox, transform.position + transform.forward * 1.5f, Quaternion.identity);
                attack.GetComponent<uppercutAttack>().damage *= strengthMultiplicatorHeavy;
                firstFinisher = true;
            }

            timerFinisher += Time.deltaTime;

            //If enough time has passed we launch the stomp
            if (timerFinisher > 0.7f)
            {
                StartStomp(strengthMultiplicatorHeavy);
                timerFinisher = 0f;
                timerHeavyAttack = 0f;
                isFinisher = false;
                firstFinisher = false;
            }
        }
        else if (timerHeavyAttack > 0)
        {
            //there is a latency before the input and the start of the move
            timerHeavyAttack -= Time.deltaTime;
            if (timerHeavyAttack < 0)
            {
                //differentiate holding the button and just one press
                timerHeavyAttack = 0;

                if (isHeavyHold)
                {
                    timerUppercut += Time.deltaTime;
                    //Launch the variables for the uppercut to work
                    LaunchUppercut();
                    GameObject attack = Instantiate(uppercutAttackHitbox, transform.position + transform.forward * 1.5f, Quaternion.identity);
                    attack.GetComponent<uppercutAttack>().damage *= strengthMultiplicatorHeavy;
                }
                else
                {
                    Instantiate(heavyAttackHitbox   , transform.position + transform.forward * 1.5f, Quaternion.identity);
                }
            }
        }
    }

    //Handle the variables and the timers for the uppercut and after it
    private void UppercutTimer()
    {
        if(timerUppercut > 0)
        {
            timerUppercut += Time.deltaTime;
            player.isGrounded = false;
            if(timerUppercut > 0.5f)
            {
                timerUppercut = 0f;
                player.isJumping = false;
                player.player.state = PlayerState.CLASSIC;
            }
        }    
    }

    //Set the player as in the air, add a velocity Y and constraints the rotation to avoid bugs
    private void LaunchUppercut()
    {
        if(player.isGrounded)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            player.isJumping = true;
            player.isGrounded = false;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rb.velocity = new Vector3(rb.velocity.x, jumpUppercut * Time.deltaTime, rb.velocity.z) ;
        }
    }

    //Every attacks timer are handled here
    private void HandleTimers()
    {
        timeAtt += Time.deltaTime;

        //If the input is long enough we add the holding in the buffer
        if (timerInput > 0)
        {
            timerInput += Time.deltaTime;
            if (timerInput > 0.3f)
            {
                player.buffer.buffer.Add(Inputs.HOLDHEAVY);
                timerInput = 0f;
            }
        }

        if (isQAttack)
            if (timerAttack < timeAtt - 0.2)
                isQAttack = false;

    }

    //Add the quick attack input in the buffer
    public void StartQuickAttack(InputAction.CallbackContext context)
    {
        if (context.started && (player.player.state == PlayerState.CLASSIC || player.player.state == PlayerState.COMBO_MOON))
        {
            player.buffer.buffer.Add(Inputs.QUICK);
        }
    }

    //Add the heavy attack input in the buffer
    //If the button is hold enough it will be added as hold in the handle timer function
    public void StartHeavyAttack(InputAction.CallbackContext context)
    {
        if(context.started)
            timerInput += Time.deltaTime;

        if(context.canceled && (player.player.state == PlayerState.CLASSIC || player.player.state == PlayerState.COMBO_MOON))
        {        
            if(timerInput != 0)
            {
                player.buffer.buffer.Add(Inputs.HEAVY);
                timerInput = 0f;
            }
        }
    }

    //Make the quick attack
    public void QuickAttack(Attack strength)
    {
        GameObject quick = Instantiate(quickAttackHitbox, transform.position + transform.forward * 1.5f, Quaternion.identity);
        timerAttack = timeAtt;
        
        switch(strength)
        {
            case Attack.QUICK:
                player.animationPlayer.StartTriggerQuickAttack();
                break;
            case Attack.MEDIUM:
                if(!player.buffer.hasLitlleHitInMedium)
                    HeavyAttack(false, Attack.QUICK);
                else
                    player.buffer.finisher = true;
                
                break;
            default:
                break;
        }
    }

    //Launch the heavy attack, it can be holded or in the air or both
    public void HeavyAttack(bool isHold , Attack strength ,bool finish = false)
    {
        if(isHold)
        {
            isFinisher = finish;      
            if (!player.isGrounded)
            {
                //Stomp is stronger if the button is hold and even stronger if in finisher
                if (finish)
                    StartStomp(3f);
                else
                    StartStomp(2f);
            }
            else
            {
                //Make the player as combo state to make action easier
                player.player.state = PlayerState.COMBO_MOON;
                player.animationPlayer.StartTriggerUppercut();
                timerAttack = timeAtt;
                timerHeavyAttack = cooldownHeavyAttack;
            }
            switch(strength)
            {
                case Attack.MEDIUM:
                    strengthMultiplicatorHeavy = 2f;
                    break;
                case Attack.FINISHER:
                    strengthMultiplicatorHeavy = 3f;
                    break;
                default:
                    strengthMultiplicatorHeavy = 1f;
                    break;
            }
        }
        else
        {          
            if (!player.isGrounded)
            {
                StartStomp();
            }
            else
            {
                player.animationPlayer.StartTriggerStrongAttack();
                timerAttack = timeAtt;
                timerHeavyAttack = cooldownHeavyAttack;
            }
        }
        isHeavyHold = isHold;

    }

    //Launch the stomp
    private void StartStomp(float forceMultiplicator = 1.0f)
    {
        player.animationPlayer.ResetTrigger("Jump");
        player.animationPlayer.StartTriggerStomp();
        player.isStomp = true;
        player.YVelocity = -forceStomp * forceMultiplicator;
    }
}
                                                                                                                                  