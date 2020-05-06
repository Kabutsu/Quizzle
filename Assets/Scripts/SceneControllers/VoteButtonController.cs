using System;
using UnityEngine;
using UnityEngine.UI;

public class VoteButtonController : MonoBehaviour
{
    private QuestionManager QuestionManager;
    private Question Question;
    private Answer Answer;
    private bool IsPointlessQuestion;

    void Start()
    {
        QuestionManager = GameObject
            .FindGameObjectWithTag("QuestionManager")
            .GetComponent<QuestionManager>();
    }

    void Update()
    {
    }

    public void Construct(Question question, Answer answer, bool isPointlessQuestion = false)
    {
        Question = question;
        Answer = answer;
        IsPointlessQuestion = isPointlessQuestion;

        gameObject.GetComponentInChildren<Text>().text = Answer.Text;
    }

    public void Vote(Guid userId)
    {
        if(IsPointlessQuestion)
        {
            QuestionManager.PointlessVote(Question.Id, Answer.Id, userId);
        } else
        {
            QuestionManager.Vote(Question.Id, Answer.Id, userId);
        }
    }
}
