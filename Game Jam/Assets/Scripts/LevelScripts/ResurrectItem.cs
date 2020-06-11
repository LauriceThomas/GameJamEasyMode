using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectItem : MonoBehaviour
{
    public bool disappearAfterTouch = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerHealthComp playerHealth = collision.gameObject.GetComponent<PlayerHealthComp>();
            playerHealth.Resurrect();

            if(disappearAfterTouch)
            {
                Destroy(gameObject);
            }
        }
    }
}
