using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalParams : MonoBehaviour
{
    //Movement Fields:
    [SerializeField, Range(0f, 1000f)] public float _maxSpeed = 15f;
    [SerializeField, Range(0f, 300f)] public float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 1000f)] public float _maxAirAcceleration = 20f;
    [SerializeField, Range(0f, 10)] public float _downwardOnPressMultiplier = 2f;	
    
    //Jump Fields:
    [SerializeField, Range(0f, 100f)] public float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 100f)] public float _upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 1000f)] public float _jumpHeight = 100f;
    [SerializeField, Range(0f, 1f)] public float _multiJumpMultiplier = 0.75f;
    
    //linear drag:
    [SerializeField, Range(0f, 10f)] public float  _groundLinearDrag= 10f;
    [SerializeField, Range(0f, 10f)] public float  _upwardLinearDrag= 10f;
    [SerializeField, Range(0f, 10f)] public float  _downardLinearDrag= 10f;
    [SerializeField, Range(0f, 15f)] public float _size = 1f;
    
    //Power and xp:
    // [SerializeField] public AnimalPower _power;
    // [SerializeField, Range(0f, 10)] public int _xpNeeded;
}
