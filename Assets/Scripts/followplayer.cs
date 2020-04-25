using UnityEngine;
using System;

public class followplayer : MonoBehaviour
{
	public Transform player;
 //   public Transform Player2;
	//[SerializeField] Vector3 posOffset;
 //   [SerializeField] Vector3 rotOffset;
 //   //public float distBtwn;

 //   public float xRot;
 //   public float yRot;
 //   public float zRot;
 //   float z;
 //   float x;
 //   float y;

    void Update()
    {
        float yDiff  = transform.position.y - player.position.y;
        float zDiff =  transform.position.z - player.position.z;

        transform.eulerAngles = new Vector3( 0, player.rotation.eulerAngles.y, 0);
        transform.position = player.position;
    }
}
