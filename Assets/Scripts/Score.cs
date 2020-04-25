using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	//public Transform player;
	[SerializeField] PlayerController pc; 
	public Text scoreText;
	float lap_stop = 0.0f;


	void Update()
    {
		scoreText.text = pc.num_coins.ToString();//((int)(Time.time - lap_stop)).ToString();
    }

	public void Reset()
	{
		lap_stop = Time.time;

	}
}

