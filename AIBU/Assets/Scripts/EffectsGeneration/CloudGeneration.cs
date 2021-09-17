using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGeneration : MonoBehaviour
{
    public int horizonatalStackSize = 20;
    public float cloudHeight;
    public Mesh quadMesh;
    public Material cloudMaterial;
    float offset;

    public int layer;
    public Camera camera;
    private Matrix4x4 matrix;

    // Update is called once per frame
    private void Start()
    {
        offset = cloudHeight / horizonatalStackSize / 2f;
        Vector3 startPosition = transform.position + (Vector3.up * (offset * horizonatalStackSize / 2f));

        //Debug.Log("in CloudGeneration, update");

        for (int i = 0; i < horizonatalStackSize; i++) 
        {
            //Debug.Log("in CloudGeneration, updates for-loop");
            matrix = Matrix4x4.TRS(startPosition - (Vector3.up * offset * i), transform.rotation, transform.localScale);
            Graphics.DrawMesh(quadMesh, matrix, cloudMaterial, layer, camera, 0, null, true, false, false);
        }
    }
}
