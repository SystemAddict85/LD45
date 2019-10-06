using UnityEngine;
using System.Collections;
using System;

public class Snowball : SimpleSingleton<Snowball>
{

    SnowballController controller;

    public Action<Obstacle> gotHit;
    [SerializeField]
    private float timeToWaitAfterHit = 2f;
        
    private void Awake()
    {
        controller = GetComponent<SnowballController>();
        gotHit += GotHit;
    }

    private void GotHit(Obstacle obs)
    {
        print($"hit {obs.name}");
        // if negative snowchange, stop ball and lose snow
        if (obs.snowChangePercentage < 0f)
        {
            controller.ToggleMovement(false);
            StartCoroutine(BlinkSnowball(obs.snowChangePercentage));
        }
        else
        {
            controller.ChangeSnow(obs.snowChangePercentage);
        }
        
    }

    private IEnumerator BlinkSnowball(float damageTaken)
    {
        var rend = GetComponent<Renderer>();

        for (int i = 0; i < Mathf.RoundToInt(timeToWaitAfterHit / .2f); ++i)
        {
            yield return new WaitForSeconds(.1f);
            rend.enabled = false;
            yield return new WaitForSeconds(.1f);
            rend.enabled = true;
            controller.ChangeSnow(damageTaken / Mathf.RoundToInt(timeToWaitAfterHit / .2f));
        }

        controller.ToggleMovement(true);
    }

    
}
