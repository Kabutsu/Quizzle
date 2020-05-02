using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SuggestionController : MonoBehaviour
{
    [SerializeField] private Text QuestionText;
    [SerializeField] private Button SubmitButton;
    [SerializeField] private InputField InputField;
    [SerializeField] private Slider TimeRemainingSlider;
    private Image TimeRemainingSliderImage;

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
    private string Answer;

    public float TotalTime;
    private float TimeLeft;
    private bool CountingDown = true;

    void Start()
    {
        TimeRemainingSliderImage = TimeRemainingSlider
            .GetComponentsInChildren<Image>()
            .Where(x => x.name.Contains("Fill"))
            .FirstOrDefault();

        TimeLeft = TotalTime;

        SelectedQuestionIndex = Random.Range(0, Questions.Length);
        QuestionText.text = Questions[SelectedQuestionIndex].Question;
    }

    void Update()
    {
        if(CountingDown)
        {
            TimeLeft -= Time.deltaTime;

            float timeLeftClamped = TimeLeft / TotalTime;
            TimeRemainingSlider.value = timeLeftClamped;
            TimeRemainingSliderImage.LerpColor3(Color.green, Color.yellow, Color.red, 0.5f, timeLeftClamped);

            if (TimeLeft <= 0f)
            {
                CountingDown = false;

                if (InputField.text.Trim().Length == 0)
                {
                    string[] answers = Questions[SelectedQuestionIndex].Answers;
                    Answer = answers[Random.Range(0, answers.Length)];
                } else
                {
                    Answer = InputField.text;
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
