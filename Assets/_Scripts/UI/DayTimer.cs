using UnityEngine;
using UnityEngine.UI;

public class DayTimer : MonoBehaviour
{   //this script is the countdown timer for rounds in the game. the player has until the timer ends to complete their tasks.

    //variables
    [SerializeField] float timeLeft; 
    [SerializeField] bool timerOn = false;

    [SerializeField] Text timerText;


    private void Start()
    {   //start timer at the begining of the level
        timerOn = true;
    }


    private void Update()
    {
        TimerCheck();    
      
    }
    public void TimerCheck()//this method checks to see if the timer is on, then starts to countdown. 
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                Debug.Log("Time is up");//place here what will happen after time is up.
                timeLeft = 0;
                timerOn = false;
            }


        }
    }

    void UpdateTimer(float currentTime)//This method sets up the string format for the timer as well as creating the minutes and seconds
    {
        currentTime += 1; 

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);


        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

    }




}
