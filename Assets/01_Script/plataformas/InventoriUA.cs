using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InventoriUA : MonoBehaviour
{
    private TextMeshProUGUI milText;
    public Text feli;
    void Start()
    {
        milText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void updatemilk(PlayerMilkIve playerInvetori)
    {
        if (int.Parse(playerInvetori.numberMilk.ToString())!= 3)
        {
            milText.text = playerInvetori.numberMilk.ToString() + "/3";
        }
        else
        {
            feli.gameObject.SetActive(true);
        }
         
    }
}
