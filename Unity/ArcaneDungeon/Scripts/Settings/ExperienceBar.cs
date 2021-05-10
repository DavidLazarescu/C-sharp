using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Slider slider;
    public void setMaxExperience(int experience)
    {
        slider.maxValue = experience;
    }

    public void setExperience(int experience)
    {
        slider.value = experience;
    }
}
