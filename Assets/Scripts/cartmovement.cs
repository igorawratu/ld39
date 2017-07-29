using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cartmovement : MonoBehaviour {
    public float max_forward_vel_ = 50f;
    public float max_reverse_vel_ = 0f;

    public float acceleration_ = 100f;
    public float break_multiplier_ = 5f;
    public float rotation_speed_ = 90f;
    public float look_speed_ = 360f;
    public float jump_velocity_ = 25f;
    public float start_height_ = 25f;
    public float grounded_threshold = 1f;

    private Rigidbody rigid_body_;
    private bool grounded_ = false;
    private bool rotation_locked_ = false;

	void Start () {
        rigid_body_ = GetComponent<Rigidbody>();
    }

    private void UpdateMovement()
    {
        if (!grounded_)
        {
            return;
        }

        float forward = Input.GetAxis("Vertical");
        if (forward < 0)
        {
            forward *= 5f;
        }

        rigid_body_.AddForce(gameObject.transform.forward * forward, ForceMode.Acceleration);

        bool moving_forward = Vector3.Dot(rigid_body_.velocity, gameObject.transform.forward) > 0;
        float cap = moving_forward ? max_forward_vel_ : max_reverse_vel_;
        float currvelmag = rigid_body_.velocity.magnitude;

        float currvel = currvelmag > cap ? cap : currvelmag;
        if (!moving_forward)
        {
            currvel *= -1;
        }

        rigid_body_.velocity = gameObject.transform.forward * currvel;

        float rotate = Input.GetAxis("Horizontal");

        rigid_body_.angularVelocity = new Vector3(rigid_body_.angularVelocity.x, 0, rigid_body_.angularVelocity.z);

        Quaternion rotation = Quaternion.AngleAxis(rotate * Time.deltaTime * rotation_speed_, Vector3.up);
        gameObject.transform.localRotation *= rotation;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid_body_.velocity = new Vector3(rigid_body_.velocity.x, jump_velocity_, rigid_body_.velocity.z);
        }
    }

    private void CheckGrounded()
    {
        int mask = LayerMask.NameToLayer("Environment");
        grounded_ = Physics.Raycast(gameObject.transform.position, -gameObject.transform.up, grounded_threshold, 1 << mask);
    }

    private void UpdateJumpLook()
    {
        if (grounded_ || rotation_locked_)
        {
            return;
        }

        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");

        gameObject.transform.Rotate(Vector3.right, mousey * Time.deltaTime * look_speed_, Space.Self);
        gameObject.transform.Rotate(Vector3.up, mousex * Time.deltaTime * look_speed_, Space.World);
    }
	
    private void CheckRespawn()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rigid_body_.velocity = rigid_body_.angularVelocity = Vector3.zero;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, start_height_, gameObject.transform.position.z);
        }
    }

	void Update () {
        CheckGrounded();
        CheckRespawn();
        UpdateMovement();
        UpdateJumpLook();
    }

    void OnCollisionEnter(Collision collision)
    {
        rotation_locked_ = true;
    }

    void OnCollisionExit(Collision collision)
    {
        rotation_locked_ = false;
    }
}
