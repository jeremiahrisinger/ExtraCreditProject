using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] Transform t;
	[SerializeField] GameManager gm;
	[SerializeField] float freedom = 30;
	[SerializeField] static int maxHp = 3;
	[SerializeField] int hpLeft = maxHp;
    [SerializeField] bool living = true;
	[SerializeField] float degrees;

	public int num_coins = 0;
	public float rotSpeed = 0.0f;
	public float force = 0.0f;

	private int framesNotFlat = 0;
	//private float poppingForce = 10;
	private float eulerAngX;
	private float eulerAngY;
	private float eulerAngZ;
	private float z;
    private float x;

	void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() { 
		eulerAngX = transform.localRotation.eulerAngles.x;
		eulerAngY = transform.localRotation.eulerAngles.y;
		eulerAngZ = transform.localRotation.eulerAngles.z;// transform.localEulerAngles.z;
		degrees = transform.localRotation.eulerAngles.y;

		z = force* (float) Math.Cos((degrees-90) / 180 * Math.PI);
		x = force* (float) Math.Sin((degrees-90) / 180 * Math.PI);

		if (Input.GetKey(KeyCode.W))
		{
			rb.AddForce(x * Time.deltaTime, 0, z * Time.deltaTime, ForceMode.VelocityChange);
		}
		if (Input.GetKey(KeyCode.S))
		{
			rb.AddForce(-x * Time.deltaTime, 0, -z * Time.deltaTime, ForceMode.VelocityChange);
		}
		if (Input.GetKey(KeyCode.D)){
			if (Input.GetKey(KeyCode.S))
			{
				degrees -= (rotSpeed * Time.deltaTime);
				t.Rotate( 0, -rotSpeed * Time.deltaTime, 0);
			}
			else
			{
				degrees += (rotSpeed * Time.deltaTime);
				t.Rotate(0, rotSpeed * Time.deltaTime,0);
			}
		}
		if (Input.GetKey(KeyCode.A)){
			if (Input.GetKey(KeyCode.S))
			{
				degrees += (rotSpeed * Time.deltaTime);
				t.Rotate(0, rotSpeed * Time.deltaTime, 0);
			}
			else
			{
				degrees -= (rotSpeed * Time.deltaTime);
				t.Rotate(0, -rotSpeed * Time.deltaTime, 0);
			}
		}
		if (framesNotFlat >= 120)
		{
			Reset();
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			framesNotFlat = 0;
		}
		if (eulerAngX > freedom && eulerAngX < 360-freedom || eulerAngZ > freedom && eulerAngZ < 360-freedom)
		{
			framesNotFlat++;
		}
		else 
		{
			framesNotFlat = 0;
		}
	}

	public void Reset()
	{
		t.position = new Vector3(t.position.x, t.position.y + 2, t.position.z);
		t.rotation = Quaternion.Euler(0, t.rotation.eulerAngles.y, 0);
	}

	public void CollectCoin()
	{
		num_coins++;
	}

 
    public void die()
    {
        living = false;
		gm.Respawn();
	}
}
