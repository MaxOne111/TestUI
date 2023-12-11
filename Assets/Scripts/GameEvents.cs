using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action _Spend_Tickets;
    public static Action _Recieve_Tickets;

    public static void SpendTickets()
    {
        _Spend_Tickets?.Invoke();
    }

    public static void RecieveTickets()
    {
        _Recieve_Tickets?.Invoke();
    }
}
