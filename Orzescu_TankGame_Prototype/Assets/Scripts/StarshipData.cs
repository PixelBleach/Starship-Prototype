using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipData : MonoBehaviour {

    [Header("Components")]
    public StarshipMover mover;
    public StarshipShooter shooter;
    public StarshipHealth health;

    [Header("Data")]
    public string starShipTag = "Starship1";
    [Tooltip("Turning Speed in Degree's per Second")]
    public float turnSpeed;
    [Tooltip("Move Speed in Meter's per Second")]
    public float moveForwardSpeed;
    [Tooltip("Move Speed in Meter's per Second")]
    public float moveBackwardSpeed;
    [Tooltip("Total HP")]
    public float totalHitPoints;
    public float currentHitPoints;
    public float currentScore;
    public float scoreIncrementOnEnemyKill;
    public Transform bulletSpawnOrigin;
    [Tooltip("Firing Force in Meter's per Second")]
    public float bulletFiringForce;
    [Tooltip("Damage done in Hitpoints")]
    public float bulletDamageValue;
    public float damageTakenPerBullet;

    void Start()
    {
        mover = this.gameObject.GetComponent<StarshipMover>();
        shooter = this.gameObject.GetComponent<StarshipShooter>();
        health = this.gameObject.GetComponent<StarshipHealth>();
    }


}
