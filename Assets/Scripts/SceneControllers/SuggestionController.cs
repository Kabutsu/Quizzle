using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SuggestionController : MonoBehaviour
{
    private QuestionManager QuestionManager;

    [SerializeField] private Text QuestionText;
    [SerializeField] private Button SubmitButton;
    [SerializeField] private InputField InputField;
    [SerializeField] private Slider TimeRemainingSlider;
    private Image TimeRemainingSliderImage;

    private Guid SelectedQuestionGuid;

    public float TotalTime;
    private float TimeLeft;
    private bool CountingDown = true;

    private Vector2 SubmitButtonWidthUnchecked;
    private readonly Vector2 SubmitButtonWidthChecked = new Vector2(110, 100);

    void Start()
    {
        QuestionManager = GameObject
            .FindGameObjectWithTag("QuestionManager")
            .GetComponent<QuestionManager>();

        SelectedQuestionGuid = QuestionManager
            .QuestionGuids()[
                UnityEngine.Random.Range(0, QuestionManager.QuestionGuids().Length)];

        QuestionText.text = QuestionManager.Question(SelectedQuestionGuid).Text;

        TimeRemainingSliderImage = TimeRemainingSlider
            .GetComponentsInChildren<Image>()
            .Where(x => x.name.Contains("Fill"))
            .FirstOrDefault();

        TimeLeft = TotalTime;

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

                QuestionManager.AddAnswer(
                    InputField.text.Trim().Length == 0
                        ? QuestionManager.Question(SelectedQuestionGuid).RandomDefaultAnswer().Text
                        : InputField.text,
                    SelectedQuestionGuid);

                GameObject
                    .FindGameObjectWithTag("SceneController")
                    .GetComponent<GameSceneManager>()
                    .LoadNextScene();
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
