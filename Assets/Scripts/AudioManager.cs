using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _Audio_Source;
    
    [SerializeField] private AudioClip _Button_Click;

    private float _Click_Volume = 100;

    private void Awake()
    {
        _Audio_Source = GetComponent<AudioSource>();
    }

    public void Click()
    {
        _Audio_Source.PlayOneShot(_Button_Click, _Click_Volume);
    }

    public void ClickDisable()
    {
        _Click_Volume = 0;
    }

    public void ClickEnable()
    {
        _Click_Volume = 100;
    }

    public void VolumeOn()
    {
        _Audio_Source.Play();
    }

    public void VolumeOff()
    {
        _Audio_Source.Stop();
    }
}
