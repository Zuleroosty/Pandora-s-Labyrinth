using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectZRelation : MonoBehaviour
{
    public GameObject collidingObject, thisSprite, thisCollider, enemyParent;
    public int childID, childMax, targetColour;
    public bool disableFade;
    Color thisColour;

    // Start is called before the first frame update
    void Start()
    {
        thisColour = thisSprite.GetComponent<SpriteRenderer>().color;
        enemyParent = GameObject.Find("----EnemyParent----");
        thisSprite.transform.localPosition = new Vector3(thisSprite.transform.localPosition.x, thisSprite.transform.localPosition.y, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find(">GameManager<").GetComponent<GameManager>().gameState == GameManager.state.InGame)
        {
            CollisionTest();
            targetColour = 1;

            // ADJUST TO PLAYER POSITION
            if (GameObject.Find("ZCollision").GetComponent<SpriteRenderer>().bounds.Intersects(thisCollider.GetComponent<SpriteRenderer>().bounds))
            {
                GameObject.Find("----PlayerObjectParent----").GetComponent<ZMemory>().collidingObject = this.gameObject;
                if (GameObject.Find("----PlayerObjectParent----").GetComponent<ZMemory>().thisSprite.GetComponent<SpriteRenderer>().bounds.min.y > thisSprite.GetComponent<SpriteRenderer>().bounds.min.y) targetColour = 0;
            }
            if (collidingObject != null && collidingObject.GetComponent<ZMemory>().thisCollider.GetComponent<SpriteRenderer>().bounds.Intersects(thisCollider.GetComponent<SpriteRenderer>().bounds))
            {
                collidingObject.GetComponent<ZMemory>().collidingObject = this.gameObject;
                if (collidingObject.GetComponent<ZMemory>().thisSprite.GetComponent<SpriteRenderer>().bounds.min.y > thisSprite.GetComponent<SpriteRenderer>().bounds.min.y) targetColour = 0;
            }

            switch (targetColour)
            {
                case 0:
                    if (thisColour.a > 0.4f) thisColour.a -= 0.025f;
                    break;
                case 1:
                    if (thisColour.a < 1f) thisColour.a += 0.025f;
                    break;
            }
            if (disableFade) thisColour.a = 1;

            thisSprite.GetComponent<SpriteRenderer>().color = thisColour;
        }
    }

    void CollisionTest()
    {
        if (childID < childMax && childMax == enemyParent.transform.childCount)
        {
            collidingObject = enemyParent.transform.GetChild(childID).gameObject;
            if (!collidingObject.GetComponent<ZMemory>().thisCollider.GetComponent<SpriteRenderer>().bounds.Intersects(thisCollider.GetComponent<SpriteRenderer>().bounds)) collidingObject = null;
            else if (collidingObject.GetComponent<ZMemory>().collidingObject == null) collidingObject.GetComponent<ZMemory>().collidingObject = this.gameObject;
        }
        else
        {
            childID = -1;
            childMax = enemyParent.transform.childCount;
        }
        childID++;
    }
}
