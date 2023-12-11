using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
     private int _Current_Day;

    [SerializeField] private List<Reward> _Rewards;

    [SerializeField] private GameUI _UI;

    [SerializeField] private Image _Progress_Bar;

    private void Start()
    {

        LoadYesterday();
        
        DisableButtons();
        
        CurrentDay();
        
        Debug.Log(_Current_Day);
    }
    
    private void SaveToday()
    {
        PlayerPrefs.SetInt("day", DateTime.Today.Day);
    }

    private void LoadYesterday()
    {
         _Current_Day = PlayerPrefs.GetInt("currentDay", 1);
    }

    private void DisableButtons()
    {
        if (_Current_Day > 1 && _Current_Day < 7)
        {
            for (int i = 0; i < _Rewards.Count - (7 - _Current_Day); i++)
            {
                _Rewards[i].Button.interactable = false;
            }
        }
        else
        {
            _Rewards[0].Button.interactable = false;
        }
        
    }
    
    private void CurrentDay()
    {
        int _today = DateTime.Today.Day;
        int _day = PlayerPrefs.GetInt("day",1);

        if (_today - _day == 1)
        {
            if (_Current_Day < 7)
            {
                _Rewards[_Current_Day-1].RecieveReward();
                _Current_Day++;
            }
            else if(_Current_Day == 7)
            {
                _Current_Day++;
                DaySevenReward();
            }
            else
            {
                StartReward();
            }
        }
        else if(_today == _day)
        {
            _Progress_Bar.fillAmount += (1f / 8) * _Current_Day;
            return;
        }
        else
        {
            StartReward();
        }
        
        PlayerPrefs.SetInt("currentDay", _Current_Day);

        _Progress_Bar.fillAmount += (1f / 8) * _Current_Day;
        
        SaveToday();
    }

    private void StartReward()
    {
        _Current_Day = 1;
        _Rewards[0].RecieveReward();
    }

    private void DaySevenReward()
    {
        PlayerData.RecieveTickets(5);
        _UI.DaySevenPanelEnable();
    }
}
