//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using UnityEngine;



//public class TrackBuilder : MonoBehaviour
//{
//    struct piece
//{
//	int index;
//	GameObject prefab;
//	int x, y, z;
//	Vector3 posOffset;
//	Vector3 rotOffset;
//}
//    [SerializeField] GameObject[] trackPieces;// = new GameObject[3];
//    [SerializeField] GameObject player;
//    [SerializeField] piece[] track;
//    public int trackLength;
//    [SerializeField] float step;
//    [SerializeField] int size = 100;
//    bool[,,] containsPiece;
//    int direction = 0;
//    public Vector3 position;
//    public Vector3 rotation;
//    // Start is called before the first frame update
//    void Start()
//    {
//        track = new piece[trackLength+3];

//        containsPiece = new bool[size,size,size];
//        position = new Vector3(size / 2, size / 2, size / 2);
//        StartCoroutine(BuildTrack());
//        player = Instantiate(player);
//        player.transform.position = new Vector3(position.x, position.y + 2.0f, position.z);
//        //set transform

//    }
//    bool PieceAt(Vector3 position)
//    {
//        //check xyz location to make sure no other pieces are there
//        int x = Convert.ToInt32(position.x/step);
//        int y = Convert.ToInt32(position.y/1.2f);
//        int z = Convert.ToInt32(position.z/step);

//        return containsPiece[x, y, z];
//    }

//    void SetPiece(Vector3 position, bool isHere)
//    {
//        int x = Convert.ToInt32(position.x/step);
//        int y = Convert.ToInt32(position.y/1.2f);
//        int z = Convert.ToInt32(position.z/step);
//        containsPiece[x, y, z] = isHere;
//    }

//    bool XZOutOfRange(Vector3 position)
//    {
//        int x = Convert.ToInt32(position.x/step);
//        int z = Convert.ToInt32(position.z/step);
//        //x and z must be between 0 and size -3 -> must give space for turn
//        return (x < 4 || x > size/step - 4  || z < 4 || z > size/step - 4 );
//    }

//    //int YOutOfRange(Vector3 position)
//    //{
//    //    int y = (int)position.y;

//    //    //y must be between 0 and size -1 -> must give space for turn
//    //    return (y < 0 || y > size - 1 );
//    //}

//    int InstantiatePiece(int trackIndex, int pieceIndex)
//    {
//        if (PieceAt(position))
//        {
//            //SetPiece(position, true);

//            //how to check for up down?
//            //WHY IS UP DOWN STILL GOING WHEN THERE IS PIECE ABOVE/BELOW?w
//            UnityEngine.Debug.LogError("there is already a piece here... :O");
//            track[trackIndex] = Instantiate(trackPieces[6]);
//            int x = Convert.ToInt32(position.x / step);
//            int y = Convert.ToInt32(position.y / 1.2f);
//            int z = Convert.ToInt32(position.z / step);
//            int xi, yi, zi;
//            Vector3 positioni;
//            //for (all piece)
//            for (int i = 0; i < trackLength; i++)
//			{
//                //CHECK FOR NULLLLLLLLLL!
//                if (track[i] != null)
//                {
//                    positioni = track[i].transform.position;
//                    //if(currPos =  piecePos)
//                    xi = Convert.ToInt32(positioni.x / step);
//                    yi = Convert.ToInt32(positioni.y / 1.2f);
//                    zi = Convert.ToInt32(positioni.z / step);
//                    if (x == xi && y == yi && z == zi)
//                    {
//                        Destroy(track[i]);
//                        break;
//                    }
//                }
//            }

//            pieceIndex = 6;
//        }
//        else if (XZOutOfRange(position) && !(pieceIndex == 1 || pieceIndex ==2))
//        {

