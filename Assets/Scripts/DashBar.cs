using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxDash(int currentDashing)
    {
        slider.maxValue = currentDashing;
        slider.value = currentDashing;
    }

    public void SetDash(int currentDashing)
    {
        slider.value = currentDashing;
    }
}
