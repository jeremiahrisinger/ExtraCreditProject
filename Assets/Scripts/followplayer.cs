using UnityEngine;
using System;

public class followplayer : MonoBehaviour
{
	public Transform player;
    //public Transform Player2;
	[SerializeField] Vector3 posOffset;
    [SerializeField] Vector3 rotOffset;
    //public float distBtwn;

    public float xRot;
    public float yRot;
    public float zRot;
    float z;
    float x;
    float y;
    // Update is called once per frame
    void Update()
    {
        float yDiff  = transform.position.y - player.position.y;
        float zDiff =  transform.position.z - player.position.z;

       // z = player.position.z;
       // x = player.position.x;
        //y = player.position.y;
        //transform.position = new Vector3(x+posOffset.x, y+ posOffset.y, z + posOffset.z);
        //transform.Rotate(-player.eulerAngleX,0, -player.eulerAngleZ);
        transform.eulerAngles = new Vector3( Mathf.Asin(yDiff/zDiff), 
                                             player.rotation.eulerAngles.y-90.0f, 
                                            -player.rotation.eulerAngles.x
                                            );
    }
}
