using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    //behavior parameters:
    [SerializeField] private float witchMovementSpeed = 0.01f;
    [SerializeField] private int timeBetweenShots = 2;
    [SerializeField] private float witchGrowToShrinkRatio;

    [SerializeField] private float topBounder = 5f;
    [SerializeField] private float bottomBounder = -5f;
    [SerializeField] private float spellingTime = 2f;
    [SerializeField] private float distanceWitchFromDestPar = 0.1f;
    
    private Vector3 screenMoveDir; //to arrange witches accordingly
    private int playGroundWidth; // to arrange witches just outside play area
    private Vector3 randomSpotVec; //random spot to get to.
    private bool gotToDest; //checks if got to random spot
    private float positionX;
    private float positionZ;
    
    // Setters- todo: implement.
    // public void SetWitchMovementSpeed( int speed){}
    // public void SetWitchShotPerMinute(int shotPerMinute){}
    // public void SetWitchGrowToShrinkRatio(float ratio){}
    // public void SetScreenMoveDir(Vector3 vec){}
    // public void SetPlaygroundWidth(int width){}

    [SerializeField] private Spell shrinkSpell;
    [SerializeField] private Spell growSpell; // appear and disappear
    // Start is called before the first frame update
    void Start()
    {
        positionX = transform.position.x;
        positionZ = transform.position.z;
        UpdateRandomDest();
        gotToDest = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, randomSpotVec) < distanceWitchFromDestPar)
        {
            gotToDest = true;
            CastSpell();
            UpdateRandomDest();
            StartCoroutine(DelayAfterAnimation());
        }
        else if (!gotToDest)
        {
            if (randomSpotVec.y - transform.position.y > 0)
            {
                transform.position += new Vector3(0, witchMovementSpeed * Time.deltaTime, 0);
            }
            else
            {
                transform.position -= new Vector3(0, witchMovementSpeed * Time.deltaTime, 0);
            }
        }
    }

    private void UpdateRandomDest()
    {
        randomSpotVec = new Vector3(positionX, Random.Range(topBounder, bottomBounder), positionZ);
    }

    private void CastSpell()
    {
        //todo: animation and spell
    }
    
    IEnumerator DelayAfterAnimation()
    {
        yield return new WaitForSeconds(spellingTime);
        gotToDest = false;
    }
}
