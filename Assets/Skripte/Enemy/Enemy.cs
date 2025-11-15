using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class Enemy : MonoBehaviour
{
    [SerializeField] float speed=2f;
    [SerializeField] float damage=10f;
    [SerializeField] public float health= 10f;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip hurtClip;

    UIController ui;

    
    private bool log=false;
    Animator animator;
    AudioSource source;
    NavMeshAgent agent;
    Transform player;
    GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ui=GameObject.Find("Canvas").GetComponent<UIController>();
        target=GameObject.FindGameObjectWithTag("Player");

        animator=GetComponent<Animator>();
        player=gameObject.transform;
        source=GetComponent<AudioSource>();

        agent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            source.clip=deathClip;
            if(!source.isPlaying && !log){
                log=true;
                source.Play();}
            StartCoroutine(deathSequence());
            
            return;
        }
        Move();


    }

    void Move()
    {
        agent.SetDestination(target.transform.position);
        animator.SetBool("move", target != null);
    }

    IEnumerator deathSequence()
    {
        animator.Play("Death");
        yield return new WaitForSeconds(3f);
        ui.addKill();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }

    public void playerIsHurt()
    {
        source.clip=hurtClip;
        source.Play();
    }

}
