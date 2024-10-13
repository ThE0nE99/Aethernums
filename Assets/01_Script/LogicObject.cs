using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicObject : MonoBehaviour
{
    public int numObject;
    public TextMeshProUGUI testMision;
    public GameObject botonDemision;
    // Start is called before the first frame update
    void Start()
    {
        numObject = GameObject.FindGameObjectsWithTag("objetivo").Length;
        testMision.text = "Obten las cajas de leche" + 
                          "\n Restantes: " + numObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="objetivo")
        {
            Destroy(col.transform.parent.gameObject);
            numObject--;
            testMision.text= "Obten las cajas de leche" +
                            "\n Restantes: " + numObject;
            if (numObject <= 0)
            {
                testMision.text = "Completaste la misión";
                botonDemision.SetActive(true);
            }
        }
    }
}
