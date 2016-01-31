using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{

    public GameManager gameManager;
    public Spawner spawner;
    public float ChaseSpeed = 5f;
    public float Range = 20f;
    public float Range2 = 2f;
    public float timePerAttack = 2f;
    public float currentTimeLeftInAttack;
    float CurrentSpeed;
    public bool isAttacking;
    public CharacterController characterBeingTargeted;

    void Start()
    {
        for(int i = 0; i < 20; i++)
        {
            if(!spawner.enemies[i])
            {
                spawner.enemies[i] = gameObject;
            }
        }
    }
    void Awake()
    {
        currentTimeLeftInAttack = timePerAttack;
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            for (int i = 0; i < 4; i++)
            {
                if (gameManager.Characters[i] != characterBeingTargeted)
                {
                    if (Vector2.Distance(transform.position, gameManager.Characters[i].transform.position) < Vector2.Distance(transform.position, characterBeingTargeted.transform.position))
                    {
                        characterBeingTargeted = gameManager.Characters[i];
                    }
                }

            }
            if (Vector2.Distance(transform.position, characterBeingTargeted.transform.position) <= Range && Vector2.Distance(transform.position, characterBeingTargeted.transform.position) > Range2)
            {
                CurrentSpeed = ChaseSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, characterBeingTargeted.transform.position, CurrentSpeed);
            }
            else if (Vector2.Distance(transform.position, characterBeingTargeted.transform.position) <= Range2 && !isAttacking)
            {
                isAttacking = true;
                //play animation here
            }
        }

        else
        {
            currentTimeLeftInAttack = currentTimeLeftInAttack - Time.fixedDeltaTime;
            if (currentTimeLeftInAttack <= 0)
            {
                isAttacking = false;
                currentTimeLeftInAttack = timePerAttack;
            }
        }

    }

    void OnDestroy()
    {
        spawner.currentEnemiesAlive--;
    }
}
