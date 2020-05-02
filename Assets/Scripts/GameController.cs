using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Slider TimeRemainingSlider;
    [SerializeField] private Text QuestionText;
    [SerializeField] private Button SubmitButton;
    [SerializeField] private InputField InputField;
    private Text AnswerText;

    private QuizQuestion[] Questions =
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
                "A bathing suit",
                "A ski jacket & thick ski trousers, with goggles and helmet"
            }
        ),
    };

    private int SelectedQuestionIndex;

    public float TotalTime;
    private float TimeLeft;
    private bool CountingDown = true;

    void Start()
    {
        TimeLeft = TotalTime;
        AnswerText = InputField
            .GetComponentsInChildren<Text>()
            .Where(x => x.name.Contains("Placeholder"))
            .FirstOrDefault();

        SelectedQuestionIndex = Random.Range(0, Questions.Length);
        QuestionText.text = Questions[SelectedQuestionIndex].Question;
    }

    void Update()
    {
        if(CountingDown)
        {
            TimeLeft -= Time.deltaTime;
            TimeRemainingSlider.value = Mathf.Max(0f, TimeLeft / TotalTime);

            if(TimeLeft <= 0f)
            {
                CountingDown = false;

                if (AnswerText.text.Trim().Length == 0)
                {
                    string[] answers = Questions[SelectedQuestionIndex].Answers;
                    string answer = answers[Random.Range(0, answers.Length)];
                    Debug.Log(answer);
                }
            }
        }
    }

    public void OnSubmit()
    {
        InputField.readOnly = true;
        SubmitButton.enabled = false;
    }
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
