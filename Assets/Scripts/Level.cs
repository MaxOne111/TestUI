using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private Level _Level;
    private Button _Button;

    private void Awake()
    {
        _Button = GetComponent<Button>();
    }

    private void LevelUnlock()
    {
        _Button.interactable = true;
        PlayerData.CompleteLevel();
    }
    
    public void NextLevel()
    {
        _Level.LevelUnlock();
    }
}
