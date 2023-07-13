using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header ("Air Control")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float airSpeed;
    [SerializeField] private float fallIncreasing;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] GameObject shockWave;
    [HideInInspector] public float YVelocity;
    [HideInInspector] public bool hasJumped;
    private float a;
    [HideInInspector] public bool isJumping;
    public bool hasDoubleJumped;

    [Header ("Ground Control")]
    private float pushSpeed;
    private float movementSpeed;
    private Vector3 newVelocity;
    private Vector3 rollEndPos;
    private bool isRolling = false;

    public float groundSpeed;
    public float rollDistance = 2.5f;
    public float rollSpeed = 250f;
    


    [Header ("Sphere Control")]
    [SerializeField] private GameObject sphere;
    [SerializeField] private float moonSpeed;
    [SerializeField] private float recallTime;
    private Rigidbody sphereBody;

    [HideInInspector] public ChainController chainController;
    public GameObject impactMoonOnSlam;
    public bool ballControlled;
    public bool ballAgainstPlayer;

    [Header("General Control")]
    [SerializeField] private float distPushMax = 3;
    private Transform transformPlayer;
    private RaycastHit hit;
    private float timerInCoroutine = 0f;
    private float timerStun = 0f;

    [HideInInspector] public Rigidbody rigidbodyPlayer;
    [HideInInspector] public Player player;
    [HideInInspector] public BufferCombo buffer;
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public PlayerAnimator animationPlayer;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isStomp;
    public SoundPlayer sound;
    public MoonSound soundMoon;

    //==============================
    //Basic function 
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        //transform = GetComponent<Transform>();
        rigidbodyPlayer = GetComponent<Rigidbody>();
        chainController = GetComponent<ChainController>();
        buffer = GetComponent<BufferCombo>();
        player = GetComponent<Player>();
        sphereBody = sphere.GetComponent<Rigidbody>();
        animationPlayer = GetComponent<PlayerAnimator>();
        transformPlayer = GetComponent<Transform>();

        pushSpeed = groundSpeed / 2;
        direction = new Vector2();
        YVelocity = 0;
        a = 1;
    }
        
    // Update is called once per frame
    void FixedUpdate()
    {
        CalculatePosImpact();
        PlayerVelocityModifier();
        SoundManager();
        switch (player.state)
        {
            case PlayerState.CLASSIC:
            {
                CheckPlayer();
                PlayerMovement();
                break;
            }
            case PlayerState.STUNNED:
            {

                if(timerStun == 0)
                {
                    animationPlayer.StartTriggerMajorHit();
                    timerStun += Time.deltaTime;
                }
                    
                if (isRolling)
                Roll();
                break ;
            }
            case PlayerState.COMBO_MOON:
            {
                break;
            }
            case PlayerState.CONTROLLING_MOON:
            {
                MoonMovement();
                break;
            }
            case PlayerState.PUSHING_MOON:
            {
                MoonPushing();
                break;
            }
            default:
                return;
        }
    }
    //===============================
    
    //Public function
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

        if (player.state == PlayerState.STUNNED && !isRolling)
        {
            player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg, 0);
            Roll();
        }
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (player.state != PlayerState.STUNNED)
        {
            if (player.state == PlayerState.CLASSIC && context.started)
            {
                if (hasJumped && !hasDoubleJumped)
                {
                    StartCoroutine(DoubleJump());
                    hasDoubleJumped = true;
                }
                else
                {
                    isJumping = context.ReadValueAsButton();
                }
                animationPlayer.StartTriggerJump();
            }

            if (player.state == PlayerState.PUSHING_MOON)
            {
                player.state = PlayerState.CLASSIC;
                animationPlayer.SetPush(false);
            }

            if (context.canceled)
                isJumping = context.ReadValueAsButton();
        }
        else if (player.stunTimer <= 0)
        {
            player.state = PlayerState.CLASSIC;
            animationPlayer.StartTriggerGetUp();
            timerStun = 0;
        }
    }

    public void MoonControl(InputAction.CallbackContext context)
    {
        if(player.state == PlayerState.COMBO_MOON)
        {
            if(context.started)
                buffer.buffer.Add(Inputs.LINK);
        }
        else if(player.state == PlayerState.CLASSIC || player.state == PlayerState.CONTROLLING_MOON)
        {
            if (context.ReadValue<float>() > 0.8f)
            {
                if (chainController.isChained)
                {
                    sound.SoundCreateLink();
                    player.state = PlayerState.CONTROLLING_MOON;
                    sphereBody.constraints = RigidbodyConstraints.FreezePositionY;
                    LockPlayer();
                    animationPlayer.SetLink(true);
                    animationPlayer.SetPush(false);
                    
                }
            }
            else if (context.ReadValue<float>() < 0.8f)
            {
                soundMoon.StopControll();
                sound.SoundDestroyLink();
                chainController.DestroyLink();
                player.state = PlayerState.CLASSIC;
                UnlockPlayer();
                animationPlayer.SetLink(false);
                animationPlayer.SetPush(false);
            }

        }
    }

    public void MoonPushing()
    {
        transformPlayer.eulerAngles = new Vector3(0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg, 0);

        newVelocity = new Vector3(direction.x * pushSpeed, YVelocity, direction.y * pushSpeed) * Time.deltaTime;
        rigidbodyPlayer.velocity = newVelocity;
        sphereBody.velocity = rigidbodyPlayer.velocity + newVelocity / -2;

        if ((transformPlayer.position - sphere.transform.position).magnitude > distPushMax)
        {
            player.state = PlayerState.CLASSIC;
            animationPlayer.SetPush(false);
        }
    }

    //Constrain the player depending of his state and situation
    public void LockPlayer()
    {
        if (player.state == PlayerState.PUSHING_MOON)
        {
            rigidbodyPlayer.constraints =     RigidbodyConstraints.FreezePositionY                                          | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            if (!isGrounded)
                rigidbodyPlayer.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ   | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            else
                rigidbodyPlayer.constraints = RigidbodyConstraints.FreezePosition                                           | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    public void UnlockPlayer()
    {
        rigidbodyPlayer.constraints = RigidbodyConstraints.FreezeRotationY;
        rigidbodyPlayer.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    //===============================
    //Private function

    private void SoundManager()
    {
        if (player.state != PlayerState.CLASSIC && player.state != PlayerState.PUSHING_MOON)
        {
            sound.StopMovement();
            return;
        }

        if (isGrounded && direction != Vector2.zero)
            sound.LaunchStep();
        else
            sound.StopMovement();
    }

    private void CalculatePosImpact()
    {
        if(buffer.ComboMove.startSlam)
        {
            Vector3 gapImpact = ((player.transform.position - sphere.transform.position).normalized * 4);
            impactMoonOnSlam.transform.position = new Vector3 (player.transform.position.x + gapImpact.x , player.transform.position.y, player.transform.position.z + gapImpact.z);
            buffer.ComboMove.startSlam = false;
        }
    }

    //Make the player fall for any state
    private void PlayerVelocityModifier()
    {
        if (player.state != PlayerState.CLASSIC)
        {
            if (hasJumped)
            {
                YVelocity -= a * fallIncreasing * Time.deltaTime;
                rigidbodyPlayer.velocity = new Vector3(rigidbodyPlayer.velocity.x, YVelocity , rigidbodyPlayer.velocity.z) * Time.deltaTime;
                a++;
            }
        }
    }
    
    private void CheckPlayer()
    {
        if (TryGetComponent<SpringJoint>(out SpringJoint spring))
            Destroy(spring);

        animationPlayer.SetPush(false);
        UnlockPlayer();
    }

    private void PlayerMovement()
    {
        if (!hasJumped && isJumping)
        {
            YVelocity = jumpForce;
            hasJumped = true;
            isGrounded = false;
            animationPlayer.SetGrounded(isGrounded);
        }

        if (direction != Vector2.zero)
            transformPlayer.eulerAngles = new Vector3(0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg, 0);


        newVelocity = new Vector3(direction.x * movementSpeed, YVelocity, direction.y * movementSpeed) * Time.deltaTime;
        YVelocity -= a * fallIncreasing * Time.deltaTime;
        animationPlayer.LaunchMoveAnim(direction);
  

        rigidbodyPlayer.velocity = newVelocity;
        movementSpeed = airSpeed;
        a++;
    }

    private void MoonMovement()
    {
        soundMoon.LaunchControll();
        animationPlayer.SetLink(true);
        transformPlayer.LookAt(sphere.transform);
        sphereBody.AddForce(new Vector3(direction.x, 0, direction.y) * moonSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    private void Roll()
    {
        if(!isRolling)
        {
            animationPlayer.StartTriggerRoll();
            Vector2 normDir = direction.normalized;
            rollEndPos = new Vector3(player.transform.position.x + normDir.x * rollDistance , player.transform.position.y , player.transform.position.z + normDir.y * rollDistance);
            isRolling = true;
        }

        rigidbodyPlayer.velocity = (rollEndPos - player.transform.position).normalized * rollSpeed * Time.deltaTime;
        Vector3 distEndRoll = player.transform.position - rollEndPos;

        if(distEndRoll.magnitude < 0.2f)
        {
            isRolling = false;
            player.state = PlayerState.CLASSIC;
            timerStun = 0;
        }
    }

    
    //===============================
    //Event function
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            hasJumped = false;
            hasDoubleJumped = false;
            isGrounded = true;
            YVelocity = 0;
            a = 0;
            movementSpeed = groundSpeed;
            animationPlayer.SetGrounded(isGrounded);

            //Lock the player when touching the ground and in control to avoid unwanted jump
            if (player.state == PlayerState.CONTROLLING_MOON)
                LockPlayer();
        }

        if (collision.gameObject.layer == 9)
        {
            ballAgainstPlayer = true;

            if (Physics.Raycast(transformPlayer.position, transformPlayer.forward, out hit, 5f) && hit.transform.gameObject.layer == 9)
            {
                if(player.state == PlayerState.CLASSIC && !hasJumped)
                {
                    animationPlayer.SetPush(true);
                    player.state = PlayerState.PUSHING_MOON;
                }
            }
        }
        
        if(isStomp)
        {
            sound.LaunchShockWave();
            float powerStomp = rigidbodyPlayer.velocity.y;
            isStomp = false;
            GameObject stomp = Instantiate(shockWave, new Vector3(transformPlayer.position.x, transformPlayer.position.y - 1, transformPlayer.position.z), Quaternion.identity);
            stomp.GetComponent<ShockWave>().damage = -powerStomp;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            ballAgainstPlayer = false;
        }
        
    }

    //==========================================
    //  Coroutine
    private IEnumerator DoubleJump()
    {
        timerInCoroutine = 0f;
        float spherePlayerDist = (transformPlayer.position - sphere.transform.position).magnitude;
        float recallSpeed = spherePlayerDist / recallTime;
        Vector3 spherePlayerDirection = (transformPlayer.position - sphere.transform.position).normalized;

        while ((sphere.transform.position.x > transformPlayer.position.x + 1 || sphere.transform.position.x < transformPlayer.position.x - 1 ||
               sphere.transform.position.z > transformPlayer.position.z + 1 || sphere.transform.position.z < transformPlayer.position.z - 1) && !ballAgainstPlayer)
        {
            timerInCoroutine += Time.deltaTime;

            spherePlayerDirection = (transformPlayer.position - sphere.transform.position + Vector3.down * 2).normalized;
            Vector3 newVelocity = spherePlayerDirection * recallSpeed;
            sphere.GetComponent<Rigidbody>().velocity = newVelocity;

            //Allow the coroutine to stop if the ball get stuck and take to long
            if (timerInCoroutine > 1f)
                break;

            yield return null;
        }
        a = 0;
        YVelocity = doubleJumpForce;
        sphere.GetComponent<Rigidbody>().velocity = Vector3.down * 15;
    }
}
