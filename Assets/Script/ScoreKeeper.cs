using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswer = 0;
    int questionSeen = 0;

    public int GetCorrectAnswer()
    {
        return correctAnswer;
    }

    public void IncementCorrectAnswer()
    {
        correctAnswer++;
    }

    public int GetQuestionSeen()
    {
        return questionSeen;
    }

    public void IncementQuestionSeen()
    {
        questionSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt( correctAnswer / (float)questionSeen * 100); 
    }
}
