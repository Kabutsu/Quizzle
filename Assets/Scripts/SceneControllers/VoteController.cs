using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteController : MonoBehaviour
{
    private QuestionManager QuestionManager;

    void Start()
    {
        QuestionManager = GameObject
            .FindGameObjectWithTag("QuestionManager")
            .GetComponent<QuestionManager>();
    }

    void Update()
    {
    }
}
