using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] Moon moon;
    [SerializeField] Camera camera;
    private Vector3 posArena = new Vector3();
    private SceneData sceneData;

    [SerializeField] private float posCamX, posCamY, posCamZ;
    
    [HideInInspector] public float posACamX, posACamY, posACamZ;

    public float CamHeight;

    private float deZoomdistPlayerSphere = 6f;

    private Vector3 distPlayerMoon;
    private float distSphereBordScreen;
    private float distPlayerBordScreen;
    private float distCamPlayerZ;
    private bool moveCam = true;
    private float lastDispPlayerSphere;
    [HideInInspector] public Vector3 posCollider;
    
    //Shake Variables
    private Vector3 posBeforeShake;
    private float shakeTimer = 0f;
    public float shakeDuration;
    [HideInInspector] public float decreaseFactor;
    [HideInInspector] public bool shakeCam; 
    [HideInInspector] public float shakeAmount;

    // Start is called before the first frame update    
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        sceneData = FindObjectOfType<SceneData>();
        moveCam = true;
        transform.position = player.transform.position + new Vector3(posCamX, posCamY, posCamZ);
    }

    // Update is called once per frame
    void Update()
    {
        distCamPlayerZ = (player.transform.position.z - transform.position.z)/5;
        distPlayerMoon = player.transform.position - moon.transform.position;

        if (sceneData.inArena)
        {
            transform.position = new Vector3(posCollider.x + posACamX, posACamY + CamHeight , posACamZ + distCamPlayerZ);//deZoomdistPlayerSphere = valeur a modifier pour le zoom de la camera en fonction de la distance entre la sphere et le joueur
            transform.eulerAngles = Vector3.right * distCamPlayerZ * 3;
        }
        else
        {
            distSphereBordScreen = camera.WorldToScreenPoint(new Vector3(moon.transform.position.x, moon.transform.position.y, moon.transform.position.z)).x;
            distPlayerBordScreen = camera.WorldToScreenPoint(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z)).x;
            Vector3 nextPos = new Vector3(player.transform.position.x - distPlayerMoon.x * 0.3f, player.transform.position.y + posCamY, posCamZ);//deZoomdistPlayerSphere = valeur a modifier pour le zoom de la camera en fonction de la distance entre la sphere et le joueur
            if (distSphereBordScreen < 50 || distSphereBordScreen > 1870)
            {
                if (moveCam)
                {
                    moveCam = false;
                    lastDispPlayerSphere = distPlayerMoon.x;
                }
            }
            if (moveCam == false)
            {
                if (distSphereBordScreen <= 50)
                    moon.sphereBody.AddForce(new Vector3(5, 0, 0)*40, ForceMode.Acceleration);
                else if (distSphereBordScreen >= 1870)
                    moon.sphereBody.AddForce(new Vector3(-5, 0, 0)*40, ForceMode.Acceleration);
                if (Mathf.Abs(lastDispPlayerSphere) > Mathf.Abs(distPlayerMoon.x))
                    moveCam = true;
            }
            if (distPlayerBordScreen < 50 || distPlayerBordScreen > 1870)
                player.rigidbodyPlayer.velocity = new Vector3(-player.rigidbodyPlayer.velocity.x, player.rigidbodyPlayer.velocity.y, player.rigidbodyPlayer.velocity.z);

            if (moveCam)
                transform.position = nextPos;
            transform.eulerAngles = Vector3.forward;

        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arena"))
            posArena = other.transform.position;
    }

    public IEnumerator ShakeCamera()
    {
        posBeforeShake = transform.position;
        Debug.Log("Shake start");
        while(shakeTimer < shakeDuration)
        {
            transform.position += Random.insideUnitSphere * shakeAmount;
        
            shakeTimer += Time.deltaTime * decreaseFactor;
            yield return null;
        }
        Debug.Log("Shake end");
        transform.position = posBeforeShake;
        shakeTimer = 0f;
        shakeCam = false;
    }

}


