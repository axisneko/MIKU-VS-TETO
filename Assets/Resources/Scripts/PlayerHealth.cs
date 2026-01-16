using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    int health;
    public TextMeshProUGUI HealthField;

    public GameObject DeathScreen;


    private void Start()
    {
        health = maxHealth;
        HealthField.text = health.ToString();
    }


    public void dealDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            GetComponent<playerMovement>().isAbleToMove = false;
            GetComponent<PlayerGameplay>().isAbleToShoot = false;
            DeathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        HealthField.text = health.ToString();
    }

    public void Respawn()
    {
        health = maxHealth;
        HealthField.text = health.ToString();
        GetComponent<playerMovement>().TeleportOnSpawn();
        GetComponent<playerMovement>().isAbleToMove = true;
        GetComponent<PlayerGameplay>().isAbleToShoot = true;
        DeathScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
