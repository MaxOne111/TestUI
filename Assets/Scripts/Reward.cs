using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    [SerializeField] private int _Reward_Value;
    
    [SerializeField] private TextMeshProUGUI _Reward_Value_Text;
    
    [SerializeField] private Button _Button;
    
    public Button Button{get=>_Button;}

    private void Start()
    {
        _Reward_Value_Text.text = $"X{_Reward_Value}";
    }

    public void ResetReward()
    {
        _Reward_Value_Text.text = $"X{_Reward_Value}";
        _Button.interactable = true;
    }

    public void RecieveReward()
    {
        PlayerData.RecieveTickets(_Reward_Value);
        _Button.interactable = false;
    }
}
