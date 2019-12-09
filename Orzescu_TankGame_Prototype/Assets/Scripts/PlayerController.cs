using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Components")]
    public StarshipData data;

    [Header("Input Keys")]
    public KeyCode forwardKey;
    public KeyCode backwardKey;
    public KeyCode turnLeftKey;
    public KeyCode turnRightKey;
    public KeyCode shootKey;

    [Tooltip("Time between shots in seconds")]
    public float shootingCooldown;

    private bool canFire;
    private float countdown;

	// Use this for initialization
	void Start () {
        canFire = true;
        countdown = shootingCooldown;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 forward;

        countdown -= Time.deltaTime;

        // MOVEMENT BLOCK
        float moveSpeed = 0.0f;
        if (Input.GetKey(forwardKey))
        {
            moveSpeed = data.moveForwardSpeed;
        }
        if (Input.GetKey(backwardKey))
        {
            //TODO: Make backwards movement maybe?? for now basic backwards movement
            moveSpeed = data.moveBackwardSpeed;
        }
        //AFTER WE DETERMINE which direction the player wants to move, then send message to move
        forward = this.gameObject.transform.forward * moveSpeed;
        data.gameObject.SendMessage("Move", forward);

        // TURNING BLOCK
        if (Input.GetKey(turnRightKey))
        {
            data.gameObject.SendMessage("Turn", data.turnSpeed);
        }
        if (Input.GetKey(turnLeftKey))
        {
            data.gameObject.SendMessage("Turn", -1 * data.turnSpeed);
        }

        // SHOOTING BLOCK
        if (Input.GetKeyDown(shootKey))
        {
            if (countdown <= 0) //We can shoot! Cooldown expired
            {
                data.gameObject.SendMessage("Shoot", data.bulletFiringForce);

                countdown = shootingCooldown;
            } else {
                //We can't shoot!
                Debug.Log("Shooting Cooldown has not expired yet. You have " + countdown + " seconds until you can shoot again");

            }

        }

    }
}
