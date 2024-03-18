using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Follow : MonoBehaviour
{
    RectTransform rect;
    public GameObject player;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.transform.position);
        playerScreenPos.y += 50;
        rect.position = playerScreenPos;
    }
}
