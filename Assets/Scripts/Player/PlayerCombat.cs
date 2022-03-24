using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Melee")]
    public Transform attackPoint;
    public Animator animator;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;
    [SerializeField] private GameObject projectilePool;

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }

    void FixedUpdate()
    {

    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.U)) MeleeAttack();
    }

    void MeleeAttack()
    {
        animator.SetTrigger("Melee Attack");
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }

        FireProjectile();
    }

    private void FireProjectile()
    {
        GameObject obj = projectilePool.GetComponent<ObjectPool>().GetPooledObject();
        if (obj == null) return;

        // replace "this" with a firePosition variable later, just using player transform for now.
        obj.transform.position = attackPoint.position;
        obj.transform.rotation = attackPoint.rotation;
        obj.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
