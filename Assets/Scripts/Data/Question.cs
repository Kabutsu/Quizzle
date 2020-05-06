using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public Guid Id { get; }
    public string Text { get; }
    public List<Answer> Answers { get; private set; }
    public Stack<Answer> DefaultAnswers { get; private set; }

    public Question(string text)
    {
        Id = Guid.NewGuid();
        Text = text;
        Answers = new List<Answer>();
        DefaultAnswers = new Stack<Answer>();
    }

    public void AddAnswer(string answerText)
    {
        Answers.Add(new Answer(answerText));
    }

    public void AddDefaultAnswer(string answerText)
        => DefaultAnswers.Push(new Answer(answerText));

    public Answer RandomDefaultAnswer()
    {
        Answer[] defaultAnswersArray = DefaultAnswers.ToArray();
        defaultAnswersArray.Shuffle();
        DefaultAnswers = new Stack<Answer>(defaultAnswersArray);
        return DefaultAnswers.Pop();
    }

    public void Vote(Guid answerId, Guid userId)
        => Answers.Find(x => x.Id.Equals(answerId)).Vote(userId);

    public void PointlessVote(Guid answerId, Guid userId)
        => Answers.Find(x => x.Id.Equals(answerId)).PointlessVote(userId);

    public List<Answer> GetAnswers()
    {
        List<Answer> answers = Answers;
        for (int i = answers.Count; i < 4; i++)
                answers.Add(RandomDefaultAnswer());
        return answers;
    }
}
