using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesPop : MonoBehaviour
{
    public Vector3 newPosition;
    public GameObject trapToMove;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // TODO Will Move elsewhere
            if(trapToMove.transform.localPosition != newPosition)
            {
                Debug.Log("*Spikes Sound plays*");
            }

            trapToMove.transform.localPosition = newPosition;
        }
    }
}
