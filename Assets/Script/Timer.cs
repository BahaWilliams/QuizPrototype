using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteToAnswer = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public bool isAnswerQuestion = false;
    public float fillFraction;
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnswerQuestion)
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteToAnswer;
            }

            else
            {
                timerValue = timeToShowCorrectAnswer;
                isAnswerQuestion = false;
            }
        }

        else
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }

            else
            {
                isAnswerQuestion = true;
                timerValue = timeToCompleteToAnswer;
                loadNextQuestion = true;
            }
        }
    }
}
