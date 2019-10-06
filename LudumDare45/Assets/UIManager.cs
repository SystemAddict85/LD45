using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SimpleSingleton<UIManager>
{
    [SerializeField]
    private Image snowBar;

    public void UpdateSnowbar(float percentage)
    {
        snowBar.fillAmount = percentage;
        if (percentage == 1f)
        {
            snowBar.color = Color.cyan;
        }else if(percentage <= .05f)
        {
            snowBar.color = Color.red;
        }
        else
        {
            snowBar.color = Color.white;
        }
    }
    
    
}
