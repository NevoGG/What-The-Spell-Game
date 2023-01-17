using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] private GameObject growSpell;
    [SerializeField] private GameObject shrinkSpell;
    [SerializeField] private float _timePerSpell = 10; //in seconds
    [SerializeField] private Vector2 conjureRate = new Vector2(1, 200); //summon between x and y seconds
    private float _timeTilConjure;
    private float _timeTilVanish;
    private Camera _camera;

    private bool _conjureMode = false;
    private bool _isActiveSpell = false;
    private bool _isConjuring = false;

    private GameObject spell;
    private GameObject curSpell = null;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isConjuring)
        {
            _timeTilConjure -= Time.deltaTime;
            if (_timeTilConjure < 0) Conjure();
            return;
        }
        
        if (_isActiveSpell)
        {
            _timeTilVanish -= Time.deltaTime;
            if (_timeTilVanish < 0) DestroyConjure();
            return;
        }
        Vector3 screenPoint = _camera.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (onScreen)
        {
            _timeTilConjure = Random.Range(1, 10);
            _isConjuring = true;
        }
    }

    private void DestroyConjure()
    {
        if(curSpell != null) Destroy(curSpell);
        _isActiveSpell = false;
    }

    private void Conjure()
    {
        int choice = Random.Range(0, 2);
        spell = choice == 0 ? growSpell : shrinkSpell;
        float colliderLen = GetColliderLen() * 2/3f;
        float xPos = Random.Range(-colliderLen / 2, colliderLen / 2);
        Vector3 spellPos = new Vector3(transform.position.x + xPos, transform.position.y + 3f, transform.position.z);
        curSpell = Instantiate(spell, spellPos ,transform.rotation);
        _isActiveSpell = true;
        _isConjuring = false;
        _timeTilVanish = _timePerSpell;
    }

    private float GetColliderLen()
    {
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
            float length = 0;
            Vector2[] points = edgeCollider.points;
            for (int i = 0; i < points.Length - 1; i++)
            {
                length += Vector2.Distance(points[i], points[i + 1]);
            }
            return length;
        }
}
