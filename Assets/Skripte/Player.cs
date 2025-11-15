using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float health=10f;
    Animator animator;
    [SerializeField] private AudioClip deathClip;
    AudioSource source;
    bool log=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            source.clip=deathClip;
            if(!log && !source.isPlaying)
            {
                source.Play();
                animator.Play("Death");
                log=true;
            }
        }
    }
}
