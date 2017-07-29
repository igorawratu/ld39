using UnityEngine;

public class Target : MonoBehaviour {
    public Animator wall_animator_;
    public Collider wall_collider_;
	void OnTriggerEnter(Collider collider)
    {
        wall_collider_.enabled = false;
        wall_animator_.SetBool("blowup", true);
    }
}
