using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _Tickets_Value;
    [SerializeField] private GameObject _Day_Seven_Panel;

    private void Awake()
    {
        GameEvents._Recieve_Tickets += CurrentTickets;
        GameEvents._Spend_Tickets += CurrentTickets;
    }

    private void Start()
    {
        CurrentTickets();
    }

    private void CurrentTickets()
    {
        _Tickets_Value.text = PlayerData.PlayerTickets.ToString();
    }
    
    public void DaySevenPanelEnable()
    {
        float _duration = 1f;

        _Day_Seven_Panel.transform.localScale = Vector3.zero;

        _Day_Seven_Panel.SetActive(true);
        
        Sequence _sequence = DOTween.Sequence();

        _sequence
            .Append(_Day_Seven_Panel.transform.DOScale(Vector3.one, _duration).SetEase(Ease.OutElastic))
            .AppendInterval(2f)
            .AppendCallback(DaySevenPanelDisable);
    }
    
    private void DaySevenPanelDisable()
    {
        void DisablePanel()
        {
            _Day_Seven_Panel.SetActive(false);
        }
        
        float _duration = 1f;

        Sequence _sequence = DOTween.Sequence();

        _sequence
            .Append(_Day_Seven_Panel.transform.DOScale(Vector3.zero, _duration).SetEase(Ease.InElastic))
            .AppendCallback(DisablePanel);
    }
    
    public void SwitchUIElement(GameObject _element)
    {
        _element.SetActive(!_element.activeSelf);
    }

    private void OnDisable()
    {
        GameEvents._Recieve_Tickets -= CurrentTickets;
        GameEvents._Spend_Tickets -= CurrentTickets;
    }
}
