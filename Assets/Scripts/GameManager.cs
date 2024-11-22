using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private List<QuestionsData> questions;
    [SerializeField] private QuestionsData selectedQuestion;
    [SerializeField] private Timer timer;
    public bool isFinish = false;
    ScoreManager scoreManager;
    private List<int> doneQuestion = new List<int>();

    void Start()
    {
        scoreManager = GetComponent<ScoreManager>();
        
        if(SceneManager.GetActiveScene().name != "MainMenu"){
            SelectQuestion();
        }
    }

    void Update()
    {
        
    }

    public void SelectQuestion()
    {
        int questionIndex = Random.Range(0, questions.Count); 

        if (!doneQuestion.Contains(questionIndex)){
            doneQuestion.Add(questionIndex);

            selectedQuestion = questions[questionIndex];

            quizUI.SetQuestion(selectedQuestion);
            timer.ResetTimer();
        } else {
            if(doneQuestion.Count == questions.Count){
                FinishGame();
            } else if(doneQuestion.Contains(questionIndex)){
                SelectQuestion();
            }          
        }
    }

    public bool CheckAnswer(string answer)
    {
        bool correctAnswer = false;

        if(answer == selectedQuestion.correctAnswer){
            correctAnswer = true;
        }

        Invoke("SelectQuestion", 2f);

        return correctAnswer;
    }

    private void FinishGame()
    {
        scoreManager.isFinish = true;
        ChangeScene("MainMenu");
    }

    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
