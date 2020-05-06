using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VoteController : MonoBehaviour
{
    private QuestionManager QuestionManager;
    private Stack<Question> Questions;

    [SerializeField] private Button[] VoteButtons = new Button[4];
    [SerializeField] private bool IsPointlessRound;
    [SerializeField] private Text QuestionText;
    [SerializeField] private Slider TimeRemainingSlider;
    private Image TimeRemainingSliderImage;

    public float TotalTime;
    private float TimeLeft;
    private bool CountingDown = true;

    void Start()
    {
        QuestionManager = GameObject
            .FindGameObjectWithTag("QuestionManager")
            .GetComponent<QuestionManager>();

        Questions = new Stack<Question>(QuestionManager.Questions);

        IsPointlessRound = SceneManager.GetActiveScene().name == "GameVote";


        TimeRemainingSliderImage = TimeRemainingSlider
            .GetComponentsInChildren<Image>()
            .Where(x => x.name.Contains("Fill"))
            .FirstOrDefault();

        NextQuestionOrScene();

        TimeLeft = TotalTime;
    }

    void Update()
    {
        if (CountingDown)
        {
            TimeLeft -= Time.deltaTime;

            float timeLeftClamped = TimeLeft / TotalTime;
            TimeRemainingSlider.value = timeLeftClamped;
            TimeRemainingSliderImage.LerpColor3(Color.green, Color.yellow, Color.red, 0.5f, timeLeftClamped);

            if (TimeLeft <= 0f)
            {
                CountingDown = false;
                NextQuestionOrScene();
            }
        }
    }

    private void NextQuestionOrScene()
    {
        try
        {
            Questions.Peek();
        } catch (InvalidOperationException)
        {
            GameObject
                .FindGameObjectWithTag("SceneController")
                .GetComponent<GameSceneManager>()
                .LoadNextScene();
            return;
        }

        Question nextQuestion = Questions.Pop();
        List<Answer> answers = nextQuestion.GetAnswers();
        QuestionText.text = nextQuestion.Text;

        foreach((Button voteButton, int i) in VoteButtons.Select((voteButton, i) => (voteButton, i)))
        {
            voteButton
                .GetComponent<VoteButtonController>()
                .Construct(nextQuestion, answers[i], IsPointlessRound);
        }

        TimeLeft = TotalTime;
        CountingDown = true;
    }
}
