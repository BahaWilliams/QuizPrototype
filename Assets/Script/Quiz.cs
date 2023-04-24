using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO currentQuestion;
    [SerializeField] TextMeshProUGUI quetionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();

    [Header("Answer")]
    [SerializeField] GameObject[] answerButton;
    int correctAnswerIndex;
    bool hasAnswerEarly = true;

    [Header("Buttons")]
    [SerializeField]  Sprite defaultAnswerSprite;
    [SerializeField]  Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        FillTimer();
    }

    void FillTimer()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnswerEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }

        else if (!hasAnswerEarly && !timer.isAnswerQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void LoopingQuestion()
    {
        quetionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButton.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnswerEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            quetionText.text = "Correct";
            buttonImage = answerButton[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncementCorrectAnswer();
        }

        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string getAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            quetionText.text = "Incorrect, the correct answer is " + getAnswer;
            buttonImage = answerButton[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            LoopingQuestion();
            progressBar.value++;
            scoreKeeper.IncementQuestionSeen();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    void SetDefaultButtonSprite()
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            Image buttonImage = answerButton[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
}