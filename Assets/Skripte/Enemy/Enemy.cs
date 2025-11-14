using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class Enemy : MonoBehaviour
{
    [SerializeField] float speed=2f;
    [SerializeField] float damage=10f;
    [SerializeField] public float health= 10f;
    

    Animator animator;
    NavMeshAgent agent;
    Transform player;
    GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target=GameObject.FindGameObjectWithTag("Player");

        animator=GetComponent<Animator>();
        player=gameObject.transform;

        agent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
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
        Destroy(gameObject);
    }

}