//            track[trackIndex] = Instantiate(trackPieces[1]);
//            pieceIndex = 1;
//            //direction = (++direction) % 4;
//            //rotation.y += 90.0f;
//            //rotation.y %= 360.0f;
//            //XZPosUpdate();
//        }
//        else if (Convert.ToInt32(position.y/1.2f) >= size/1.2f - 4 )
//        {
//            track[trackIndex] = Instantiate(trackPieces[4]);
//            pieceIndex = 4;
//        }
//        else if (Convert.ToInt32(position.y / 1.2f) <= 4 )
//        {
//            track[trackIndex] = Instantiate(trackPieces[5]);
//            pieceIndex = 5;
//            position.y -= 1.2f;
//        }
//        else
//            track[trackIndex] = Instantiate(trackPieces[pieceIndex]);
        
//        //XZPosUpdate();

//        track[trackIndex].transform.position = new Vector3(position.x, position.y, position.z);
//        SetPiece(position, true);
//        if (pieceIndex == 1 || pieceIndex == 2)
//            track[trackIndex].transform.eulerAngles = new Vector3(-90f, rotation.y, rotation.z - 180f);
//        //else if (pieceIndex == 2)
//        //    track[trackIndex].transform.eulerAngles = new Vector3(-90f, rotation.y, rotation.z +180);
//        else if (pieceIndex == 5)
//            track[trackIndex].transform.eulerAngles = new Vector3(-90f, rotation.y, rotation.z - 180f);
//        else
//            track[trackIndex].transform.eulerAngles = new Vector3(-90f, rotation.y, rotation.z);
//        return pieceIndex;
//    }

//    void XZPosUpdate()
//    {
//        if (direction == 0)
//        {
//            //UnityEngine.Debug.LogError("-x");
//            position.x -= step;
//        }
//        else if (direction == 1)
//        {
//            //UnityEngine.Debug.LogError("+z");

//            position.z += step;
//        }
//        else if (direction == 2)
//        {
//            //UnityEngine.Debug.LogError("+x");

//            position.x += step;
//        }
//        else if (direction == 3)
//        {
//            //UnityEngine.Debug.LogError("-z");

//            position.z -= step;
//        }
//    }

//    void PlacePiece(int i, int pieceIndex)
//    {
//        //position.y -= .005f;
//        //UnityEngine.Debug.LogError(direction);
//        //yield return new WaitForSeconds(1.0f);
//        pieceIndex = UnityEngine.Random.Range(0, 10);

//        if (pieceIndex >= 6)
//            pieceIndex = 0;
//        if (pieceIndex == 5)
//        {
//            position.y -= 1.2f;
//            if (PieceAt(position))
//            {
//                position.y += 1.2f;
//                pieceIndex = 0;
//            }
//            else
//            {
//                SetPiece(position, true);
//            }
//        }
//        if (pieceIndex == 4)
//        {
//            position.y += 1.2f;
//            if (PieceAt(position))
//            {
//                pieceIndex = 0;
//            }
//            else
//            {
//                SetPiece(position, true);
//            }
//            position.y -= 1.2f;
//        }


//        XZPosUpdate();
       

//        pieceIndex = InstantiatePiece(i, pieceIndex);

//        if (pieceIndex == 1)
//        {
//            direction = (++direction) % 4;
//            rotation.y += 90.0f;
//            rotation.y %= 360.0f;
//        }
//        else if (pieceIndex == 2)
//        {
//            direction += 3;
//            direction %= 4;
//            rotation.y += 270.0f;
//            rotation.y %= 360.0f;
//        }
//        else if (pieceIndex == 4) // UP
//        {
//            position.y += 1.2f;
//        }
        

//        //return i;// to make sure the index of track stays the same when doing turn arounds
//    }

//    IEnumerator BuildTrack()
//    {
//        //Vector3 position = new Vector3(size / 2, size / 2, size / 2);
//        rotation = trackPieces[0].transform.rotation.eulerAngles;

//        direction = 0;
//        int pieceIndex = 0;

//        InstantiatePiece(0, 0);

//        //for all in track[]
//        for (int i = 1; i < trackLength; i++)
//        {
//            yield return new WaitForSeconds(0.25f);
//            PlacePiece(i, pieceIndex);
//        }
//        PlacePiece(trackLength, 0);
//        PlacePiece(trackLength+1, 0);
//        PlacePiece(trackLength+2, 7);
//        //return;
//    }
//}

