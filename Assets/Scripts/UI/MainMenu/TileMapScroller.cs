using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapScroller : MonoBehaviour
{

    public GameObject objectPrefab;
    public Transform player;
    public float detectionDistance = 38f; 

    private GameObject currentObject;
    private Vector3 lastPlayerPosition; 

    void Start()
    {
        lastPlayerPosition = player.position;
        currentObject = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity, this.transform); 
    }

    void Update()
    {
        Vector3 playerMovement = player.position - lastPlayerPosition;
        if (playerMovement.magnitude > 0)
        {
            Vector3 objectPosition = currentObject.transform.position;
            if (Mathf.Abs(player.position.x - objectPosition.x) >= detectionDistance ||
                Mathf.Abs(player.position.y - objectPosition.y) >= detectionDistance)
            {

                GenerateNewObject();
            }

            lastPlayerPosition = player.position;
        }
    }

    void GenerateNewObject()
    {
        Vector3 newObjectPosition = currentObject.transform.position + new Vector3(detectionDistance, 0, 0); 

        currentObject = Instantiate(objectPrefab, newObjectPosition, Quaternion.identity, this.transform); 
    }
}
