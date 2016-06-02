﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    // Other Script Class
    public CameraScript cameraScript;
    public PlayerScript playerScript;

    public Transform target;

    // public int startingHealth = 20;                            // The amount of health the player starts the game with.
   // public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                            // Reference to the UI's health bar.
    public Image playerColor;
    // public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    // public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    // PlayerMovement playerMovement;                              // Reference to the player's movement.
    // PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.


    void Awake()
    {
        // Setting up the references.
        // playerMovement = GetComponent<PlayerMovement>();

        cameraScript = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        playerScript = GetComponent<PlayerScript>();

        // Set the initial health of the player.
        // currentHealth = playerScript.health;

    }


    void Update()
    {
        target = cameraScript.TargetReturn();
        if (target != transform)
            return;

        playerColor.color = playerScript.playerColor;
        // healthSlider.value = currentHealth;
        healthSlider.value = playerScript.health;


        /*
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        */
        //damaged = false;
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        //damaged = true;
        if (!playerScript.occultCloak)
        {
            // Reduce the current health by the damage amount.
            // currentHealth -= amount;
            playerScript.health -= amount;

            // Set the health bar's value to the current health.
            // healthSlider.value = currentHealth;
            healthSlider.value = playerScript.health;

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (playerScript.health <= 0 && !isDead)
            {
                // ... it should die.
                Death();
            }
        }
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        // playerShooting.DisableEffects();


        // Turn off the movement and shooting scripts.
        // playerMovement.enabled = false;
    }
}
