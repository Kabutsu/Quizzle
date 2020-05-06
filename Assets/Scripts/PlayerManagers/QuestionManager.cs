using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public List<Question> Questions { get; private set; } = new List<Question>();

    private QuizQuestion[] DefaultQuestions =
    {
        new QuizQuestion(
            "What would the lamest superpower be?", new string[]
            {
                "Able to move through walls but can't take clothes with you",
                "Able to levitate but only 6 inches off the ground & at walking pace",
                "Turn invisible but only when nobody is looking",
                "Teleportation but you never know where you're going to end up"
            }
        ),
        new QuizQuestion(
            "You can only wear one outfit for the rest of your life. What is it?", new string[]
            {
                "A rabbit onesie",
                "A tuxedo",
                "Swimming shorts & flip-flops",
                "Full ski gear with jacket, trousers, boots, goggles & helmet"
            }
        ),
    };

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        foreach(QuizQuestion question in DefaultQuestions)
        {
            Question newQuestion = new Question(question.Question);
            foreach(string answer in question.Answers)
            {
                newQuestion.AddDefaultAnswer(answer);
            }
            Questions.Add(newQuestion);
        }
    }

    void Update()
    {
    }

    public void AddQuestion(string questionText)
    {
        Questions.Add(new Question(questionText));
    }

    public Question Question(Guid questionId) => Questions.Find(x => x.Id.Equals(questionId));
    public Guid[] QuestionGuids() => Questions.Select(x => x.Id).ToArray();
    public Guid[] AnswerGuids(Guid questionId)
        => Questions.Find(x => x.Id.Equals(questionId)).Answers.Select(y => y.Id).ToArray();

    public void AddAnswer(string answerText, Guid questionId)
    {
        Questions.Find(x => x.Id.Equals(questionId)).AddAnswer(answerText);
    }

    public void Vote(Guid questionId, Guid answerId, Guid userId)
        => Questions.Find(x => x.Id.Equals(questionId)).Vote(answerId, userId);

    public void PointlessVote(Guid questionId, Guid answerId, Guid userId)
        => Questions.Find(x => x.Id.Equals(questionId)).PointlessVote(answerId, userId);
}

class QuizQuestion
{
    public string Question { get; private set; }
    public string[] Answers { get; private set; }

    public QuizQuestion(string question, string[] answers)
    {
        Question = question;
        Answers = answers;
    }
}
