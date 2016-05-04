using UnityEngine;
using System.Collections;

public class CalloutScript : MonoBehaviour {

    private Player target;


    // SoundEffects
    private AudioSource waowAS;
    private AudioSource andHeWillAS;
    private AudioSource hesGottaUseItSoonAS;

    private float useTimer;
    private float hitTimer;

    private bool hesGottaUseItSoon;
    private bool andHeWill;
    private bool waow;

    private bool secondShot;


    private void Awake()
    {
        useTimer = 2f;
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
        if (hesGottaUseItSoon)
        {
            useTimer -= Time.deltaTime;

            if (useTimer <= 0)
            {
                hesGottaUseItSoonAS.Stop();
                hesGottaUseItSoon = false;

                useTimer = 3f;
            }
        }

        if (hesGottaUseItSoon && secondShot)
        {
            andHeWillAS.Play();
            andHeWill = true;
        }

        if (andHeWill)
        {
            hitTimer -= Time.deltaTime;

            if (hitTimer <= 0)
            {
                andHeWillAS.Stop();
                andHeWill = false;

                hitTimer = 3f;
            }

            if (target.paralyzed && hitTimer <= 2f)
            {
                waowAS.Play();
            }
        }
	}

    public void PlayerFirstShot()
    {
        hesGottaUseItSoonAS.Play();
        hesGottaUseItSoon = true;
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
