using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 offsett = new Vector3(0,0,-10);
    [SerializeField] private Transform player;

    void LateUpdate()
    {
        transform.position = player.position + offsett;
    }
}
