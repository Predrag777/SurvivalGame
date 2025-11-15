using UnityEngine;

public class Bolt : MonoBehaviour
{
    [HideInInspector] public Vector3 endPoint;
    [Header("Bolt Settings")]
    [SerializeField] float rayHeight=2f;
    [SerializeField] float effectDuration=0.75f;
    [SerializeField] float phaseDuration=0.1f;

    [Header("Bolt rendering")]
    [SerializeField] LineRenderer rayRenderer;
    [SerializeField] AnimationCurve[] rayPhases;
    [SerializeField] UIController ui;

    int phaseIndex=0;
    float timeToChangePhase;
    float timeSinceEffectStarted;
    Vector3 vectorOfBolt;

    void OnEnable()
    {
        timeToChangePhase=0f;
        timeSinceEffectStarted=0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceEffectStarted += Time.deltaTime;

        if (timeSinceEffectStarted >= effectDuration)
            gameObject.SetActive(false);

        Vector3 dir = endPoint - transform.position;
        float dist = dir.magnitude;

        // Provjera prepreke
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir.normalized, out hit, dist))
        {
            endPoint = hit.point;
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Pogadjam");
                Enemy enemy=hit.collider.GetComponent<Enemy>();
                enemy.health-=2;
                if (enemy.health > 0f)
                {
                    enemy.playerIsHurt();
                }
                else
                {
                    ui.addKill();
                }
            }
        }

        vectorOfBolt = endPoint - transform.position;

        if (timeSinceEffectStarted >= timeToChangePhase)
        {
            timeToChangePhase = timeSinceEffectStarted + phaseDuration;
            ChangePhase();
        }
    }


    void ChangePhase()
    {
        phaseIndex++;

        if(phaseIndex >= rayPhases.Length) 
            phaseIndex = 0;

        AnimationCurve curve = rayPhases[phaseIndex];

        rayRenderer.positionCount = curve.keys.Length;

        for(int i = 0; i < curve.keys.Length; i++)
        {
            Keyframe key = curve.keys[i];

            Vector3 newPoint = transform.position + vectorOfBolt * key.time;
            newPoint += Vector3.up * key.value * rayHeight; 
            rayRenderer.SetPosition(i, newPoint);
        }
    }

}
