using UnityEngine;
using System.Collections;

public class CalloutScript : MonoBehaviour {

    private Player target;


    // SoundEffects
    private AudioSource rabbitVoice;
    private AudioSource knifeGuyVoice;
    private AudioSource waowAS;
    private AudioSource andHeWillAS;
    private AudioSource hesGottaUseItSoonAS;
    private AudioSource go;
    private AudioSource applause;
    private AudioSource hahaha;

    public float useTimer;
    public float hitTimer;

    public bool hesGottaUseItSoon;
    public bool andHeWill;
    public bool waow;

    public bool secondShot;

    private bool mute;

    private void Awake()
    {
        useTimer = 3f;
        hitTimer = 3f;

        rabbitVoice = GameObject.Find("RabbitVoice").GetComponent<AudioSource>();
        knifeGuyVoice = GameObject.Find("KnifeGuyVoice").GetComponent<AudioSource>();

        waowAS = GameObject.Find("CalloutWaow").GetComponent<AudioSource>();
        andHeWillAS = GameObject.Find("CalloutAndHeWill").GetComponent<AudioSource>();
        hesGottaUseItSoonAS = GameObject.Find("CalloutHesGotta").GetComponent<AudioSource>();
        go = GameObject.Find("CalloutGo").GetComponent<AudioSource>();
        applause = GameObject.Find("CalloutApplause").GetComponent<AudioSource>();
        hahaha = GameObject.Find("CalloutHahaha").GetComponent<AudioSource>();

        DontDestroy();

        applause.Play();
    }
    private void Start ()
    {
        applause.Pause();
    }

    private void DontDestroy()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(rabbitVoice);
        DontDestroyOnLoad(knifeGuyVoice);
        DontDestroyOnLoad(hesGottaUseItSoonAS);
        DontDestroyOnLoad(andHeWillAS);
        DontDestroyOnLoad(waowAS);
        DontDestroyOnLoad(go);
        DontDestroyOnLoad(applause);
        DontDestroyOnLoad(hahaha);
    }
    // Update is called once per frame
    private void Update ()
    {
        MuteCheck();
        ShootSequence();
    }
    // MuteCheck
    private void MuteCheck()
    {
        mute = MenuScript.mute;
    }

    // Shoot sequence
    private void ShootSequence()
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
                if (!mute)
                {
                    waowAS.Play();
                }

                waow = true;
            }
        }

        // Second shot
        if (hesGottaUseItSoon && secondShot)
        {
            if (!mute)
            {
                andHeWillAS.Play();
            }

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
        if (!mute)
        {
            hesGottaUseItSoonAS.Play();
        }

        hesGottaUseItSoon = true;
        waow = false;
    }
    public void PlayerSecondShot(Player otherPlayer)
    {
        this.target = otherPlayer;
        secondShot = true;
    }

    // SoundEffects
    public void Go()
    {
        if (!mute)
            go.Play();
    }
    public void Applause()
    {
        if (!mute)
            applause.UnPause();
    }
    public void ParalyzedLoss()
    {
        if (!mute)
            hahaha.Play();
    }
    public void RabbitVoice()
    {
        if (!mute)
            rabbitVoice.Play();
    }
    public void KnifeGuyVoice()
    {
        if (!mute)
            knifeGuyVoice.Play();
    }


    // Called upon when pressing restart
    public void Restart()
    {
        applause.Play();
        applause.Pause();
    }

}
