using System;
using System.Collections.Generic;
using UnityEngine;

public class ResultsController : MonoBehaviour
{
    [SerializeField] private GameObject UserProfilePrefab;
    [SerializeField] private GameObject[] SpawnPoints = new GameObject[8];
    private QuestionManager QuestionManager;
    private PlayerRegister PlayerRegister;

    private List<User> RankedUsers;
    private int currentSpawnPoint;

    void OnEnable()
    {
        QuestionManager = GameObject
            .FindGameObjectWithTag("QuestionManager")
            .GetComponent<QuestionManager>();

        PlayerRegister = GameObject
            .FindGameObjectWithTag("QuestionManager")
            .GetComponent<PlayerRegister>();
    }

    void Start()
    {
        currentSpawnPoint = SpawnPoints.Length - PlayerRegister.Players.Count;

        foreach(Question question in QuestionManager.Questions)
        {
            List<Answer> answers = question.Answers;
            answers.Sort(new AnswerComparer());
            answers.Reverse();

            List<Answer> minAnswers = new List<Answer>();
            minAnswers.Add(answers[answers.Count - 1]);

            int i = answers.Count - 1;
            while(i >= 0 && answers[i].Votes.Count == answers[i -1].Votes.Count)
            {
                i--;
                minAnswers.Add(answers[i]);
            }

            foreach(Answer answer in minAnswers)
            {
                foreach (Guid userId in answer.PointlessVotes)
                    PlayerRegister.GetUser(userId).ScorePoint();
            }
        }

        RankedUsers = new List<User>(PlayerRegister.Players);
        RankedUsers.Sort(new UserComparer());

        foreach (User player in RankedUsers)
            AddProfile(player);
    }

    void Update()
    {
    }

    private void AddProfile(User userInfo)
    {
        Transform spawnPoint = SpawnPoints[currentSpawnPoint].transform;
        var profile = Instantiate(UserProfilePrefab, spawnPoint.position, spawnPoint.rotation);
        profile.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
        profile.GetComponent<UserProfileController>().SetUI(userInfo.Avatar, userInfo.Nickname, userInfo.Score);

        currentSpawnPoint++;
    }
}
