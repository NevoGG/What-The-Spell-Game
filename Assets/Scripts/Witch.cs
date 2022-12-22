using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    //behavior parameters:
    private int witchMovementSpeed;
    private int witchShotPerMinute;
    private float witchGrowToShrinkRatio;
    private Vector3 screenMoveDir; //to arrange witches accordingly
    private int playGroundWidth; // to arrange witches just outside play area
    
    // Setters- todo: implement.
    public void SetWitchMovementSpeed( int speed){}
    public void SetWitchShotPerMinute(int shotPerMinute){}
    public void SetWitchGrowToShrinkRatio(float ratio){}
    public void SetScreenMoveDir(Vector3 vec){}
    public void SetPlaygroundWidth(int width){}

    [SerializeField] private Spell shrinkSpell;
    [SerializeField] private Spell growSpell; // appear and disappear
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
