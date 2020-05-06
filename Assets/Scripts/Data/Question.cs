using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public Guid Id { get; }
    public string Text { get; }
    public List<Answer> Answers { get; private set; }
    public List<Answer> DefaultAnswers { get; }

    public Question(string text)
    {
        Id = Guid.NewGuid();
        Text = text;
        Answers = new List<Answer>();
        DefaultAnswers = new List<Answer>();
    }

    public void AddAnswer(string answerText)
    {
        Answers.Add(new Answer(answerText));
    }

    public void AddDefaultAnswer(string answerText)
    {
        DefaultAnswers.Add(new Answer(answerText));
    }

    public Answer RandomDefaultAnswer()
        => DefaultAnswers[UnityEngine.Random.Range(0, DefaultAnswers.Count)];

    public void Vote(Guid answerId, Guid userId)
        => Answers.Find(x => x.Id.Equals(answerId)).Vote(userId);

    public void PointlessVote(Guid answerId, Guid userId)
        => Answers.Find(x => x.Id.Equals(answerId)).PointlessVote(userId);
}
