using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthComp : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;
    public Image healthbar;                             // Manipulate HP Bar in UI
    public static bool isInDeathMode = false;           // use 'PlayerHealthComp.isInDeathMode' to access this variable in other files 

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Inflict self damage
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ReceiveDamage(20);
        }

        // Update HP Bar in UI
        if (healthbar)
        {
            healthbar.fillAmount = health / maxHealth;
            healthbar.color = (health > (maxHealth / 4) ? Color.green : Color.red);
        }
    }

    public void ReceiveDamage(float damage)
    {
        if (health <= 0) { return; }

        // Prevent HP from going less than 0 and over max health
        float healthDelta = health - damage;
        health = Mathf.Clamp(healthDelta, 0, maxHealth);

        if(health <= 0)
        {
            isInDeathMode = true;
            Debug.Log("*Play Death Mode Sound*");
        }
    }

    public void GainHealth(float healthAdded)
    {
        // Prevent HP from going less than 0 and over max health
        float healthDelta = health + healthAdded;
        health = Mathf.Clamp(healthDelta, 0, maxHealth);

        isInDeathMode = false;
    }

    public void Resurrect()
    {
        if(isInDeathMode)
        {
            Debug.Log("*Play Alive Mode Sound*");
            health = maxHealth;
            isInDeathMode = false;
        }
    }
}
