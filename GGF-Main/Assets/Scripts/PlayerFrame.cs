using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerFrame : MonoBehaviour {

    public PlayerFrame frame;

    public int portraitIndex;

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

    private List<Image> textures;

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
        portraitIndex = index;

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
    }

    // Pickup Toggle Visibility
    public void Doublejump()
    {
        jumpTexture.enabled = true;
        jump.GetComponent<Animation>().Play();
    }
    public void Speedboost()
    {
        speedTexture.enabled = true;
        speed.GetComponent<Animation>().Play();
    }
    public void Ammo()
    {
        ammoTexture.enabled = true;
        ammo.GetComponent<Animation>().Play();
    }
    public void Bomb()
    {
        bombTexture.enabled = true;
        bomb.GetComponent<Animation>().Play();
    }
    public void Blink()
    {
        blinkTexture.enabled = true;
        blink.GetComponent<Animation>().Play();
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
    public void ClearPickups()
    {
        portraitIndex = -1;

        jumpTexture.enabled = false;
        speedTexture.enabled = false;
        ammoTexture.enabled = false;
        bombTexture.enabled = false;
        blinkTexture.enabled = false;
    }

    public void ClearFrame()
    {
        knifeguyTexture.enabled = false;
        varulvTexture.enabled = false;
        vålnadTexture.enabled = false;
        vampireTexture.enabled = false;
        asukaTexture.enabled = false;
        blastoiseTexture.enabled = false;
    }
}
