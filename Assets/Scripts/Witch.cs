using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    [SerializeField] private GameObject camera;
    
    private Animator animator;
    private Vector3 screenMoveDir; //to arrange witches accordingly
    private int playGroundWidth; // to arrange witches just outside play area
    private Vector3 randomSpotVec; //random spot to get to.
    private bool gotToDest; //checks if got to random spot
    private float positionX;
    private float positionZ;
    private float topBounderReal;
    private float bottomBounderReal;

    
    [SerializeField] private GameObject growSpell;
    [SerializeField] private GameObject shrinkSpell;
    [SerializeField] private int facingRotation;
    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private float projectileSpread;
    public bool _isGrow = false;
    public bool _isShrink = false;
    private GameObject spell;


        // Setters- todo: implement.
    // public void SetWitchMovementSpeed( int speed){}
    // public void SetWitchShotPerMinute(int shotPerMinute){}
    // public void SetWitchGrowToShrinkRatio(float ratio){}
    // public void SetScreenMoveDir(Vector3 vec){}
    // public void SetPlaygroundWidth(int width){}

    // appear and disappear
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        positionX = transform.localPosition.x;
        positionZ = transform.localPosition.z;
        UpdateRandomDest();
        gotToDest = false;
    }

    void UpdateBounderies()
    {
        topBounderReal = camera.transform.position.y + topBounder/2;
        bottomBounderReal = camera.transform.position.y + bottomBounder/2;
    }
    // Update is called once per frame
    void Update()
    {
        //UpdateBounderies();
        animator.SetBool("IsGrow", _isGrow);
        animator.SetBool("IsShrink", _isShrink);
        if (!GameManager.gameEnded && GameManager.countDownFinish)
        {
            CustomUpdate();
        }
    }


    private void CustomUpdate()
    {
        if (Vector3.Distance(transform.localPosition, randomSpotVec) < distanceWitchFromDestPar)
        {
            //print("current: " + transform.position.y);
            gotToDest = true;
            CastSpell();
            UpdateRandomDest();
            StartCoroutine(DelayAfterAnimation());
        }
        else if (!gotToDest)
        {
            if (randomSpotVec.y - transform.localPosition.y > 0)
            {
                transform.localPosition += new Vector3(0, witchMovementSpeed * Time.deltaTime, 0);
            }
            else
            {
                transform.localPosition -= new Vector3(0, witchMovementSpeed * Time.deltaTime, 0);
            }
        }
    }

    private void UpdateRandomDest()
    {
        randomSpotVec = new Vector3(positionX, Random.Range(topBounder, bottomBounder), positionZ);
        //print("to get to: " + randomSpotVec.y);
        int choice = Random.Range(0, 2);
        _isGrow = choice == 0 ? true : false;
        _isShrink = choice == 1 ? true : false;
        spell = choice == 0 ? growSpell : shrinkSpell;
    }

    private void CastSpell()
    {
        //todo: animation and spell
        // for (int i = 0; i < 3; i++)
        // {
        //     int choice = Random.Range(0, 2);
        //     if (choice == 0)
        //     {
        //         Instantiate(shrinkSpell);
        //         shrinkSpell.transform.rotation 
        //     }
        // }
        // int choice = Random.Range(0, 2);
        // _isGrow = choice == 0 ? true : false;
        // _isShrink = choice == 1 ? true : false;
        
        
        // spell = choice == 0 ? growSpell : shrinkSpell;
        float startRotation = facingRotation*(+projectileSpread / 2f);
        float angleIcrease = projectileSpread / ((float)numberOfProjectiles - 1f);
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float tempRot = startRotation + angleIcrease * i;
            GameObject newSpell = Instantiate(spell, transform.position, Quaternion.Euler(0f, 0f, tempRot));
            // newSpell= 
        }
    }
    
    IEnumerator DelayAfterAnimation()
    {
        yield return new WaitForSeconds(spellingTime);
        gotToDest = false;
    }
}
