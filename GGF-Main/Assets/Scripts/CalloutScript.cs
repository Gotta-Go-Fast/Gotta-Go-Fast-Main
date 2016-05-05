using UnityEngine;
using System.Collections;

public class CalloutScript : MonoBehaviour {

    private Player target;


    // SoundEffects
    private AudioSource waowAS;
    private AudioSource andHeWillAS;
    private AudioSource hesGottaUseItSoonAS;

    public float useTimer;
    public float hitTimer;

    public bool hesGottaUseItSoon;
    public bool andHeWill;
    public bool waow;

    public bool secondShot;


    private void Awake()
    {
        useTimer = 3f;
        hitTimer = 3f;

        waowAS = GameObject.Find("CalloutWaow").GetComponent<AudioSource>();
        andHeWillAS = GameObject.Find("CalloutAndHeWill").GetComponent<AudioSource>();
        hesGottaUseItSoonAS = GameObject.Find("CalloutHesGotta").GetComponent<AudioSource>();

        DontDestroy();
    }
    private void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

        // Checking if second shot hit
        if (andHeWill)
        {
            hitTimer -= Time.deltaTime;

            if (hitTimer <= 0 || waow)
            {
                andHeWillAS.Stop();
                andHeWill = false;

                hitTimer = 3f;
            }

            if (target.paralyzed)
            {
                waowAS.Play();
                waow = true;
            }
        }

        // Second shot
        if (hesGottaUseItSoon && secondShot)
        {
            andHeWillAS.Play();
            andHeWill = true;
            secondShot = false;
        }


        // First shot
        if (hesGottaUseItSoon)
        {
            useTimer -= Time.deltaTime;

            if (useTimer <= 0 || secondShot)
            {
                hesGottaUseItSoonAS.Stop();
                hesGottaUseItSoon = false;

                useTimer = 3f;
            }
        }
    }

    public void PlayerFirstShot()
    {
        hesGottaUseItSoonAS.Play();
        hesGottaUseItSoon = true;
        waow = false;
    }
    public void PlayerSecondShot(Player otherPlayer)
    {
        this.target = otherPlayer;
        secondShot = true;
    }
    private void DontDestroy()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(hesGottaUseItSoonAS);
        DontDestroyOnLoad(andHeWillAS);
        DontDestroyOnLoad(waowAS);
    }
}
