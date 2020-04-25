using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        transform.eulerAngles = new Vector3(0f, player.rotation.eulerAngles.y, 0f);
    }
}
