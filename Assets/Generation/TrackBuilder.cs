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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
