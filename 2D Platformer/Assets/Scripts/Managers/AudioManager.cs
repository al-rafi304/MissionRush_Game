using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public bool underWater;

    public SoundsHolder[] sounds;

    private void Awake() 
    {
        foreach (SoundsHolder s in sounds)
        {
            GameObject audioObject = new GameObject();
            audioObject.transform.SetParent(this.transform);
            audioObject.name = s.name;
            s.Source = audioObject.AddComponent<AudioSource>();
            s.Source.clip = s.clip;
            s.Source.volume = s.volume;
            s.Source.loop = s.loop;
            s.Source.outputAudioMixerGroup = s.mixer;


            if(s.underWaterEffect)
            {
                s.lowPass = audioObject.AddComponent<AudioLowPassFilter>();
                s.lowPass.cutoffFrequency = s.cutoffFrequency;
            }
            
        }    
    }

    private void Update() 
    {
        if(underWater)
        {
            PlayUnderWater(true);
        }    
        else
            PlayUnderWater(false);
    }

    public void PlayUnderWater(bool bypass) 
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].underWaterEffect == true)
            {
                sounds[i].Source.bypassEffects = !bypass;
            }
            else 
            {
                sounds[i].Source.bypassEffects = bypass;
            }


        }
    }

    public void Play(string name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(name == sounds[i].name)
            {
                sounds[i].Source.Play();
                break;
            }
            else
                continue;
        }
    }

    public void Mute()
    {
        foreach(var sound in sounds)
        {
            sound.Source.volume = 0;
        }
    }

    private void Start() 
    {
        Play("Background");    
    }
}
