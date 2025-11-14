using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance=3f;
    [SerializeField] float height=3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        transform.position=target.position-new Vector3(0f, height, distance);
    }
}
