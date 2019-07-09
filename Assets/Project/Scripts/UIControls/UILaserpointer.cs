﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR;

public class UILaserpointer : MonoBehaviour
{
    [SerializeField] float laserLength;
    [SerializeField] LayerMask laserHitMask;
    [SerializeField] SteamVR_Input_Sources pointerObject;
    [SerializeField] SteamVR_Action_Boolean ClickAction;

    private bool active = false;

    private LineRenderer laser;
    private Button selectedButton;


    void Awake()
    {
        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
    }

    void Update()
    {
        if (active)
        {
            laser.SetPosition(0, transform.position);

            Vector3 laserTarget;

            RaycastHit laserHit;

            if (Physics.Raycast(new Ray(transform.position, transform.forward), out laserHit, laserLength, laserHitMask))
            {
                laserTarget = laserHit.point;

                Button button = laserHit.collider.GetComponent<Button>();

                if (button != null)
                {
                    button.Select();
                    selectedButton = button;
                }
                else if (selectedButton != null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    selectedButton = null;
                }

            }
            else
            {
                laserTarget = transform.position + (transform.forward * laserLength);

                if (selectedButton != null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    selectedButton = null;
                }
            }

            laser.SetPosition(1, laserTarget);


            if (ClickAction.GetState(pointerObject) && selectedButton != null)
            {
                selectedButton.onClick.Invoke();
            }
        }
    }


    public void ToggleLaser(bool isActive)
    {
        active = isActive;
        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, transform.position + (transform.forward * laserLength));
        laser.enabled = isActive;
    }
}