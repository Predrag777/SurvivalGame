using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    Player player;

    Animator animator;
    CharacterController agent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player=GetComponent<Player>();
        agent=GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.health<=0f) return;
        performMove();
        performRotation();
    }

    void performMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0f, vertical);
        animator.SetBool("move", move != Vector3.zero);

        Vector3 step=transform.TransformDirection(move);
        //transform.Translate(move * speed * Time.deltaTime);
        agent.Move(step*speed*Time.deltaTime);
    }
    
    void performRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 lookDirection = point - transform.position;
            lookDirection.y = 0; 
            if (lookDirection.sqrMagnitude > 0.001f)
            {
                Quaternion rotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }
        }
    }

}
