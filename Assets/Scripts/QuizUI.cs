using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Image questionImage;
    [SerializeField] private List<Image> answersImage;
    [SerializeField] private List<Button> options;
    [SerializeField] public List<GameObject> panel;
    [SerializeField] private TextMeshProUGUI questionNumberText;
    private string currentQuestionNumber;
    private int questionNumber = 1;
    private QuestionsData questions;
    private bool answered;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void Awake() {
        for(int i=0 ; i<options.Count ; i++){
            Button localButton = options[i];
            localButton.onClick.AddListener(() => OnClick(localButton));
        }
    }

    public void SetQuestion(QuestionsData questions)
    {
        RemoveImage();

        this.questions = questions;

        List<string> answerList = questions.options;
        List<Sprite> answerListImage = questions.answersImage;

        switch(questions.questionType)
        {
            case QuestionType.TEXT:
                questionImage.transform.gameObject.SetActive(false);
                questionText.text = questions.questionInfo;

                break;
            case QuestionType.IMAGE:
                questionImage.transform.gameObject.SetActive(true);
                questionImage.sprite = questions.questionImage;

                break;

            default:
                break;
        }

        switch(questions.answersType)
        {
            case AnswersType.TEXT:
                for (int i=0 ; i<options.Count ; i++){
                    options[i].GetComponentInChildren<TextMeshProUGUI>().text = answerList[i];
                    options[i].name = answerList[i];
                }

                answered = false;

                break;
            case AnswersType.IMAGE:
                for (int i=0 ; i<options.Count ; i++){
                    options[i].GetComponentInChildren<TextMeshProUGUI>().text = null;
                    options[i].GetComponentInChildren<Image>().sprite = answerListImage[i];
                    options[i].name = answerList[i];
                }

                answered = false;

                break;

            default:
                break;
        }
    }

    private void OnClick(Button answerButton)
    {
        if(!answered){
            answered = true;
            bool correct = gameManager.CheckAnswer(answerButton.name);

            if(correct){
                panel[0].SetActive(true);                       // Activate WinPanel
                Invoke("RemovePanel", 2f);
            } else {
                panel[1].SetActive(true);                       // Activate LosePanel
                Invoke("RemovePanel", 2f);
            }
            Invoke("IncrementQuestion", 2f);
        }
    }

    private void RemovePanel()
    {
        panel[0].SetActive(false);
        panel[1].SetActive(false);
    }

    private void RemoveImage()
    {
        for (int i=0 ; i<options.Count ; i++){
            options[i].GetComponentInChildren<Image>().sprite = null;
        }
    }

    private void IncrementQuestion(){
        questionNumber++;
        currentQuestionNumber = $"Q{questionNumber}";
        questionNumberText.text = currentQuestionNumber;
    }
}
