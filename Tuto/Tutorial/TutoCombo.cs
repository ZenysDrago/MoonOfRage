using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutoCombo : MonoBehaviour
{
    private TutoMoon moon;
    private Rigidbody sphereBody;
    private float timerInCoroutine = 0f;
    private float timerRecall = 0f;

    [HideInInspector] public bool isInCoroutine = false;
    [HideInInspector] public bool isInCollider = false;

    public GameObject sphere;
    public GameObject player;
    public AnimationCurve curveSlamUp;
    public AnimationCurve curveSlamDown;
    public GameObject pointAbovePlayer;

    public bool startSlam = false;
    public float spinDistPlayerSphere;
    public float spinTime = 2;
    public float recallTime = 0.2f;
    public float spinSpeed = 100;
    public float slamTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        moon = sphere.GetComponent<TutoMoon>();

        sphereBody = sphere.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SpinningCombo(float recallTime, float spinSpeed, float spinTime)
    {
        timerInCoroutine = 0f;
        isInCoroutine = true;
        Vector3 distPlayerSphere = player.transform.position - sphere.transform.position;
        float recallSpeed = distPlayerSphere.magnitude / recallTime;
        float spinTimer = 0;

        while (distPlayerSphere.magnitude > spinDistPlayerSphere)
        {
            timerInCoroutine += Time.deltaTime;
            distPlayerSphere = player.transform.position - sphere.transform.position;
            sphereBody.velocity = distPlayerSphere.normalized * recallSpeed;

            if (timerInCoroutine > 1f)
                break;

            yield return null;
        }

        TutoControls p = player.GetComponent<TutoControls>();
        p.chainController.CreateChain();
        p.LockPlayer();
        p.animationPlayer.SetLink(true);

        while (spinTimer < spinTime)
        {
            sphereBody.velocity = Vector3.zero;
            sphere.transform.LookAt(player.transform);
            sphereBody.velocity = sphere.transform.right * spinSpeed;

            spinTimer += Time.deltaTime;
            yield return null;
        }

        sphereBody.velocity /= 2;
        isInCoroutine = false;
        p.chainController.DestroyLink();
        p.UnlockPlayer();
        p.animationPlayer.SetLink(false);
    }

    public IEnumerator SlamCombo(float recallTime)
    {
        timerInCoroutine = 0f;
        isInCoroutine = true;
        Vector3 MoonInitPos = sphere.transform.position;
        TutoControls p = player.GetComponent<TutoControls>();
        startSlam = true;

        while (!isInCollider)
        {
            timerInCoroutine += Time.deltaTime;
            timerRecall += Time.deltaTime;
            float timerOnOne = timerRecall / recallTime;
            sphere.transform.position = Vector3.Lerp(MoonInitPos, pointAbovePlayer.transform.position, curveSlamUp.Evaluate(timerOnOne));

            if (timerInCoroutine > recallTime * 1.5)
                break;

            yield return null;
        }
        timerRecall = 0f;
        moon.asHitSlam = false;

        while (!moon.asHitSlam || moon.isOnGround)
        {
            timerInCoroutine += Time.deltaTime;
            timerRecall += Time.deltaTime;
            float timerOnOne = timerRecall / recallTime;

            if (timerInCoroutine > recallTime * 1.5)
                break;

            if (timerOnOne < 0.9)
                sphere.transform.position = Vector3.Lerp(pointAbovePlayer.transform.position, p.impactMoonOnSlam.transform.position, curveSlamDown.Evaluate(timerOnOne));
            else
                sphereBody.velocity += new Vector3(0, -25, 0) * Time.deltaTime;

            yield return null;
        }

        moon.asHitSlam = false;
        timerRecall = 0f;
        sphereBody.velocity = Vector3.zero;
        isInCoroutine = false;
    }

    public void Spin(InputAction.CallbackContext context)
    {
        if (context.started)
            StartCoroutine(SpinningCombo(recallTime, spinSpeed, spinTime));
    }
}
