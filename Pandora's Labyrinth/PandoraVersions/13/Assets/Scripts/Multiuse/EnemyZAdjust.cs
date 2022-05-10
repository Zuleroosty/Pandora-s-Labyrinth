using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZAdjust : MonoBehaviour
{
    public GameObject enemyParent, collidingEnemy, collisionObject;
    int childID, childMax;

    // Start is called before the first frame update
    void Start()
    {
        enemyParent = GameObject.Find("----EnemyParent----");
    }

    // Update is called once per frame
    void Update()
    {
        CollisionTest(); // REPEATED FOR ACCURACY
        CollisionTest();
        CollisionTest();
    }
    void CollisionTest()
    {
        if (childID < childMax && childMax == enemyParent.transform.childCount)
        {
            collidingEnemy = enemyParent.transform.GetChild(childID).gameObject;
            if (collidingEnemy.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.Intersects(GetComponent<SpriteRenderer>().bounds))
            {
                if (collidingEnemy.transform.GetChild(0).transform.position.y > transform.position.y - 1)
                {
                    collidingEnemy.GetComponent<EnemyAI>().spriteObject.transform.position = new Vector3(collidingEnemy.GetComponent<EnemyAI>().spriteObject.transform.position.x, collidingEnemy.GetComponent<EnemyAI>().spriteObject.transform.position.y, -0.1f);
                }
                else collidingEnemy.GetComponent<EnemyAI>().spriteObject.transform.position = new Vector3(collidingEnemy.GetComponent<EnemyAI>().spriteObject.transform.position.x, collidingEnemy.GetComponent<EnemyAI>().spriteObject.transform.position.y, 0.1f);
            }
            //else collidingEnemy.GetComponent<EnemyAI>().spriteObject.transform.position = new Vector3(collidingEnemy.GetComponent<EnemyAI>().spriteObject.transform.position.x, collidingEnemy.GetComponent<EnemyAI>().spriteObject.transform.position.y, 0);
        }
        else
        {
            childID = -1;
            childMax = enemyParent.transform.childCount;
        }
        childID++;
    }
}
