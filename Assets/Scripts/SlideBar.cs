using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxSlide(int currentSliding)
    {
        slider.maxValue = currentSliding;
        slider.value = currentSliding;
    }

    public void SetSlide(int currentSliding)
    {
        slider.value = currentSliding;
    }
}
