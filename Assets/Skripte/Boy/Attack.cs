using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem light;
    [SerializeField] int numberOfAttacks;
    float range = 200f;
    [SerializeField] LayerMask strikeableMask;
    AudioSource source;
    [SerializeField] AudioClip boltClip;

    [Header("UI")]
    int attackIndex = 0;
    float attackCooldown = 0.5f;
    float timeOfLastAttack = 0f;
    bool canAttack = true;

    [SerializeField] Bolt lightningPrefab; 
    Transform firePoint;


    Camera mainCam;

    void Start()
    {
        firePoint=gameObject.transform;
        source=GetComponent<AudioSource>();
        mainCam = Camera.main;

        explosion.gameObject.SetActive(false);
        light.gameObject.SetActive(false);

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            light.gameObject.SetActive(true);
            light.Play();
            Fire();
        }
    }

    public void Fire()
    {

        // Ray iz kamere do pozicije miša
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            Vector3 targetPoint = hit.point;

            Vector3 direction=(targetPoint-firePoint.position).normalized;
            float distance = Vector3.Distance(firePoint.position, targetPoint);

            RaycastHit trueHit;
            Vector3 finalHitPoint=targetPoint;

            if(Physics.Raycast(firePoint.position, direction, out trueHit, distance))
            {
                finalHitPoint=trueHit.point;
            }


            light.gameObject.SetActive(true);

            lightningPrefab.transform.position = firePoint.position; 
            lightningPrefab.endPoint = finalHitPoint;
            lightningPrefab.gameObject.SetActive(true); 


            explosion.gameObject.SetActive(true);
            explosion.transform.position = finalHitPoint;
            
            explosion.transform.rotation = Quaternion.LookRotation(hit.normal);
            source.clip=boltClip;
            source.Play();
            explosion.Play();
            timeOfLastAttack = Time.time;

            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Pogadjam");
                Enemy enemy=hit.collider.GetComponent<Enemy>();
                enemy.health-=2;
                if (enemy.health > 0f)
                {
                    enemy.playerIsHurt();
                }
            }
        }
    }

    bool ReadyToAttack()
    {
        return Time.time >= timeOfLastAttack + attackCooldown;
    }
}
