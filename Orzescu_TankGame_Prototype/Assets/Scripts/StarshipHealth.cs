using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarshipHealth : MonoBehaviour {

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public Slider healthBarSlider;

    private StarshipData data;


	// Use this for initialization
	void Start () {

        data = gameObject.GetComponent<StarshipData>();
        MaxHealth = data.totalHitPoints;
        CurrentHealth = MaxHealth;
        data.currentHitPoints = CurrentHealth;

        healthBarSlider.value = CalculateHealthPercentage();
    }

    void Update()
    {
        //TEST FOR DEALING DMG
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(10);
        }
    }
	
    public void TakeDamage(float dmgToTake)
    {
        CurrentHealth -= dmgToTake;
        data.currentHitPoints = CurrentHealth;
        healthBarSlider.value = CalculateHealthPercentage();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        CurrentHealth = 0;
        data.currentHitPoints = CurrentHealth;
        healthBarSlider.value = CalculateHealthPercentage();
        //FOR NOW: Destroy's object
        //in future will most likely take the visuals of the game object, disable them, and disable the controller on the object also!
        if (gameObject.tag == "Player")
        {
            Debug.Log("YOU'VE DIED!!!");
        }
        if (gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        Debug.Log(gameObject.name + "Took too much damage and died!");
    }

    public float CalculateHealthPercentage()
    {
        return CurrentHealth / MaxHealth;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log(gameObject.name + " was hit by a bullet");
            if (collision.gameObject.name != data.starShipTag)
            {
                //I got hit by a bullet that wasn't my own bullet
                TakeDamage(data.damageTakenPerBullet);
            }
        }
    }

}
