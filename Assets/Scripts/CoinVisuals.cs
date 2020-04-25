using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinVisuals : MonoBehaviour
{

    [SerializeField] float spinAngle = 270;
    [SerializeField] Material baseMat;
    [SerializeField] Material flashMat;
    Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponentInChildren<Renderer>();
        StartCoroutine("Flash");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, spinAngle * Time.deltaTime, 0f);
    }

    IEnumerator Flash()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.8f);
            renderer.material = flashMat;
            yield return new WaitForSeconds(0.2f);
            renderer.material = baseMat;
        }
    }
}