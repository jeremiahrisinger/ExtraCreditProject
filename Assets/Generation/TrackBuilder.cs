using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder : MonoBehaviour
{
    [SerializeField] GameObject[] trackPieces;// = new GameObject[3];

    [SerializeField] GameObject[] track;
    public int trackLength;
    // Start is called before the first frame update
    void Start()
    {
        track = new GameObject[trackLength];
        BuildTrack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void BuildTrack()
    { 
        //for all in track[]
            //randomly instantiate track piece
            //do more straight than turn
                // 4:1 OR 3:1 
            //if a turn 
                //randomly decide right or left turn
        //for loop done

        //return
    }
}
