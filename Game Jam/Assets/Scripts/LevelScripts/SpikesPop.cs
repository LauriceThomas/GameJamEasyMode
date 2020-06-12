using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesPop : MonoBehaviour
{
    public Vector3 newPosition;
    public GameObject trapToMove;
    bool isActivated = false;
    public float spikesPopSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isActivated && trapToMove.transform.localPosition != newPosition)
        {
            trapToMove.transform.localPosition = Vector3.Lerp(trapToMove.transform.localPosition, newPosition, spikesPopSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("*Play Spikes sound*");
            isActivated = true;
        }
    }
}
