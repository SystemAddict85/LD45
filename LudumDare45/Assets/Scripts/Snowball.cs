using System;
using System.Collections;
using UnityEngine;

public class Snowball : SimpleSingleton<Snowball>
{
    [HideInInspector]
    public SnowballController controller;

    public Action<Obstacle> gotHit;
    [SerializeField]
    private float timeToWaitAfterHit = 2f;

    private static int Score = 0;
    public static int Health = 3;

    public const int SCORE_REDUCTION_MULTIPLIER_FROM_HIT = 100;
    public const int SCORE_MULTIPLIER_FROM_BENEFIT = 1000;
    public const int SCORE_MULTIPLIER_FROM_MOVEMENT = 10;
    public const int SCORE_FROM_SKIER = 10000;

    private void Awake()
    {
        controller = GetComponent<SnowballController>();
        gotHit += GotHit;
        UpdateScore(0);
        Score = 0;
        Health = 3;
    }

    private void Start()
    {
        UIManager.Instance.UpdateHealth(Health);
    }

    private void GotHit(Obstacle obs)
    {
        print($"hit {obs.name}");
        int scoreChange = 0;
        bool takeDamage = false;
        if (obs.gameObject.layer == LayerMask.NameToLayer("Skier"))
        {
            takeDamage = controller.MaxSizePercentage < .5f ? true : false;
            scoreChange = controller.MaxSizePercentage < .5f ? -SCORE_FROM_SKIER / 10 : SCORE_FROM_SKIER;
        }
        // if negative snowchange, stop ball and lose snow
        else if (obs.snowChangePercentage < 0f)
        {
            controller.ToggleMovement(false);
            takeDamage = true;
            scoreChange = (int)(SCORE_REDUCTION_MULTIPLIER_FROM_HIT * obs.snowChangePercentage);
        }
        else
        {
            controller.ChangeSnow(obs.snowChangePercentage);
            scoreChange = (int)(obs.snowChangePercentage * SCORE_MULTIPLIER_FROM_BENEFIT);
            if (obs.gameObject.tag == "Snowman")
            {
                ++Health;
                if (Health > 3)
                {
                    Health = 3;
                }
                UIManager.Instance.UpdateHealth(Health);
            }
            AudioManager.PlaySound("pickup");
        }
        if (takeDamage)
        {
            --Health;
            UIManager.Instance.UpdateHealth(Health);
            if (Health <= 0)
            {
                GameOver();
            }
            else
            {
                AudioManager.PlaySound("hit");
                StartCoroutine(BlinkSnowball(obs.snowChangePercentage));
            }
        }
        UpdateScore(scoreChange);
    }

    public void UpdateScore(int scoreChange)
    {
        Score += scoreChange;
        if (Score < 0)
        {
            Score = 0;
        }
        UIManager.Instance.UpdateScore(Score);
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

    private void GameOver()
    {
        print("GAME OVER");
        AudioManager.PlaySound("gameOver");
        UIManager.Instance.ShowGameOverPanel();
    }


}
