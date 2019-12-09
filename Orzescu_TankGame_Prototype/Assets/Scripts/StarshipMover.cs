using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipMover : MonoBehaviour {

	public Transform tf;
    private CharacterController cc;

	// Use this for initialization
	void Start () {

        tf = this.gameObject.GetComponent<Transform>();
        cc = this.gameObject.GetComponent<CharacterController>();

	}
	
    public void Move(Vector3 moveSpeedAndDirection)
    {
        cc.SimpleMove(moveSpeedAndDirection); //already handles speed over time, so we don't need to do speed * time.deltatime
    }

    public void Turn(float turnSpeedAndDirection)
    {
        tf.Rotate(new Vector3(0, turnSpeedAndDirection * Time.deltaTime, 0));
    }



}
