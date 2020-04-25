using UnityEngine;
using UnityEngine.UI;

public class lapCounter : MonoBehaviour
{
	public int onLap = 0;
	public int totalLaps = 4;
	//public Transform player;
	public GameObject endWall;
	public Text laps;
	void Update()
	{
		laps.text = ((int)onLap).ToString();
	}

	public bool AddLap() 
	{
		Debug.Log(onLap+1);
		onLap++;
		if (onLap > totalLaps)
		{
			Debug.Log("TRUE");
			return true;
		}
		Debug.Log("FALSE");
		return false;
	}
}
