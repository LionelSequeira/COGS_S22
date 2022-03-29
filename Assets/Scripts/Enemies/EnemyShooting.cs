using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Enemy Pool")]
    [SerializeField] private GameObject projectilePool;

    [Header("Player Detection")]
    [SerializeField] private GameObject player;

    [Header("Shooting Direction")]
    [SerializeField] private float spreadX; //X offset (translational change)
    [SerializeField] private float spreadY; //Y offset (translational change)
    [Range(-360f, 360f)] [SerializeField] private float spreadDegrees; //Degree difference between consecutive projectiles
    [Range(-20f, 20f)] [SerializeField] private float centerRadius;
    [Range(-180f, 180f)] [SerializeField] private float centerRotation;
    [SerializeField] private bool uniDirectionalPitch; //If turned ON: every projectile has the same same facing the center
    [Range(-180f, 180f)] [SerializeField] private float pitch; //Determines rotation of each individual projectile
    [SerializeField] private bool playerHoming; //Only detects player position ONCE when instantiated

    //NOTE: If both uniDirectionalPitch and playerHoming are checked, uniDirectionalPitch will take precedence!

    [Header("Shooting Mechanics")]
    [Range(0, 20)] [SerializeField] private int numberOfPoints;
    [Range(0f, 5f)] [SerializeField] private float timeBetweenProjectiles;

    //Instance Variables
    Vector2 pos;
    Quaternion rotation;
    float timeActivated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (Time.time - timeActivated > timeBetweenProjectiles)
        {
            Shoot();
            timeActivated = Time.time;
        }
    }

    void Shoot()
    {
        for(int i = 0; i < numberOfPoints; i++)
        {
            pos = (Vector2)transform.position + new Vector2(centerRadius * (Mathf.Cos((spreadDegrees * (i + 1) + centerRotation) * Mathf.Deg2Rad)), centerRadius * (Mathf.Sin((spreadDegrees * (i + 1) + centerRotation) * Mathf.Deg2Rad)));
            pos.x += spreadX;
            pos.y += (i - numberOfPoints / 2f) *  spreadY + spreadY/2;

            if (uniDirectionalPitch)
            {
                rotation = Quaternion.Euler(0, 0, pitch + spreadDegrees * (i + 1) + centerRotation);
            } 
            else if (playerHoming)
            {
                Vector3 difference = player.transform.position - transform.position;
                difference.Normalize();
                float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                rotation = Quaternion.Euler(0, 0, rotation_z);
            }
            else
            {
                rotation = Quaternion.Euler(0, 0, pitch);
            }

            GameObject obj = projectilePool.GetComponent<ObjectPool>().GetPooledObject();
            if (obj == null) return;

            obj.transform.position = pos;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
        }
    }
    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            pos = (Vector2)transform.position + new Vector2(centerRadius * (Mathf.Cos((spreadDegrees * (i + 1) + centerRotation) * Mathf.Deg2Rad)), centerRadius * (Mathf.Sin((spreadDegrees * (i + 1) + centerRotation) * Mathf.Deg2Rad)));
            pos.x += spreadX;
            pos.y += (i - numberOfPoints / 2f) * spreadY + spreadY / 2;

            if (uniDirectionalPitch)
            {
                rotation = Quaternion.Euler(0, 0, pitch + spreadDegrees * (i + 1) + centerRotation);
            }
            else
            {
                rotation = Quaternion.Euler(0, 0, pitch);
            }

            Gizmos.matrix = Matrix4x4.TRS(new Vector2(pos.x, pos.y), rotation, transform.lossyScale);
            Gizmos.DrawWireCube(Vector3.zero, Vector2.one);
        }
    }
}
