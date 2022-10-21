using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public float distance;

    public GameObject startingCanvas, inGameCanvas, failedCanvas;

    public Text highScoreText, currentDistanceText, finalDistanceText;

    public GameObject startingBalls;
    GameObject startingBallObject;

    public GameObject removeAdsButton, crownImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startingBallObject = Instantiate(startingBalls);
    }

    // Update is called once per frame
    void Update()
    {
        highScoreText.text = "Score: " + String.Format("{0:0}", PlayerPrefs.GetFloat("HighScore")) + "m";
        MenuSelection();
        currentDistanceText.text = String.Format("{0:0}", distance) + "m";
        finalDistanceText.text = String.Format("{0:0}", distance) + "m";

        if (PlayerPrefs.GetInt("Premium") == 0)
        {
            removeAdsButton.SetActive(true);
            crownImage.SetActive(false);
        }
        else
        {
            removeAdsButton.SetActive(false);
            crownImage.SetActive(true);
        }
    }

    void MenuSelection()
    {
        if (!GameManager.instance.gameStarted && !GameManager.instance.failed)
        {
            startingCanvas.SetActive(true);
            inGameCanvas.SetActive(false);
            failedCanvas.SetActive(false);
        }
        else if (GameManager.instance.gameStarted && !GameManager.instance.failed)
        {
            startingCanvas.SetActive(false);
            inGameCanvas.SetActive(true);
            failedCanvas.SetActive(false);

            if (startingBallObject != null)
            {
                Destroy(startingBallObject, 0f);
            }

        }
        else if (GameManager.instance.gameStarted && GameManager.instance.failed)
        {
            GameManager.instance.soundtrack.Stop();
            startingCanvas.SetActive(false);
            inGameCanvas.SetActive(false);
            failedCanvas.SetActive(true);

            if (GameManager.instance.instantiatedBackgrounds.Count != 0)
            {
                foreach (GameObject item in GameManager.instance.instantiatedBackgrounds)
                {
                    Destroy(item, 0f);
                }
            }

            if (GameManager.instance.instantiatedRandomObjects.Count != 0)
            {
                foreach (GameObject item in GameManager.instance.instantiatedRandomObjects)
                {
                    Destroy(item, 0f);
                }
            }



            if (distance > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", distance);
            }
        }
    }

    public void PlayButtonClick()
    {
        GameManager.instance.StartingGame();
        GameManager.instance.gameStarted = true;
    }

    public void TryAgainClick()
    {
        PlayerPrefs.SetInt("GameCount", PlayerPrefs.GetInt("GameCount") + 1);

        if (PlayerPrefs.GetInt("GameCount") == 2 && PlayerPrefs.GetInt("Premium") != 1)
        {
            PlayerPrefs.SetInt("GameCount", 0);
            AdManager.instance.ShowInterstitialAd();
            
        }

        GameManager.instance.gameStarted = false;
        GameManager.instance.failed = false;
        startingBallObject = Instantiate(startingBalls);
    }

    public void SelectBlueClick()
    {
        PlayerPrefs.SetInt("SelectedBall", 0);
    }

    public void SelectGreenClick()
    {
        PlayerPrefs.SetInt("SelectedBall", 1);
    }

    public void SelectOrangeClick()
    {
        PlayerPrefs.SetInt("SelectedBall", 2);
    }

    public void SelectPinkClick()
    {
        PlayerPrefs.SetInt("SelectedBall", 3);
    }

    public void SelectPurpleClick()
    {
        PlayerPrefs.SetInt("SelectedBall", 4);
    }

    public void BoseGamesClick()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Bose+Games");
    }
}
