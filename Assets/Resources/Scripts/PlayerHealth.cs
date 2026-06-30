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
}
