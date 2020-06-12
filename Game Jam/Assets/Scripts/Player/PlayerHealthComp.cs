using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthComp : MonoBehaviour
{
    public Image healthbar;                             // Manipulate HP Bar in UI
    public static bool isInDeathMode = false;           // use 'PlayerHealthComp.isInDeathMode' to access this variable in other files 

    private SpriteRenderer spriteRenTorso;
    private SpriteRenderer spriteRenLeftArm;
    private SpriteRenderer spriteRenRightArm;
    private SpriteRenderer spriteRenLegs;
    private SpriteRenderer SpriteRenDeath;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenLegs = gameObject.transform.Find("Legs").GetComponent<SpriteRenderer>();
        spriteRenLeftArm = gameObject.transform.Find("L Arm").GetComponent<SpriteRenderer>();
        spriteRenRightArm = gameObject.transform.Find("R Arm").GetComponent<SpriteRenderer>();
        SpriteRenDeath = gameObject.transform.Find("Sprite Dead").GetComponent<SpriteRenderer>();
        spriteRenTorso = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Inflict self damage
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            KillPlayer();
        }

        // Update HP Bar in UI
        if (healthbar)
        {
            healthbar.fillAmount = !isInDeathMode ? 1 : 0;
            healthbar.color = !isInDeathMode ? Color.green : Color.red;
        }

        UpdateSprites();
    }

    public static void KillPlayer()
    {
        bool hasResKey = GrabberComp.keyInHand && GrabberComp.keyInHand.tag == "ResKey";

        if (isInDeathMode || hasResKey) { return; }

        isInDeathMode = true;
        Debug.Log("*Play Death Mode Sound*");
    }

    public static void Resurrect()
    {
        if(isInDeathMode)
        {
            Debug.Log("*Play Alive Mode Sound*");
            isInDeathMode = false;
        }
    }

    private void UpdateSprites()
    {
        spriteRenLeftArm.enabled = !isInDeathMode;
        spriteRenRightArm.enabled = !isInDeathMode;
        spriteRenLegs.enabled = !isInDeathMode;
        spriteRenTorso.enabled = !isInDeathMode;
        SpriteRenDeath.enabled = isInDeathMode;
    }
}
