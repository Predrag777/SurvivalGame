using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] ParticleSystem explosion;
    [SerializeField] int numberOfAttacks;
    float range = 200f;
    [SerializeField] LayerMask strikeableMask;

    [Header("UI")]
    int attackIndex = 0;
    float attackCooldown = 0.5f;
    float timeOfLastAttack = 0f;
    bool canAttack = true;

    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    public void Fire()
    {

        // Ray iz kamere do pozicije miša
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("Shoot");
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Pogadjam");
            explosion.transform.position = hit.point;
            
            explosion.transform.rotation = Quaternion.LookRotation(hit.normal);

            explosion.Play();
            timeOfLastAttack = Time.time;
        }
    }

    bool ReadyToAttack()
    {
        return Time.time >= timeOfLastAttack + attackCooldown;
    }
}
