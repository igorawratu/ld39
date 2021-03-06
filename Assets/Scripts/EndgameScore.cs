﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class EndgameScore : MonoBehaviour {
    public Text your_score_;
    public Text score_list_;


    void Start () {
        List<float> scores = Scores.Instance.scores_;

        if(scores.Count > 0)
        {
            float yourscore = scores[scores.Count - 1];

            scores.Sort();
            if(scores.Count > 10)
            {
                scores.RemoveRange(10, scores.Count - 10);
            }

            your_score_.text = "Your time: " + yourscore;

            score_list_.text = "Top times: \n";
            for (int i = 0; i < scores.Count; ++i)
            {
                Debug.Log(scores[i]);
                score_list_.text += scores[i] + "\n";
            }
        }
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }
	}
}
