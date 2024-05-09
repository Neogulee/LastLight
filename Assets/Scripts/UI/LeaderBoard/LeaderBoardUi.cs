using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using TMPro;

public class LeaderBoardUi : MonoBehaviour
{
    private const int maxEntries = 10;
    public string filePath = "leaderboard.txt";
    public KillCountUi killCountUi;
    public TMP_Text[] textMeshProEntries;

    private void Awake()
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        gameObject.SetActive(false);
    }
    public void WriteNumberToFile(float number)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.Write(number.ToString() + " ");
            writer.WriteLine();
        }
    }
    private List<float> ReadScoresFromFile()
    {
        List<float> scores = new List<float>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] scoreStrings = line.Split(' ');
                foreach (string scoreString in scoreStrings)
                {
                    if (float.TryParse(scoreString, out float score))
                    {
                        scores.Add(score);
                    }
                }
            }
        }

        return scores;
    }
    private List<float> GetTopScores(List<float> scores)
    {
        scores.Sort((a, b) => b.CompareTo(a));

        List<float> topScores = new List<float>();
        for (int i = 0; i < Math.Min(maxEntries, scores.Count); i++)
        {
            topScores.Add(scores[i]);
        }

        return topScores;
    }
    private void UpdateTextMeshPro(List<float> topScores)
    {
        for (int i = 0; i < topScores.Count; i++)
        {
            if (i < textMeshProEntries.Length)
            {
                textMeshProEntries[i].text = topScores[i].ToString();
                textMeshProEntries[i].gameObject.SetActive(true);
            }
        }

        for (int i = topScores.Count; i < textMeshProEntries.Length; i++)
        {
            textMeshProEntries[i].gameObject.SetActive(false);
        }
    }
    
    private void SaveScore()
    {
        WriteNumberToFile(killCountUi.killcount);
    }
    
    private void ShowScore()
    {
        List<float> scores = ReadScoresFromFile();
        List<float> topScores = GetTopScores(scores);
        UpdateTextMeshPro(topScores);
    }

    public void FinishGame()
    {
        gameObject.SetActive(true);
        SaveScore();
        ShowScore();
    }
    
}
