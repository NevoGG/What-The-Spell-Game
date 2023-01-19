using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellStandAlone : MonoBehaviour
{
    
    //
    private bool isSpellReady;
    [SerializeField] ParticleSystem appearParticles;
    [SerializeField] private ParticleSystem moveParticles;

    private const float timeTilSummon = 1.5f;
    private float timeLeft = timeTilSummon;
    private Animator animator;
    private Vector3 initLocation;
    
    // Start is called before the first frame update
    private float randomDir;
    private Vector2 randForce;
    private string initTag;
    [SerializeField] private float movementForce = 4f;
    void Start()
    {
        initLocation = transform.localPosition;
        animator = GetComponent<Animator>();
        initTag = tag;
        tag = "IgnorePlayer";
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsAppearing", isSpellReady);
        if (!isSpellReady) transform.localPosition = initLocation;
        if (timeLeft < 0 && !isSpellReady) InitSpell();
        else timeLeft -= Time.deltaTime;
    }

    private void InitSpell()
    {
        moveParticles.Play();
        tag = initTag;
        isSpellReady = true;
        appearParticles.Stop();
        int choice = Random.Range(0, 2);
        if (choice < 1) randomDir = -1;
        else randomDir = 1;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        randForce = Vector2.right * randomDir * movementForce;
        GetComponent<Rigidbody2D>().AddForce(randForce);
    }
}
