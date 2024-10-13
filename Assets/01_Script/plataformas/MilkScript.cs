using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkScript : MonoBehaviour
{
    GameObject texto;
     void Start()
    {
        texto = GameObject.FindGameObjectWithTag("found");
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        PlayerMilkIve player = other.GetComponent<PlayerMilkIve>();
        if (player != null)
        {
            player.MilkColecte();
            texto.gameObject.SetActive(false);
            texto.gameObject.SetActive(true);
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
