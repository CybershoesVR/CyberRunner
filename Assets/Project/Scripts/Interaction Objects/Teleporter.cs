﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    //[SerializeField]
    //Animator effectCanvas;
    public float fadeDuration = 2;

    private Transform player;


    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    public void Teleport()
    {
        //effectCanvas.SetTrigger("Fade");
        SteamVR_Fade.Start(Color.black, fadeDuration);
        Invoke("TpAfterFade", fadeDuration);
    }

    void TpAfterFade()
    {
        player.position = spawnPoint.position;

        SteamVR_Fade.Start(Color.clear, fadeDuration);

        //effectCanvas.SetTrigger("Fade");
    }
}
