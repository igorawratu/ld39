using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour{
    public Text timer_;
    public Text end_game_;
    public Text end_game_countdown_;
    public Image velocity_meter_;
    
    public CarMovement carmovement_;
    public float time_ = 0f;
    public float start_time_ = 0f;

    private bool ended_ = false;
    private AudioSource audio_source_;

    void Start()
    {
        audio_source_ = GetComponent<AudioSource>();
        start_time_ = Time.time;
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
        time_ = Time.time - start_time_;
        timer_.text = "Time: " + time_;
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
        Scores.Instance.scores_.Add(time_);
        StartCoroutine(LoadEndScene());
    }
}
