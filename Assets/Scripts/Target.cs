using UnityEngine;
using UnityEngine.Audio;

public class Target : MonoBehaviour {
    public Animator wall_animator_;
    public Collider wall_collider_;
    public AudioClip explosion_;
    public Camera main_camera_;
    void Start()
    {
        main_camera_ = Camera.main;
    }

	void OnTriggerEnter(Collider collider)
    {
        if (wall_collider_ == null)
        {
            var colliders = gameObject.transform.parent.gameObject.GetComponentsInChildren<Collider>();
            for(int i = 0; i < colliders.Length; ++i)
            {
                colliders[i].enabled = false;
            }
        }
        else
        {
            wall_collider_.enabled = false;
        }
        
        wall_animator_.SetBool("blowup", true);
        AudioSource.PlayClipAtPoint(explosion_, main_camera_.transform.position, 2f);
    }
}
