using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionsData
{
    public string questionInfo;
    public List<string> options;
    public string correctAnswer;
    public QuestionType questionType;
    public Sprite questionImage;
    public List<Sprite> answersImage;
    public AnswersType answersType;

}

public enum QuestionType
{
    TEXT,
    IMAGE
}

public enum AnswersType
{
    TEXT,
    IMAGE
}