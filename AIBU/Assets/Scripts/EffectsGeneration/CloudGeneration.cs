using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGeneration : MonoBehaviour
{
    public float spawnHeight;
    public float heightVariation;
    public int spawnAreaX;
    public int spawnAreaZ;

    public int cloudAmount;
    public GameObject cloudGameObject;

    private void Start()
    {
        float spawnMinHeigh = spawnHeight - heightVariation;

        for (int i = 0; i < cloudAmount; i++) 
        {
            Vector3 spawnPosition = new Vector3(Random.Range(0, transform.position.x + spawnAreaX),
                Random.Range(spawnMinHeigh, spawnHeight), 
                Random.Range(0, transform.position.z + spawnAreaZ));
            Instantiate(cloudGameObject, spawnPosition, Quaternion.identity, this.transform);
        }
        
    }
}
