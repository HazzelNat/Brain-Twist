using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{   
    [Header("Game Scene")]
    [SerializeField] public List<Image> answersImage;
    [SerializeField] public List<Button> options;
    [SerializeField] public List<GameObject> panel;
    [SerializeField] private TextMeshProUGUI questionNumberText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Timer timer;

    [Header("Data")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Image questionImage;
    
    AudioManager audioManager;

    
    
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

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
                questionText.transform.gameObject.SetActive(true);
                questionImage.transform.gameObject.SetActive(false);
                questionText.text = questions.questionInfo;

                break;
            case QuestionType.IMAGE:
                questionText.transform.gameObject.SetActive(false);
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
                    // options[i].GetComponentInChildren<Image>().sprite = answerListImage[i];
                    answersImage[i].transform.gameObject.SetActive(true);
                    answersImage[i].sprite = answerListImage[i];
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
                timer.StopTimer();
                audioManager.PlaySFX(audioManager.correct);
            } else {
                panel[1].SetActive(true);                       // Activate LosePanel
                Invoke("RemovePanel", 2f);
                timer.StopTimer();
                audioManager.PlaySFX(audioManager.wrong);
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
            answersImage[i].transform.gameObject.SetActive(false);
        }
    }

    private void IncrementQuestion(){
        questionNumber++;
        currentQuestionNumber = $"Q{questionNumber}";
        questionNumberText.text = currentQuestionNumber;
    }
}
