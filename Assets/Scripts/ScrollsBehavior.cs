using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;



public class ScrollsBehavior : MonoBehaviour
{

    private TextMeshProUGUI scrollText;

    public UnityEvent<ScrollsBehavior> ScrollCreated;

    private void Start()
    {
        ScrollCreated.Invoke(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            playerInventory.ScrollCollected();
            gameObject.SetActive(false);

            Debug.Log("scroll collected");
        }
    }
}
