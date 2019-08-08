﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] int value = 1;

    private MeshRenderer meshRenderer;
    private SphereCollider coinCollider;
    private ParticleSystem collectParticles;
    private AudioSource source;


    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        coinCollider = GetComponent<SphereCollider>();
        collectParticles = GetComponentInChildren<ParticleSystem>();
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.transform.root.GetComponent<PlayerController>();

        if (player)
        {
            float pitchOffset = player.AddGems(value);
            coinCollider.enabled = false;
            meshRenderer.enabled = false;
            collectParticles.Play();
            source.pitch = 1 + (pitchOffset * 0.05f);
            source.Play();
        }
    }

    public void Respawn()
    {
        coinCollider.enabled = true;
        meshRenderer.enabled = true;
    }
}