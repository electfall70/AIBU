using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public List<GameObject> objectsToPlace;
    public Transform parent;

    void PlaceObjects()
    {
        for (int y = 0; y < 256; y++)
        {
            for (int x = 0; x < 256; x++)
            {
                Ray ray = new Ray(new Vector3(x, y, 100), Vector3.down);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    GameObject toPlace = Instantiate(objectsToPlace[0],parent);
                    toPlace.transform.position = hitInfo.point;
                    toPlace.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                }
            }
        }
    }
}
