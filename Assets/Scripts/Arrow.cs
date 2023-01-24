using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject toFollow;
    private Camera _camera;
    private Animator _animator;

    [SerializeField] private AudioSource outOfScreen;
    [SerializeField] private AudioSource loosingClaps;
    [SerializeField] private GameObject _boom;
    private Animator _boomAnimator;
    private float upY;
    private float downY;
    private bool isFlipped = false;
    private float fallThreshold = 0.3f;
    private Vector2 initScale;
    private float _mushroomScale = 2;

    private float _distFromEdge = 14f;

    private static readonly int DeadUp = Animator.StringToHash("DeadUp");
    private static readonly int IsOutOfScreen = Animator.StringToHash("IsOutOfScreen");
    private static readonly int DeadDown = Animator.StringToHash("DeadDown");

    // Start is called before the first frame update
    void Start()
    {
        //flip boom: 
        Vector3 ang = _boom.transform.localEulerAngles;
        _boom.transform.localEulerAngles = new Vector3(ang.x, ang.y, ang.z + 180);
        _boom.transform.localScale *= 2;
        
        _boomAnimator = _boom.GetComponent<Animator>();
        initScale = transform.localScale;
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
        upY =  _distFromEdge;
        downY = CameraMovment.cameraWidth - _distFromEdge;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = _camera.WorldToViewportPoint(toFollow.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (!onScreen)
        {
            _animator.SetBool(IsOutOfScreen, true);
            if (screenPoint.y > 1)
             {
                 if(isFlipped) Flip();
                 transform.localPosition = new Vector3(toFollow.transform.position.x, upY, screenPoint.z);
                 _animator.SetBool(IsOutOfScreen, true);
                 transform.localScale = initScale * (1 / (screenPoint.y * 1.5f));
                 OutOfRange();
                 if (screenPoint.y > 1 + fallThreshold)
                 {
                     _animator.SetTrigger(DeadUp);
                     // toFollow.GetComponent<Animal>().HasLost();
                     PlayerDead();
                 }  
             }
             
             if (screenPoint.y < 0)
             {
                 if(!isFlipped) Flip();
                 transform.localPosition = new Vector3(toFollow.transform.position.x, -upY, screenPoint.z);
                 _animator.SetBool(IsOutOfScreen, true);
                 transform.localScale = initScale * (1 / (1 + (screenPoint.y * -1) * 1.5f));
                 OutOfRange();
                 if (screenPoint.y < 0 - fallThreshold)
                 {
                     // Flip();
                     _boom.transform.localPosition = new Vector3(0f, -3f, screenPoint.z);
                     _animator.SetTrigger(DeadDown);
                     _boomAnimator.SetTrigger(DeadDown);
                     PlayerDead();
                    // toFollow.GetComponent<Animal>().HasLost();
                 }
             }
        }
        else
        {
            _animator.SetBool(IsOutOfScreen, false);
            transform.localScale = initScale;
        }
    }

    public void SetFollowing(GameObject g)
    {
        toFollow = g;
        
    }
    private void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 ang = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(ang.x, ang.y, ang.z + 180);
    }

    private void OutOfRange()
    {
        //todo: Fede Kapara
        // outOfScreen.Play();
    }
    
    
    private void PlayerDead()
    {
        //todo: Fede Kapara
        // loosingClaps.Play();
    }
}
