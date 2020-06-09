using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamageComp : MonoBehaviour
{
    public float damageToApply = 100;

    private PlayerHealthComp playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Player Character").GetComponent<PlayerHealthComp>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == playerHealth.gameObject)
        {
            playerHealth.ReceiveDamage(damageToApply);
        }
    }
}
