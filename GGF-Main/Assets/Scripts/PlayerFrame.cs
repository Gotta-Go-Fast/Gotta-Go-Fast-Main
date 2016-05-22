using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerFrame : MonoBehaviour {

    public PlayerFrame frame;

    // Pickups
    public GameObject jump;
    public GameObject speed;
    public GameObject ammo;
    public GameObject bomb;
    public GameObject blink;

    private Image jumpTexture;
    private Image speedTexture;
    private Image ammoTexture;
    private Image bombTexture;
    private Image blinkTexture;


    // Characters
    public GameObject knifeguy;
    public GameObject varulv;
    public GameObject vampire;
    public GameObject vålnad;
    public GameObject asuka;
    public GameObject blastoise;

    private Image knifeguyTexture;
    private Image varulvTexture;
    private Image vampireTexture;
    private Image vålnadTexture;
    private Image asukaTexture;
    private Image blastoiseTexture;

    private void Awake()
    {
        frame = gameObject.GetComponent<PlayerFrame>();

        jumpTexture = jump.GetComponent<Image>();
        speedTexture = speed.GetComponent<Image>();
        ammoTexture = ammo.GetComponent<Image>();
        bombTexture = bomb.GetComponent<Image>();
        blinkTexture = blink.GetComponent<Image>();

        knifeguyTexture = knifeguy.GetComponent<Image>();
        varulvTexture = varulv.GetComponent<Image>();
        vampireTexture = vampire.GetComponent<Image>();
        vålnadTexture = vålnad.GetComponent<Image>();
        asukaTexture = asuka.GetComponent<Image>();
        blastoiseTexture = blastoise.GetComponent<Image>();
    }

    // Player Portrait
    public void GetPortrait(int index)
    {
        if (index == 0)
        {
            varulvTexture.enabled = true;
        }
        if (index == 1)
        {
            knifeguyTexture.enabled = true;
        }
        if (index == 2)
        {
            vampireTexture.enabled = true;
        }
        if (index == 3)
        {
            vålnadTexture.enabled = true;
        }
        if (index == 4)
        {
            asukaTexture.enabled = true;
        }
        if (index == 5)
        {
            blastoiseTexture.enabled = true;
        }
        if (index == 6)
        {
            varulvTexture.enabled = true;
            varulvTexture.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (index == 7)
        {
            knifeguyTexture.enabled = true;
            knifeguyTexture.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (index == 8)
        {
            vampireTexture.enabled = true;
            vampireTexture.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (index == 9)
        {
            vålnadTexture.enabled = true;
            vålnadTexture.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (index == 10)
        {
            asukaTexture.enabled = true;
            asukaTexture.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (index == 11)
        {
            blastoiseTexture.enabled = true;
            blastoiseTexture.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Pickup Toggle Visibility
    public void Doublejump()
    {
        jumpTexture.enabled = true;
    }
    public void Speedboost()
    {
        speedTexture.enabled = true;
    }
    public void Ammo()
    {
        ammoTexture.enabled = true;
    }
    public void Bomb()
    {
        bombTexture.enabled = true;
    }
    public void Blink()
    {
        blinkTexture.enabled = true;
    }

    public void RemoveDoublejump()
    {
        jumpTexture.enabled = false;
    }
    public void RemoveSpeedboost()
    {
        speedTexture.enabled = false;
    }
    public void RemoveAmmo()
    {
        ammoTexture.enabled = false;
    }
    public void RemoveBomb()
    {
        bombTexture.enabled = false;
    }
    public void RemoveBlink()
    {
        blinkTexture.enabled = false;
    }

    // Called on reset
    public void Clear()
    {
        jumpTexture.enabled = false;
        speedTexture.enabled = false;
        ammoTexture.enabled = false;
        bombTexture.enabled = false;
        blinkTexture.enabled = false;
    }
}
