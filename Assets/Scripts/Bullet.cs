using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float destruction_distance_ = 100f;
    public float rotation_speed_ = 360f;

    private Vector3 vel_ = Vector3.zero;
    private float travelled_ = 0f;

    public void SetBulletVelocity(Vector3 vel)
    {
        vel_ = vel;
        gameObject.transform.up = vel.normalized;
    }

	void Update () {
        if(travelled_ > destruction_distance_)
        {
            Destroy(gameObject);
        }

        gameObject.transform.position += vel_ * Time.deltaTime;
        travelled_ += vel_.magnitude * Time.deltaTime;

        //gameObject.transform.localRotation *= Quaternion.AngleAxis(rotation_speed_ * Time.deltaTime, Vector3.forward);
	}
}
