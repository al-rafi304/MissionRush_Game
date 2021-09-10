using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundsHolder
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup mixer;
    
    [Range(0,1)]
    public float volume;
    public bool loop;
    public bool underWaterEffect;
    public float cutoffFrequency;


    
    [HideInInspector]
    public AudioLowPassFilter lowPass;
    [HideInInspector]
    public AudioSource Source;

    public enum FilterList
    {
        None,
        lowPassFilter,
        highPassFilter
    };

    public FilterList filter;
}
