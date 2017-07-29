using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    private GameManager manager;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        manager.EndGame();
    }
}
