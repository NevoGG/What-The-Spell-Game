using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject toFollow;
    private Camera _camera;
    private Animator _animator;
    private float upY;
    private float downY;
    private bool isFlipped = false;
    private float fallThreshold = 0.3f;
    private Vector2 initScale;

    private float _distFromEdge = 14f;
    // Start is called before the first frame update
    void Start()
    {
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
            _animator.SetBool("IsOutOfScreen", true);
            if (screenPoint.y > 1)
             {
                 if(isFlipped) Flip();
                 transform.localPosition = new Vector3(toFollow.transform.position.x, upY, screenPoint.z);
                 _animator.SetBool("IsOutOfScreen", true);
                 transform.localScale = initScale * (1 / (screenPoint.y * 1.5f));
                 if (screenPoint.y > 1 + fallThreshold)
                 {
                     _animator.SetTrigger("DeadUp");
                 }  
             }
             
             if (screenPoint.y < 0)
             {
                 if(!isFlipped) Flip();
                 transform.localPosition = new Vector3(toFollow.transform.position.x, -upY, screenPoint.z);
                 _animator.SetBool("IsOutOfScreen", true);
                 transform.localScale = initScale * (1 / (1 + (screenPoint.y * -1) * 1.5f));
                 if(screenPoint.y < 0 - fallThreshold){}
             }
        }
        else
        {
            _animator.SetBool("IsOutOfScreen", false);
            transform.localScale = initScale;
        }
    }

    private void Flip()
    {
        isFlipped = !isFlipped;
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * -1);
    }
}
