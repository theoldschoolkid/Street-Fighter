using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    private Image health_UI;

    [SerializeField]
    Text lvltext, victoryText,gameOver;
    [SerializeField]
    GameObject hitFxLoop;
    [SerializeField]
    Image hitIcon;

    public static HUD HUDinstance;

    void Awake() {
       health_UI = GameObject.FindWithTag(Tags.HEALTH_UI).GetComponent<Image>();    
    }

    private void Start()
    {
        if (HUDinstance == null)
            HUDinstance = this;
            DisplayLevel(1);   // Level 1 at start
    }

    public void DisplayHealth(float value) {

        value /= 100f;  // the fill amount is a slider ranging from 0 to 1, hence value is divided by 100

        if (value < 0f)
            value = 0f;

       health_UI.fillAmount = value;

    }

    public void DisplayLevel(int Level)
    {

        lvltext.enabled = true;
        lvltext.text = "Level  " + Level;
        Invoke("DisableLevel", 2f);             //Disable level dispay after 2 seconds

    }

     void DisableLevel()
    {
        lvltext.enabled = false;        //Dont disable the TextGameObject just disable Text script for this to work
    }

    public void Victory()
    {
        victoryText.enabled = true;
    }
    
    public void GameOver()
    {
        print("InsideGameover");
        gameOver.enabled = true;
    }

    public void GroudPunch(bool Activate)
    {
        if (Activate)
        {
            hitFxLoop.SetActive(true);                   //For GameObject setActive
            hitIcon.enabled = true;                     //For GO components .enabled
            AudioManager.Play(AudioName.Powerup);
        }
        else if(!Activate)
        {
            hitFxLoop.SetActive(false);                   //For GameObject setActive
            hitIcon.enabled = false;
        }
    }



} // class

































