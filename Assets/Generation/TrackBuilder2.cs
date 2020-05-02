using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


[System.Serializable]
struct piece
{
    public int index;
    public GameObject prefab;
    public int x, y, z;
    public int direction;
    public Vector3 posOffset;
    public Vector3 rotOffset;
};


public class TrackBuilder2 : MonoBehaviour
{
   
    [SerializeField] GameObject[] trackPieces;// = new GameObject[3];
    [SerializeField] GameObject[] trackGameObjects;
    [SerializeField] GameObject player;
    [SerializeField] piece[] track;
    [SerializeField] int trackLength;
    [SerializeField] int sizeOfField;
    [SerializeField] int straightness;
    [SerializeField] float stepXZ = 6.0f;
    [SerializeField] float stepY  = 1.2f;
    [SerializeField] int randomSeed = 1;
    [SerializeField] float renderSpeed = 2;
    
    bool[,,] containsPiece;
    int currDir = 0;
    [SerializeField]int mid;
    Vector3 currPos;
    Vector3 currRot;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.seed = randomSeed;
        mid = sizeOfField / 2;
        if (trackLength > 3 * sizeOfField)
            trackLength = 3 * sizeOfField;
        if (straightness > sizeOfField / 10)
            straightness = sizeOfField / 10;
        containsPiece = new bool[sizeOfField, sizeOfField, sizeOfField];
        track = new piece[trackLength+3];
        trackGameObjects = new GameObject[trackLength + 3];
        StartCoroutine("BuildTrack");
        player = Instantiate(player);
        player.transform.position = new Vector3(0,3,0);
    }

    Vector3 GetPosOffset(int pieceIndex)
    {
        Vector3 offset = new Vector3(0,0,0);
        //set XZ
        if      (currDir == 0) offset.x += stepXZ;
        else if (currDir == 1) offset.z -= stepXZ;
        else if (currDir == 2) offset.x -= stepXZ;
        else if (currDir == 3) offset.z += stepXZ;
        //set Y
        if (pieceIndex == 4)
        {

            offset.y += stepY;
        }
        else if (pieceIndex == 5)
        {
            offset.y -= stepY;
            //currPos.y -= stepY;
        }
        return offset;
    }

    Vector3 GetRotOffset(int pieceIndex)
    {
        Vector3 offset = new Vector3(0, 0, 0);

        if (pieceIndex == 1)
        {
            currDir = (++currDir) % 4;
            offset.y += 90.0f;
        }
        else if (pieceIndex == 2 || pieceIndex == 9)
        {
            currDir = (currDir+3) % 4;
            offset.y -= 90.0f;
        }
        return offset;
    }



    int GetPieceOverlaping(piece p) 
    {
        
		for (int i = 0; i < trackLength; i++)
		{
            if (track[i].x == p.x && track[i].y == p.y && track[i].z == p.z)
            {
                return i;
            }
		}
        return -1;//there is no overlap
    }

    bool NearXZEdge(int trackIndex)
    {
        bool ret = (track[trackIndex].x + mid < sizeOfField - 2 * stepXZ
				    && track[trackIndex].x + mid > 2 * stepXZ
				    && track[trackIndex].z + mid < sizeOfField - 2 * stepXZ
				    && track[trackIndex].z + mid > 2 * stepXZ);
		return !ret;
	}

    bool PieceAbove(piece p)
    {
        for (int i = 0; i < trackLength; i++)
        {
            if (track[i].x == p.x && track[i].y == p.y + 1 && track[i].z == p.z)
            {
                return true;
            }
        }
        return false;
    }

    bool PieceBelow(piece p)
    {
        for (int i = 0; i < trackLength; i++)
        {
            if (track[i].x == p.x && track[i].y == p.y - 1 && track[i].z == p.z)
            {
                return true;
            }
        }
        return false;
    }

    void undoDataSet(int trackIndex, int pieceIndex)
    {
        if (pieceIndex == 1)
            GetRotOffset(2);
        else if (pieceIndex == 2)
            GetRotOffset(1);
        //else if(pieceIndex == 5)
        //    track[trackIndex].y += 1;
        else
            return;
    }

    bool SetTrackData(int trackIndex, int pieceIndex)
    {
        //bool ret = true;
        track[trackIndex].index = pieceIndex;
        track[trackIndex].prefab = trackPieces[pieceIndex];
        track[trackIndex].x = Convert.ToInt32(currPos.x / stepXZ);
        track[trackIndex].y = Convert.ToInt32(currPos.y / stepY );
        track[trackIndex].z = Convert.ToInt32(currPos.z / stepXZ);
        track[trackIndex].rotOffset = GetRotOffset(pieceIndex);
        track[trackIndex].posOffset = GetPosOffset(pieceIndex);
        
        track[trackIndex].direction = currDir;
       
        //UnityEngine.Debug.LogError(track[trackIndex].x + mid);
        //UnityEngine.Debug.LogError(track[trackIndex].y + mid);
        //UnityEngine.Debug.LogError(track[trackIndex].z + mid);
        if (!containsPiece[track[trackIndex].x + mid, track[trackIndex].y + mid, track[trackIndex].z + mid])
        {
                return ( (containsPiece[track[trackIndex].x + mid,
                       track[trackIndex].y + mid,
                       track[trackIndex].z + mid] = true));
        }
        else
        {
            return false;
        }
        //ret = ret && (track[trackIndex].x + mid < sizeOfField - 2 * stepXZ
        //          && track[trackIndex].x + mid > 2 * step
        //          && track[trackIndex].z + mid < sizeOfField - 2 * stepXZ
        //          && track[trackIndex].z + mid > 2 * step);
        //return ret;
    }

    void InstantiatePiece(piece p, int index)
    {
        //create piece
        if (p.index == 5)
        {
            p.y -= 1;
        }
        GameObject g;
        g = Instantiate(p.prefab);
        g.transform.position = new Vector3(p.x * stepXZ, p.y * stepY, p.z * stepXZ);
        g.transform.eulerAngles = currRot;
        trackGameObjects[index] = g;
        //updates for next piece
        currPos += p.posOffset;
        currRot += p.rotOffset;
    }

    IEnumerator BuildTrack()
    {
        //int mid = sizeOfField / 2;
        int pieceIndex = 0;
        bool canSetData = true;
        bool nearEdge = false;
        bool aboveOk = true;
        bool belowOk = true;
        bool oppositeDirections = false;
        currPos = new Vector3(0,0,0);
        currRot = new Vector3(-90, 0, 0);
        currDir = 0;
        SetTrackData(0, 7);
        InstantiatePiece(track[0],7);

        SetTrackData(1, 0);
        InstantiatePiece(track[1],1);
        int i = 2;
        for (i = 2; i < trackLength; i++)
        {
            yield return new WaitForSeconds(1.0f/renderSpeed);
            //UnityEngine.Debug.LogError(currDir);
            pieceIndex = UnityEngine.Random.Range(0, straightness+5);
            if (pieceIndex > 5) pieceIndex = 0;

            canSetData = SetTrackData(i, pieceIndex);
            nearEdge = NearXZEdge(i);
            belowOk = !PieceBelow(track[i]);
            aboveOk = !PieceAbove(track[i]);
            if (canSetData && !nearEdge && aboveOk && belowOk)
            {
                UnityEngine.Debug.LogError("all good");
                InstantiatePiece(track[i], i);
            }
            else if (!canSetData)//this one has to override near edge if it is near an edge
            {

                UnityEngine.Debug.LogError("bitch we sharing this shit.");
                undoDataSet(i,pieceIndex);
                int overlap = GetPieceOverlaping(track[i]);
                oppositeDirections = (track[i].direction % 2) != (track[overlap].direction % 2);
                //track[overlap] and track[i] have the same position;
                if (overlap == -1)
                {
                    UnityEngine.Debug.LogError("There is actually NOT an overlap...");
                }

                if (oppositeDirections && track[overlap].index == 0)//create crisscross
                {
                    //track[i] = new piece();
                    SetTrackData(i, 6);//this should act as a straight piece
                    InstantiatePiece(track[i], i);
                    Destroy(trackGameObjects[overlap]);
                }
                else if (oppositeDirections && (track[overlap].index == 3 || track[overlap].index == 4))
                {
                    SetTrackData(i, 8);//this should act as a straight piece
                    InstantiatePiece(track[i], i);
                }
                else if (oppositeDirections && track[overlap].index == 5)
                {
                    SetTrackData(i, 3);//this should act as a straight piece
                    InstantiatePiece(track[i], i);
                }
                else if (track[overlap].index == 1 || track[overlap].index == 2)
                {
                    SetTrackData(i, 9);//this should act as a double left turn
                    InstantiatePiece(track[i], i);
                    Destroy(trackGameObjects[overlap]);

                }

            }
            else if (!aboveOk && !belowOk)
            {
                UnityEngine.Debug.LogError("cant above or below so im going straight");
                undoDataSet(i,pieceIndex);
                pieceIndex = 0;
                SetTrackData(i, pieceIndex);
                InstantiatePiece(track[i], i);
            }
            else if (!aboveOk)
            {
                UnityEngine.Debug.LogError("cant go up so i going down");
                undoDataSet(i,pieceIndex);
                pieceIndex = 5;
                SetTrackData(i, pieceIndex);
                InstantiatePiece(track[i], i);
            }
            else if (!belowOk)
            {
                UnityEngine.Debug.LogError("cant go down so i going up");
                undoDataSet(i,pieceIndex);
                pieceIndex = 4;
                SetTrackData(i, pieceIndex);
                InstantiatePiece(track[i], i);
            }
            else if (nearEdge)
            {
                UnityEngine.Debug.LogError("too close to edge!! turning around");
                undoDataSet(i,pieceIndex);
                SetTrackData(i, 1);
                InstantiatePiece(track[i], i);
            }
            
            //instant

            
        }
        SetTrackData(++i, 7);
        //instant
        InstantiatePiece(track[i],i);
        //SetTrackData(++i, 0);
        ////instant
        //InstantiatePiece(track[i],i);
        //SetTrackData(++i, 7);//finish line
        ////instant
        //InstantiatePiece(track[i],i);
    }

}
