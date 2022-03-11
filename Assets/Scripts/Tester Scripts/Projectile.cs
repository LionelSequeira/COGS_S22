using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 1f;

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
}
