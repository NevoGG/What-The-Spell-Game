using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource changeAnimalGrow;
    [SerializeField] private AudioSource changeAnimalShrink;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource oneTwoThreeGo;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource win2;
    [SerializeField] private AudioSource outOfScreen;
    [SerializeField] private AudioSource loosingClaps;
    [SerializeField] private AudioSource buttonPress;
    [SerializeField] private AudioSource dash;
    [SerializeField] private AudioSource playerConnect1;
    [SerializeField] private AudioSource playerConnect2;
    [SerializeField] private AudioSource playerConnect3;
    [SerializeField] private AudioSource playerConnect4;


    public void playChangeAnimalGrow()
    {
        changeAnimalGrow.Play();
    }
    public void playLoosingClaps()
    {
        loosingClaps.Play();
    }
    public void playChangeAnimalShrink()
    {
        changeAnimalShrink.Play();
    }
    public void playJump()
    {
        jump.Play();
    }
    public void playOneTwoThreeGo()
    {
        oneTwoThreeGo.Play();
    }
    public void playWin()
    {
        win.Play();
    }
    public void playWin2()
    {
        win2.Play();
    }
    public void playOutOfScreen()
    {
        outOfScreen.Play();
    }
    public void playButtonPress()
    {
        buttonPress.Play();
    }
    public void playDash()
    {
        dash.Play();
    }
    public void playPlayerConnect1()
    {
        playerConnect1.Play();
    }
    public void playPlayerConnect2()
    {
        playerConnect2.Play();
    }
    public void playPlayerConnect3()
    {
        playerConnect3.Play();
    }
    public void playPlayerConnect4()
    {
        playerConnect4.Play();
    }

}
