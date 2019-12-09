using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint_AIController : MonoBehaviour {

	public StarshipData data;
	public Transform[] waypointsArray;
	public List<Transform> waypointsList;

	public bool isActive = true;

	[Header("Waypoint Info")]
	[SerializeField]
	private int currentWaypoint;
	public PatrolTypes patrolType;

	[SerializeField]
	private int patrolDirection; //1 = forward, -1 = backward

	public float moveSpeed;
	public float closeEnoughDistance; //Buffer for waypoints, so if you're next to waypoint it's "good enough" and you can progess through to the next waypoint

	// Use this for initialization
	void Start () {
	
		data = GetComponent<StarshipData> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (!isActive) {
			return;
		}

		Patrol ();
		
	}

	void Patrol () {

		if (Vector3.Distance(data.mover.tf.position, waypointsList[currentWaypoint].position) > closeEnoughDistance) {

			moveSpeed = 0.0f;

			//Turn to face the waypoint

			//create vector from our position to target position : (targetPoint - startPoint)
			Vector3 vectorToTarget = waypointsList [currentWaypoint].position - data.mover.tf.position;
			//create "set of rules" (quaternion) that'll point the object down the target vector
			Quaternion targetRotation = Quaternion.LookRotation (vectorToTarget, Vector3.up);
	
			//If not facing the waypoint
			if (data.mover.tf.rotation != targetRotation) {

				//Move part-way, to where the look is
				data.mover.tf.rotation = Quaternion.RotateTowards (data.mover.tf.rotation, targetRotation, data.turnSpeed * Time.deltaTime);

			} else { 
				//Debug.Log ("MOVING BABBBBYYYYYY!!!");
				//if we are, move forward to point
				moveSpeed = data.moveForwardSpeed;
				vectorToTarget = vectorToTarget.normalized * moveSpeed;
			}

			data.mover.Move (vectorToTarget);

		} else {
			//We're at the waypoint

			if (patrolType == PatrolTypes.Loop) {
				currentWaypoint++;
				if (currentWaypoint >= waypointsList.Count) {
					currentWaypoint = 0;
				}
			}
			else if (patrolType == PatrolTypes.Stop) {
				currentWaypoint++;
				if (currentWaypoint >= waypointsList.Count) {
					isActive = false;
				}
			}
			else if (patrolType == PatrolTypes.PingPong) {
				currentWaypoint += patrolDirection;
				if (currentWaypoint >= waypointsList.Count) {
					patrolDirection = -1;
					currentWaypoint = waypointsList.Count - 2;
				} else if (currentWaypoint <= 0) {
					patrolDirection = 1;
					currentWaypoint = 0;
				}
			}

		}
	}

}
