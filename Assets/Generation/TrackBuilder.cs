//using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TrackBuilder : MonoBehaviour
{
    [SerializeField] GameObject[] trackPieces;// = new GameObject[3];

    [SerializeField] GameObject[] track;
    public int trackLength;
    [SerializeField] int step;
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
        Vector3 offset = trackPieces[0].GetComponent<Transform>().position;
        Vector3 currRotation = trackPieces[0].GetComponent<Transform>().rotation.eulerAngles;
        //currRotation.x = -1;
        int previousIndex = 0;
        int direction = 0;
        int pieceIndex = 0;
        //offset.x -= step;
        track[0] = Instantiate(trackPieces[pieceIndex]);
        track[0].transform.position = new Vector3(offset.x, offset.y, offset.z);


        //for all in track[]
        for (int i = 1; i < trackLength; i++)
        {
            UnityEngine.Debug.LogError(direction);
            
            //currRotation.x = -90;
            pieceIndex = Random.Range(0, 5);
            if (pieceIndex >= 3)
                pieceIndex = 0;
            //if (pieceIndex == 1)
            //    currRotation.y += 180;
            //if (pieceIndex == 2)
            //    currRotation.y += 90;
            currRotation = trackPieces[pieceIndex].transform.eulerAngles;
            if (previousIndex == 1)
            {
                direction = (++direction)%4;
                currRotation.y -= 90;
                // currRotation.y %= 360;
            }
            else if (previousIndex == 2)
            {
                direction += 3;
                direction %= 4;
               currRotation.y += 90;
               //currRotation.y %= 360;
            }
            //currRotation.z = 0;
            //currRotation.y += 90;
            //currRotation.x = -90;
            if (direction == 2)
            { 
                offset.x += step;
            }
            else if (direction == 3)
            {
                offset.z -= step;
            }
            else if (direction == 0)
            {
                offset.x -= step;
            }
            else if (direction == 1)
            {
                offset.z += step;
            }
            //currPosition.x += 3;

            track[i] = Instantiate(trackPieces[pieceIndex]);
            track[i].transform.position = new Vector3(offset.x, offset.y, offset.z);
            track[i].transform.eulerAngles = currRotation;
            
            //randomly instantiate track piece
            //do more straight than turn
            // 4:1 OR 3:1 
            //if a turn 
            //randomly decide right or left turn
            previousIndex = pieceIndex;
        }

        return;
    }
}
