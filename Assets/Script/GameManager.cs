using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndSceen endSceen;


    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endSceen = FindObjectOfType<EndSceen>();
    }

    void Start()
    {
        quiz.gameObject.SetActive(true);
        endSceen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endSceen.gameObject.SetActive(true);
            endSceen.ShowFinalScore();
        }
    }

    public void OnReplayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
