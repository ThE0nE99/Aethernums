using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMilkIve : MonoBehaviour
{
    public int numberMilk { get; private set; }
    public UnityEvent<PlayerMilkIve> OnMilkCollected;
    public void MilkColecte()
    {

        numberMilk++;
        OnMilkCollected.Invoke(this);
    }
}
