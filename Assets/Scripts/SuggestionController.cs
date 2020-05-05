using System.Collections;
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

    private Vector2 SubmitButtonWidthUnchecked;
    private readonly Vector2 SubmitButtonWidthChecked = new Vector2(110, 100);

    void Start()
    {
        TimeRemainingSliderImage = TimeRemainingSlider
            .GetComponentsInChildren<Image>()
            .Where(x => x.name.Contains("Fill"))
            .FirstOrDefault();

        TimeLeft = TotalTime;

        SelectedQuestionIndex = Random.Range(0, Questions.Length);
        QuestionText.text = Questions[SelectedQuestionIndex].Question;

        SubmitButtonWidthUnchecked = SubmitButton.GetComponent<RectTransform>().sizeDelta;

        SubmitButton.interactable = false;
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

    public void OnInputFieldChange(string inputText)
        => SubmitButton.interactable = (inputText.Trim().Length > 0);

    public void OnSubmit()
    {
        InputField.interactable = false;
        SubmitButton.interactable = false;

        StartCoroutine(SetButtonToOk(0.5f));
    }

    IEnumerator SetButtonToOk(float overTime)
    {
        var t = 0f;
        RectTransform submitButtonRect = SubmitButton.GetComponent<RectTransform>();
        Text submitButtonText = SubmitButton.GetComponentInChildren<Text>();
        Image submitButtonImage = SubmitButton.GetComponent<Image>();
        Image okImage = SubmitButton
            .GetComponentsInChildren<Image>()
            .Where(x => x.name.Contains("SubmitButtonCheck"))
            .FirstOrDefault();

        while (t < 1)
        {
            t += (Time.deltaTime / overTime);
            submitButtonRect.sizeDelta = Vector2.Lerp(SubmitButtonWidthUnchecked, SubmitButtonWidthChecked, t);
            submitButtonImage.color = Color.Lerp(Color.white, Color.green, t);
            if (t <= 0.5f)
            {
                submitButtonText.color = Color.Lerp(Color.gray, Color.clear, t * 2f);
            } else
            {
                okImage.color = Color.Lerp(Color.clear, Color.white, (t - 0.5f) * 2f);
            }

            yield return null;
        }
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
