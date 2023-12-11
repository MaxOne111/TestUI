using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingButton : MonoBehaviour
{
    [SerializeField] private int _Value;
    [SerializeField] private GameObject _Complete;

    public void Buy()
    {
        if (PlayerData.SpendTickets(_Value))
        {
            _Complete.SetActive(true);
        }

    }
    
    public void Buy(int _level)
    {
        if (PlayerData.SpendTickets(_Value) && PlayerData.CompletedLevel >= _level)
        {
            _Complete.SetActive(true);
        }

    }
}
