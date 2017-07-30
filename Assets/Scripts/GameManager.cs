using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class Score
{
    public float time = 0f;
}

public class GameManager : MonoBehaviour{
    public Text timer_;
    public Text end_game_;
    public Text end_game_countdown_;
    public Image velocity_meter_;
    
    public CarMovement carmovement_;
    public float time_ = 0f;

    private bool ended_ = false;
    private AudioSource audio_source_;

    [SerializeField]
    private List<Score> times_;

    void Start()
    {
        times_ = new List<Score>();
        audio_source_ = GetComponent<AudioSource>();
        string jsonstring = File.ReadAllText("scores.json");
        JsonUtility.FromJsonOverwrite(jsonstring, times_);
    }

    void Update()
    {
        if (ended_)
        {
            return;
        }

        if(!audio_source_.isPlaying && Time.time > 3f)
        {
            audio_source_.Play();
        }

        velocity_meter_.fillAmount = carmovement_.VelocityBar;
        time_ = Time.time;
        timer_.text = "Time: " + Time.time;
    }

    private IEnumerator LoadEndScene()
    {
        
        end_game_.text = "Game Ended";
        for (int i = 5; i > 0; --i)
        {
            end_game_countdown_.text = "Loading scores in: " + i;
            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene("scores");
    }

    public void EndGame()
    {
        if (ended_)
        {
            return;
        }

        ended_ = true;
        carmovement_.lock_ = true;
        Score score = new Score();
        score.time = time_;
        times_.Add(score);
        Debug.Log(JsonUtility.ToJson(times_));
        File.WriteAllText("scores.json", JsonUtility.ToJson(times_));
        StartCoroutine(LoadEndScene());
    }
}
