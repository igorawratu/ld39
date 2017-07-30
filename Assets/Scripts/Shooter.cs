using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    public Camera shooting_camera_;
    public Camera main_camera_;
    public GameObject bullet_prefab_;
    public float bullet_speed_ = 50f;
    public float cooldown_ = 1f;
    public Rigidbody car_rb_;
    public CarMovement cart_movement_;
    public AudioClip shooting_sound_;

    private float time_since_last_shot_ = 1f;
    
    private void UpdateCameras()
    {
        if (Input.GetMouseButton(1))
        {
            shooting_camera_.enabled = true;
            main_camera_.enabled = false;
        }
        else
        {
            shooting_camera_.enabled = false;
            main_camera_.enabled = true;
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && time_since_last_shot_ > cooldown_)
        {
            time_since_last_shot_ = 0f;
            var bullet = Instantiate(bullet_prefab_);
            bullet.transform.position = gameObject.transform.position;
            Bullet bscript = bullet.GetComponent<Bullet>();
            Vector3 vel = bullet_speed_ * gameObject.transform.forward;
            bscript.SetBulletVelocity(vel);
            AudioSource.PlayClipAtPoint(shooting_sound_, main_camera_.transform.position);
        }
    }

	void Update () {
        UpdateCameras();
        Shoot();
        
        time_since_last_shot_ += Time.deltaTime;
    }
}
