using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int projectileDamage = 20;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        if(rb != null)
        {
            rb.velocity = Vector2.right * projectileSpeed;
        }
        Invoke("Disable", 2f);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * projectileSpeed;
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((layerMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(projectileDamage);
            Invoke("Disable", 0f);
            Debug.Log("Collided with something! layer number: " + collision.gameObject.layer);
        } else
        {
            Debug.Log("not in layer mask! object name: " + collision.gameObject.name);
        }
    }
}
