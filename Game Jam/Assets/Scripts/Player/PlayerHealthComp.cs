using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthComp : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;
    public Image healthbar;
    public bool isInDeathMode = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ReceiveDamage(100);

        if (healthbar)
        {
            healthbar.fillAmount = health / maxHealth;
            healthbar.color = (health > 25 ? Color.green : Color.red);
        }
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            isInDeathMode = true;
        }
    }

    public void GainHealth(float healthAdded)
    {
        health += healthAdded;
    }
}
