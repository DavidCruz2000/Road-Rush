using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
public class FuelBar : MonoBehaviour
{
public Slider slider;

    public void SetMaxFuel(int fuel)
    {
        slider.maxValue = fuel;
        slider.value = fuel;
    }

    public void SetFuel(int fuel)
    {
        slider.value = fuel;  // Ensure fuel value stays within valid range;
    }




}
