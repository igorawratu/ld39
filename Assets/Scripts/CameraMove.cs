using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public GameObject object_to_follow_;
    public float max_distance_ = 5f;
    public float desired_height_ = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 curr = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        Vector2 follow = new Vector2(object_to_follow_.transform.position.x, object_to_follow_.transform.position.z);

        if(Vector2.Distance(curr, follow) > max_distance_)
        {
            Vector2 dif = curr - follow;
            Vector2 desired_p = dif.normalized * max_distance_ + follow;
            gameObject.transform.position = new Vector3(desired_p.x, 0, desired_p.y);
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, object_to_follow_.transform.position.y + desired_height_, gameObject.transform.position.z);
        gameObject.transform.LookAt(object_to_follow_.transform);
	}
}
