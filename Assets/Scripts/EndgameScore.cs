using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class EndgameScore : MonoBehaviour {
    public Text your_score_;
    public Text score_list_;
    public List<float> scores_;


    void Start () {
        scores_ = new List<float>();
        JsonUtility.FromJsonOverwrite(File.ReadAllText("scores.json"), scores_);

        if(scores_.Count > 0)
        {
            float yourscore = scores_[scores_.Count - 1];

            scores_.Sort();
            if(scores_.Count > 10)
            {
                scores_.RemoveRange(10, scores_.Count - 10);
            }

            your_score_.text = "Your time: " + yourscore;

            score_list_.text = "Top Scores: \n";
            for (int i = 0; i < scores_.Count; ++i)
            {
                score_list_.text += scores_[i] + '\n';
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
