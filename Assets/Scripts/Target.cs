using UnityEngine;

public class Target : MonoBehaviour {
    public Animator wall_animator_;
    public Collider wall_collider_;
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
        Destroy(gameObject);
    }
}
