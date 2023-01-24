using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource buttonPress;
    [SerializeField] private float waitTime = 19f;
    private bool active;
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.Return))
        {
            
            buttonPress.Play();
            ActivateChoosePlayerScene();
        }
        if (active)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                active = false;
                ActivateChoosePlayerScene();
            }
        }
    }
    private void ActivateChoosePlayerScene()
    {
        SceneManager.LoadScene("ChoosePlayer");
    }
}
