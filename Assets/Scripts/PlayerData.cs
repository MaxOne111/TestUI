using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    private static int _Player_Tickets = PlayerPrefs.GetInt("tickets", 0);
    private static int _Completed_Level = 0;
    
    public static int PlayerTickets{get=>_Player_Tickets;}
    public static int CompletedLevel{get=>_Completed_Level;}


    public static void CompleteLevel()
    {
        _Completed_Level++;
    }
    public static void RecieveTickets(int _value)
    {
        if (_value > 0)
        {
            _Player_Tickets += _value;
            PlayerPrefs.SetInt("tickets", _Player_Tickets);
            GameEvents.RecieveTickets();
        }
        
    }

    public static bool SpendTickets(int _value)
    {
        if (_Player_Tickets >= _value)
        {
            _Player_Tickets -= _value;
            PlayerPrefs.SetInt("tickets", _Player_Tickets);
            GameEvents.SpendTickets();

            return true;
        }
        else
        {
            return false;
        }
    }
}
