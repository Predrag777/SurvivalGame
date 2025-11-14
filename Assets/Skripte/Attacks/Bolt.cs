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
        timeSinceEffectStarted+=Time.deltaTime;
        if(timeSinceEffectStarted>=effectDuration)
            gameObject.SetActive(false);
        vectorOfBolt=endPoint-transform.position;

        if (timeSinceEffectStarted >= timeToChangePhase)
        {
            timeToChangePhase=timeSinceEffectStarted+phaseDuration;

            ChangePhase();
        }

    }

    void ChangePhase()
    {
        phaseIndex++;

        if(phaseIndex>=rayPhases.Length) phaseIndex=0;

        AnimationCurve curve=rayPhases[phaseIndex];

        rayRenderer.positionCount=curve.keys.Length;

        for(int i = 0; i < curve.keys.Length; i++)
        {
            Keyframe key=curve.keys[i];

            Vector3 newPoint=transform.position+vectorOfBolt*key.time;

            endPoint+=Vector3.up*key.value*rayHeight;

            rayRenderer.SetPosition(i, newPoint);
        }
    }
}
