using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumeSettings : MonoBehaviour
{
    [SerializeField] public AudioMixer _audioMixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private Slider _slider2;

    private void Start()
    {
        setMusicVolume();
        setSfxVolume();
    }

    public void setMusicVolume()
    {
        float volume = _slider.value;
        _audioMixer.SetFloat("music", Mathf.Log10(volume) * 10);
    }
    
    public void setSfxVolume()
    {
        float sfx = _slider2.value;
        _audioMixer.SetFloat("sfx", Mathf.Log10(sfx) * 10);
    }
}
