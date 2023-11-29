using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberScrollsCollected { get; private set; }

    public UnityEvent<PlayerInventory> OnScrollCollected;

    public void ScrollCollected()
    {
        NumberScrollsCollected++;
        OnScrollCollected.Invoke(this);
    }


}
