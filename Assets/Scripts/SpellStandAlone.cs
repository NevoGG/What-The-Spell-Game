using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellStandAlone : MonoBehaviour
{
    
    //
    private bool isSpellReady;
    [SerializeField] ParticleSystem appearParticles;

    private const float timeTilSummon = 2f;
    private float timeLeft = timeTilSummon;
    private Animator animator;
    
    // Start is called before the first frame update
    private float randomDir;
    private Vector2 randForce;
    private string initTag;
    [SerializeField] private float movementForce = 4f;
    void Start()
    {
        animator = GetComponent<Animator>();
        initTag = tag;
        tag = "IgnorePlayer";
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsAppearing", isSpellReady);
        if (timeLeft < 0) InitSpell();
        else timeLeft -= Time.deltaTime;
    }

    private void InitSpell()
    {
        tag = initTag;
        isSpellReady = true;
        appearParticles.Stop();
        int choice = Random.Range(0, 2);
        if (choice < 1) randomDir = -1;
        else randomDir = 1;
        randForce = Vector2.right * randomDir * movementForce;
        GetComponent<Rigidbody2D>().AddForce(randForce);
    }
}
