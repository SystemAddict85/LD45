using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : SimpleSingleton<UIManager>
{
    [SerializeField]
    private Image snowBar;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private RectTransform healthStack;
    private Image[] healthImages;

    [SerializeField]
    private RectTransform gameOverPanel;

    [SerializeField]
    private RectTransform startPanel;



    private void Awake()
    {
        healthImages = healthStack.GetComponentsInChildren<Image>();
        gameOverPanel.gameObject.SetActive(false);
        gameOverPanel.GetComponentInChildren<Button>().onClick.AddListener(ReloadGame);
        startPanel.gameObject.SetActive(true);
        startPanel.GetComponentInChildren<Button>().onClick.AddListener(StartGame);
        UpdateSnowbar(0f);
    }

    private void StartGame()
    {
        startPanel.gameObject.SetActive(false);
        Snowball.Instance.controller.ToggleMovement(true);
        AudioManager.PlaySound("start");
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateSnowbar(float percentage)
    {
        snowBar.fillAmount = percentage;
        if (percentage == 1f)
        {
            snowBar.color = Color.cyan;
        }else if(percentage <= .5f)
        {
            snowBar.color = Color.red;
        }
        else
        {
            snowBar.color = Color.white;
        }
    }

    public void UpdateHealth(int health)
    {
        for(int i = 0; i < health; ++i)
        {
            healthImages[i].enabled = true;
        }
        for(int i = 2; i >= health; --i)
        {
            healthImages[i].enabled = false;
        }
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
    }
    
    private void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
