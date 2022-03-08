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
            Debug.Log("Hit Enemy: " + enemy.name);
            enemy.GetComponent<EnemyTest>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
