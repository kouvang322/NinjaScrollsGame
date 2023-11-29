using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI scrollText = null;

    public PlayerInventory inventory;
    public ShrineBehavior shrine;

    public int TotalScrolls = 0;

    // Start is called before the first frame update
    void Start()
    {
        //scrollText = GetComponent<TextMeshProUGUI>();

        //StartCoroutine(WaitToUpdateText());

    }

    public void UpdateScrollText(PlayerInventory playerInventory)
    {
        scrollText.text = $"{playerInventory.NumberScrollsCollected} / {TotalScrolls}";
    }

    public void IncreaseTotalScrolls()
    {
        TotalScrolls++;

        // Setting initial scroll text
        scrollText = GetComponent<TextMeshProUGUI>();

        if (scrollText)
        {
            scrollText.text = $"0 / {TotalScrolls}";

        }
    }

    //IEnumerator WaitToUpdateText()
    //{
    //    yield return new WaitForSeconds(.05f);
    //    scrollText.text = $"{inventory.NumberScrollsCollected} / {shrine.TotalScrollCount}";

    //    Debug.Log(scrollText.text);
    //}


}
