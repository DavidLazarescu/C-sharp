using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonLookAtPlayer : MonoBehaviour
{
    private Transform cam;
    private Transform player;

    void Awake()
    {
        cam = Camera.main.transform;
        player = FindObjectOfType<PlayerManager>().gameObject.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 tempPlayerPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(tempPlayerPos);
    }
}
