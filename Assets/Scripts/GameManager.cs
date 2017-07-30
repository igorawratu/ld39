using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    public Text timer_;
    public Text end_game_;
    public Text end_game_countdown_;
    public Image velocity_meter_;

    public CarMovement carmovement_;
    public float time_ = 0f;

    private bool ended_ = false;

    public List<float> times_;

    void Start()
    {
        times_ = new List<float>();
        string jsonstring = File.ReadAllText("scores.json");
        JsonUtility.FromJsonOverwrite(jsonstring, times_);
    }

    void Update()
    {
        if (ended_)
        {
            return;
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
        times_.Add(time_);
        File.WriteAllText("scores.json", JsonUtility.ToJson(times_));
        StartCoroutine(LoadEndScene());
    }
}
