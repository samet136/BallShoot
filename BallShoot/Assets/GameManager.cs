using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("BALL SETTINGS")]
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private GameObject FirePoint;
    [SerializeField] private float PowerBall;
    int ActiveBallIndex;
    [SerializeField] private int AvailableBall;
    [SerializeField] private Animator ThrowsBall;
    [SerializeField] private ParticleSystem ThrowsBallEffect;
    [SerializeField] private ParticleSystem[] BallEffects;
    [SerializeField] private AudioSource[] BallSounds;
    int ActiveBallSoundIndex;



    [Header("LEVEL SETTINGS")]
    [SerializeField] private int TargetBall;
    int EnteringBall;

    [SerializeField] private Slider TargetSlider;
    [SerializeField] private TextMeshProUGUI RemainingBallText;
    [Header("UI SETTINGS")]
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI StarCount;
    [SerializeField] private TextMeshProUGUI WinLevelCount;
    [SerializeField] private TextMeshProUGUI LostLevelCount;
    int ActiveBallEffectIndex;

    [Header("OTHER SETTINGS")]
    [SerializeField] private AudioSource[] OtherSounds;
    string LevelName;

    void Start()
    {
        ActiveBallEffectIndex = 0;
        LevelName = SceneManager.GetActiveScene().name;
        TargetSlider.maxValue = TargetBall;
        RemainingBallText.text = AvailableBall.ToString();
    }
    public void BallEntered()
    {
        EnteringBall++;

        TargetSlider.value = EnteringBall;

        BallSounds[ActiveBallSoundIndex].Play();
        ActiveBallSoundIndex++;
        if (ActiveBallSoundIndex == BallSounds.Length - 1)
        {
            ActiveBallSoundIndex = 0;
        }
        if (EnteringBall == AvailableBall)

        {
            Time.timeScale = 0;
            OtherSounds[1].Play();
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Star", PlayerPrefs.GetInt("Star") + 15);
            StarCount.text = PlayerPrefs.GetInt("Star").ToString();
            WinLevelCount.text = "LEVEL: " + LevelName;
            Panels[1].SetActive(true);
        }
        if (AvailableBall == 0 && EnteringBall != TargetBall)
        {
            Lost();

        }
        if (AvailableBall + EnteringBall < TargetBall)
        {
            Lost();

        }
    }
    public void DidntEnter()
    {


        if (AvailableBall == 0)
        {
            Lost();

        }
        if (AvailableBall + EnteringBall < TargetBall)
        {
            OtherSounds[0].Play();
            LostLevelCount.text = "LEVEL: " + LevelName;
            Panels[2].SetActive(true);
        }
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                AvailableBall--;
                RemainingBallText.text = AvailableBall.ToString();
                ThrowsBall.Play("ThrowsBall");
                ThrowsBallEffect.Play();
                OtherSounds[2].Play();
                Balls[ActiveBallIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
                Balls[ActiveBallIndex].SetActive(true);

                Balls[ActiveBallIndex].GetComponent<Rigidbody>().AddForce(Balls[ActiveBallIndex].transform.TransformDirection(90, 90, 0) * PowerBall, ForceMode.Force);

                if (Balls.Length - 1 == ActiveBallIndex)
                {
                    ActiveBallIndex = 0;
                }
                else
                {
                    ActiveBallIndex++;
                }
            }
        }

    }
    public void StopGame()
    {
        Panels[0].SetActive(true);
        Time.timeScale = 0;
    }
    public void OperationForPanels(string operation)
    {
        switch (operation)
        {
            case "Continue":
                Time.timeScale = 1;
                Panels[0].SetActive(false);
                break;
            case "Quit":
                Application.Quit();
                break;
            case "Settings":
                break;
            case "Again":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Next":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }
    public void ParcEffect(Vector3 _position, Color colorr)
    {
        BallEffects[ActiveBallEffectIndex].transform.position = _position;
        var main = BallEffects[ActiveBallEffectIndex].main;
        main.startColor = colorr;
        BallEffects[ActiveBallEffectIndex].gameObject.SetActive(true);
        ActiveBallIndex++;

        if (ActiveBallIndex == BallEffects.Length - 1)
        {
            ActiveBallIndex = 0;
        }
    }
    void Lost()
    {
        Time.timeScale = 0;
        OtherSounds[0].Play();
        LostLevelCount.text = "LEVEL: " + LevelName;
        Panels[2].SetActive(true);

    }
}
