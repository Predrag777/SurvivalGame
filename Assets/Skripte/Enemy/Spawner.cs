using UnityEngine;
using System.Collections;
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject  prefab;
    [SerializeField] private int timeToSpawn;
    bool isSpawn=true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawn){
            StartCoroutine(spawn());
        }
    }

    IEnumerator spawn()
    {
        isSpawn=false;
        yield return new WaitForSeconds(timeToSpawn);
        GameObject character=Instantiate(prefab, transform.position, Quaternion.identity);
        isSpawn=true;
    }

}
