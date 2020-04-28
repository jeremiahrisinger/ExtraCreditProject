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

    void BuildTrack()
    {
        Vector3 position = trackPieces[0].transform.position;
        Vector3 currRotation = trackPieces[0].transform.rotation.eulerAngles;
        
        //currRotation.x = -1;
        int previousIndex = 0;
        int direction = 0;
        int pieceIndex = 0;
        //position.x -= step;
        track[0] = Instantiate(trackPieces[pieceIndex]);
        track[0].transform.position = new Vector3(position.x, position.y, position.z);


        //for all in track[]
        for (int i = 1; i < trackLength; i++)
        {
            UnityEngine.Debug.LogError(direction);

            pieceIndex = Random.Range(0, 10);
            
            if (pieceIndex >= 6)
                pieceIndex = 0;

            if (direction == 0)
            { 
                position.x += step;
            }
            else if (direction == 1)
            {
                position.z -= step;
            }
            else if (direction == 2)
            {
                position.x -= step;
            }
            else if (direction == 3)
            {
                position.z += step;
            }
            //position.x += step;

            if (pieceIndex == 2)
            {
                
                currRotation.y += 90.0f;
                currRotation.y %= 360f;
            }
            if (pieceIndex == 4) //UP
            {
                currRotation.y += 180f;
            }
            if (pieceIndex == 5) //DOWN
            {
                position.y -= 1.2f;
            }
            //currRotation.x = -89.98f;
            track[i] = Instantiate(trackPieces[pieceIndex]);
            track[i].transform.position = new Vector3(position.x, position.y, position.z);
            track[i].transform.eulerAngles = currRotation;

            if (pieceIndex == 1)
            {
                direction = (++direction) % 4;
                currRotation.y += 90f;
                currRotation.y %= 360f;
            }
            else if (pieceIndex == 2)
            {
                direction += 3;
                direction %= 4;
                currRotation.y -= 180f;
                currRotation.y %= 360f;
            }
            else if (pieceIndex == 4) // UP
            {
                currRotation.y -= 180f;
                position.y += 1.2f;
            }


            previousIndex = pieceIndex;
        }

        return;
    }
}
