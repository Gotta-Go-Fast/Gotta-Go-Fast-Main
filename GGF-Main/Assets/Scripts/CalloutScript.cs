﻿using UnityEngine;
using System.Collections;

public class CalloutScript : MonoBehaviour {

    private Player target;

    // Voice
    private AudioSource rabbitVoice;
    private AudioSource knifeGuyVoice;
    private AudioSource vampireVoice;
    private AudioSource lucasVoice;
    private AudioSource pelleVoice;
    private AudioSource blastoiseVoice;

    // SoundEffects
    private AudioSource waowAS;
    private AudioSource andHeWillAS;
    private AudioSource hesGottaUseItSoonAS;

    private AudioSource go;
    private AudioSource applause;
    private AudioSource hahaha;
    private AudioSource naej;
    private AudioSource bleigh;

    // Pickups
    private AudioSource pickup;
    private AudioSource shoot;
    private AudioSource placeBomb;
    private AudioSource speedBoost;
    private AudioSource doubleJump;
    private AudioSource blink;

    public float useTimer;
    public float hitTimer;

    public bool hesGottaUseItSoon;
    public bool andHeWill;
    public bool waow;

    public bool firstShot;
    public bool secondShot;

    private bool mute;

    private void Awake()
    {
        useTimer = 3f;
        hitTimer = 3f;

        // Find voices
        rabbitVoice = GameObject.Find("RabbitVoice").GetComponent<AudioSource>();
        knifeGuyVoice = GameObject.Find("KnifeGuyVoice").GetComponent<AudioSource>();
        vampireVoice = GameObject.Find("VampireVoice").GetComponent<AudioSource>();
        lucasVoice = GameObject.Find("LucasVoice").GetComponent<AudioSource>();
        pelleVoice = GameObject.Find("AsukaVoice").GetComponent<AudioSource>();
        blastoiseVoice = GameObject.Find("BlastoiseVoice").GetComponent<AudioSource>();

        // Find callouts
        waowAS = GameObject.Find("CalloutWaow").GetComponent<AudioSource>();
        andHeWillAS = GameObject.Find("CalloutAndHeWill").GetComponent<AudioSource>();
        hesGottaUseItSoonAS = GameObject.Find("CalloutHesGotta").GetComponent<AudioSource>();
        go = GameObject.Find("CalloutGo").GetComponent<AudioSource>();
        applause = GameObject.Find("CalloutApplause").GetComponent<AudioSource>();
        hahaha = GameObject.Find("CalloutHahaha").GetComponent<AudioSource>();
        naej = GameObject.Find("CalloutNaej").GetComponent<AudioSource>();
        bleigh = GameObject.Find("CalloutBleigh").GetComponent<AudioSource>();

        // Find Pickups
        pickup = GameObject.Find("Pickup").GetComponent<AudioSource>();
        shoot = GameObject.Find("Shoot").GetComponent<AudioSource>();
        placeBomb = GameObject.Find("PlaceBomb").GetComponent<AudioSource>();
        speedBoost = GameObject.Find("Speedboost").GetComponent<AudioSource>();
        doubleJump = GameObject.Find("Doublejump").GetComponent<AudioSource>();
        blink = GameObject.Find("Blink").GetComponent<AudioSource>();

        DontDestroy();
    }
    private void Start ()
    {

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
        DontDestroyOnLoad(naej);
        DontDestroyOnLoad(bleigh);

        DontDestroyOnLoad(pickup);
        DontDestroyOnLoad(shoot);
        DontDestroyOnLoad(placeBomb);
        DontDestroyOnLoad(speedBoost);
        DontDestroyOnLoad(doubleJump);
        DontDestroyOnLoad(blink);
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
                //andHeWillAS.Play();
            }
            Debug.Log("hallou");
            andHeWill = true;
            secondShot = false;
        }


        // Checking if first shot hit
        if (firstShot && !secondShot && target.paralyzed)
        {
            hesGottaUseItSoon = true;
            firstShot = false;

            if (!mute)
            {
                hesGottaUseItSoonAS.Play();
            }
        }

        // After first shot, fire second shot in time
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
    public void PlayerFirstShot(Player otherPlayer)
    {
        this.target = otherPlayer;

        hitTimer = 3f;
        useTimer = 3f;

        hesGottaUseItSoon = false;
        andHeWill = false;
        waow = false;
        secondShot = false;

        firstShot = true;
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
            applause.Play();
    }
    public void Losing()
    {
        if (!mute)
            naej.Play();
    }
    public void ParalyzedLoss()
    {
        if (!mute)
            hahaha.Play();
    }
    public void PlayerHit()
    {
        if (!mute)
            bleigh.Play();
    }

    // Character Select
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
    public void VampireVoice()
    {
        if (!mute)
            vampireVoice.Play();
    }
    public void LucasVoice()
    {
        if (!mute)
            lucasVoice.Play();
    }
    public void PelleVoice()
    {
        if (!mute)
            pelleVoice.Play();
    }
    public void BlastoiseVoice()
    {
        if (!mute)
            blastoiseVoice.Play();
    }

    // Called upon when pressing restart
    public void Restart()
    {

    }

    // Pickup Effects
    public void Pickup()
    {
        if (!mute)
            pickup.Play();
    }
    public void Shoot()
    {
        if (!mute)
            shoot.Play();
    }
    public void PlaceBomb()
    {
        if (!mute)
            placeBomb.Play();
    }
    public void Speedboost()
    {
        if (!mute)
            pickup.Play();
    }
    public void Doublejump()
    {
        if (!mute)
            doubleJump.Play();
    }
    public void Blink()
    {
        if (!mute)
            pickup.Play();
    }
}
