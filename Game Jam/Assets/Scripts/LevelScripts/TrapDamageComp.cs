using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamageComp : MonoBehaviour
{
    public float damageToApply = 100;

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

            if(playerHealth)
            {
                playerHealth.ReceiveDamage(damageToApply);
            }
        }
    }
}
