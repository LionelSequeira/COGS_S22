using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Health Stats")]
    public int maxHealth = 100;

    [Header("Combo Stats")]
    public int maxI = 100;
    public int maxO = 100;
    public int maxP = 100;

    //Instance variables
    private int currentHealth;
    private int currentI;
    private int currentO;
    private int currentP;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentI = maxI;
        currentO = maxO;
        currentP = maxP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Damage Taken: " + damage);
        if (currentHealth <= 0) Die();
    }

    public bool reduceI(int amount)
    {
        if (currentI > amount)
        {
            currentI -= amount;
            return true;
        }
        return false;
    }

    public bool reduceO(int amount)
    {
        if (currentO > amount)
        {
            currentO -= amount;
            return true;
        }
        return false;
    }

    public bool reduceP(int amount)
    {
        if (currentP > amount)
        {
            currentP -= amount;
            return true;
        }
        return false;
    }

    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
    }
}
