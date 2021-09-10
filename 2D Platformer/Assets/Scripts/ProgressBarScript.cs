using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        //slider = GetComponent<Slider>();
    }

    public void setSlider(int curr)
    {
        slider.value = curr;
        //FindObjectOfType<AudioManager>().Play("PowerUp");
    }
}
