using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEndCredits : MonoBehaviour
{
    private Collider col;

    void Start()
    {
        col = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }
    }
}
