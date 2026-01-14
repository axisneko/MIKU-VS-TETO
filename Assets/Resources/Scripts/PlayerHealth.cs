using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    int health;
    public TextMeshProUGUI HealthField;

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
            gameObject.transform.position = new Vector3(0, 0.22f, 0);
            health = maxHealth;
        }
        HealthField.text = health.ToString();
    }
}
