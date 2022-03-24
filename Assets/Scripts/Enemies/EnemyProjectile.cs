using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int projectileDamage = 20;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = transform.right * projectileSpeed;
        }
        Invoke("Disable", 4f);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * projectileSpeed;
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
        if (collision.gameObject.tag == "Player")
        {
            Invoke("Disable", 0f);
        }
    }
}
