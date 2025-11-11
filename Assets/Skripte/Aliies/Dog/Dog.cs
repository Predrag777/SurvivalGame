using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dog : MonoBehaviour
{
    private float speed = 2.0f;
    private Animator animator;
    private bool isMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(performMove());
        }
    }

    IEnumerator performMove()
    {
        float moveDuration = 5.0f; 
        float elapsedTime = 0.0f;

        while (elapsedTime < moveDuration)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            animator.SetBool("move", true);
            yield return null;
        }       

        animator.SetBool("move", false);
        transform.Rotate(0, 180, 0);

        yield return new WaitForSeconds(3.0f);
        isMoving = false;
    }
    

}
