using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGeneration : MonoBehaviour
{
    public float spawnHeight;
    public float heightVariation;
    public int spawnArea;

    public int cloudAmount;
    public GameObject cloudGameObject;

    private void Start()
    {
        for (int i = 0; i < cloudAmount; i++) 
        {
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - spawnArea, transform.position.x + spawnArea),
                Random.Range(-spawnHeight, spawnHeight), 
                Random.Range(transform.position.z - spawnArea, transform.position.z + spawnArea));
            Instantiate(cloudGameObject, spawnPosition, Quaternion.identity, this.transform);
        }
        
    }
}
