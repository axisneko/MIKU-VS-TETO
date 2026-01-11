using TMPro;
using UnityEngine;

public class EnemyStandardMechanics : MonoBehaviour
{
    public int health;

    public TextMeshPro healthDisplay;

    public void dealDamage(int amount)
    {
        health -= amount;
        healthDisplay.text = health.ToString();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
