using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class EntityGeneralMechanics : MonoBehaviour
{
    //variables
    public float health;
    public float maxHealth;
    public float walkSpeed;
    public float mass;

    //bool variables
    public bool isAbleToTakeDamage;
    public bool isAbleToBeKilled;
    public bool isAbleToMove;
    public bool isAbleToUseItems;

    //inventory
    public Item[] items = new Item[5];
    public GameObject ItemHolder;
    public int currentSlot = 0;

    //behaviour types
    public string behaviourType; //player / notaplayer

    //notaplayer entity
    public string notaplayerEntityBehaviourType;
    public string notaplayerEntityDeathBehaviourType;

    //player controllable entity
    public GameObject PlayerDeathScreen;

    void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (behaviourType is "notaplayer")
        {
            if (notaplayerEntityBehaviourType is "paralyzed") { }
        }
        if (behaviourType is "player")
        {
            
        }
    }

    public void dealDamage(float amount)
    {
        if (isAbleToTakeDamage)
        {
            health -= amount;
        }
        if (health <= 0 && isAbleToBeKilled)
        {
            isAbleToMove = false;
            isAbleToUseItems = false;
            if (behaviourType is "player")
            {
                PlayerDeathScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                if (notaplayerEntityDeathBehaviourType is "standard" || notaplayerEntityDeathBehaviourType is "")
                {
                    Destroy(gameObject);
                }
            }
        }
        if (behaviourType is "player")
        {
            GetComponent<PlayerGameplay>().HealthField.text = Mathf.RoundToInt(health).ToString();
        }
        else
        {
            GetComponent<EnemyHealthDisplay>().UpdateHealthDisplay();
        }
    }
}
