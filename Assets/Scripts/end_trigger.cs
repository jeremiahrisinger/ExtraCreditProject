using UnityEngine;

public class end_trigger : MonoBehaviour
{
	//public int laps = 3;
	//public int onLap = 0;
	public GameManager gameMan;
	//public Canvas canvas;
	public GameObject inGameScore;
	public lapCounter lapCounter;
	public lapCounter lapCounter2;
	public Score timer;
	public Score timer2;
	public PlayerController p1;
	public PlayerController p2;
	public bool p1Finished;
	public bool p2Finished;
	// Start is called before the first frame update
	void OnTriggerEnter(Collider target)
	{
		//gameMan = GetComponent<GameManager>();

		//canvas = GetComponent<Canvas>();
		if (target.tag == "p1")
		{
			p1Finished = lapCounter.AddLap();
			p1.num_coins += (lapCounter.onLap - lapCounter2.onLap);
			Debug.Log(p1Finished);
			//p1.TakeDmg(1);
			p1.num_coins += lapCounter.onLap;
			timer.Reset();
		}
		else if(target.tag == "p2")
		{
			p2Finished = lapCounter2.AddLap();
			p2.num_coins += (lapCounter2.onLap-lapCounter.onLap);
			Debug.Log(p2Finished);
				p2.num_coins += lapCounter2.onLap;
			//p2.TakeDmg(1);
			timer2.Reset();
		}
		if (p1Finished || p2Finished)
		{
			if(p1.num_coins> p2.num_coins)
			{
				inGameScore.SetActive(false);
				Invoke("Blue",2);
				//gameMan.BlueWins();
				Invoke("ToMain", 15);
			}
			else 
			{
				inGameScore.SetActive(false);
				Invoke("Red", 2);
				Invoke("ToMain", 15);
				//gameMan.RedWins();
			}
		}

	}
	public void Red()
	{
		gameMan.RedWins();
	}

	public void Blue()
	{
		gameMan.BlueWins();
	}
	public void ToMain()
	{
		gameMan.ToMainMenu();
	}
}