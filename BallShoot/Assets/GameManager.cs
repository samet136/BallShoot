using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    [Header("BALL SETTINGS")]
    public GameObject[] Balls;
    public GameObject FirePoint;
    [SerializeField] private float PowerBall;
    int ActiveBallIndex;
    [SerializeField] private int AvailableBall;


    [Header("LEVEL SETTINGS")]
    [SerializeField] private int TargetBall;
    int EnteringBall;

    public Slider TargetSlider;
    public TextMeshProUGUI RemainingBallText;
    void Start()
    {
        TargetSlider.maxValue = TargetBall;
        RemainingBallText.text = AvailableBall.ToString();
    }
    public void BallEntered()
    {
        EnteringBall++;
        TargetSlider.value = EnteringBall;
        if (EnteringBall == AvailableBall)
        {
            //Top Atma Ýþlemini Kilitleyeceðiz
            Debug.Log("KAZANDIN");
        }
        if (AvailableBall == 0 && EnteringBall != TargetBall)
        {
            Debug.Log("Kayebettin");
        }
        if (AvailableBall + EnteringBall < TargetBall)
        {
            Debug.Log("Kaybettin");
        }
    }
    public void DidntEnter()
    {


        if (AvailableBall == 0)
        {
            Debug.Log("Kaybettin");
        }
        if (AvailableBall + EnteringBall < TargetBall)
        {
            Debug.Log("Kaybettin");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AvailableBall--;
            RemainingBallText.text = AvailableBall.ToString();
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
