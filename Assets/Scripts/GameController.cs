using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameController : MonoBehaviour
{
    public TMP_Text timeField;
    public GameObject[] hangMan;
    public GameObject winText;
    public GameObject loseText;
    public TMP_Text wordToFindField;
    public GameObject replayButton;
    private float time;
    private string[] words = File.ReadAllLines(@"Assets/Words.txt");
    private string chosenWord;
    private string hiddenWord;
    private int fails;
    private bool gameEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < wordsLocal.Length; i++)
        //{
        //    Debug.Log(wordsLocal[i]);
        //}


        chosenWord = words[Random.Range(0, words.Length)];


        for (int i = 0; i < chosenWord.Length; i++)
        {
            char letter = chosenWord[i];
            if (char.IsWhiteSpace(letter))
            {
                hiddenWord += " ";
            }
            else
            {
                hiddenWord += "_";
            }
            
        }

        wordToFindField.text = hiddenWord;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd == false)
        {
            time += Time.deltaTime;
            timeField.text = string.Format("{0:0.00}", time);
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedLetter = e.keyCode.ToString();
            Debug.Log("Keydown even was triggered " + pressedLetter);

            if (chosenWord.Contains(pressedLetter))
            {
                int i = chosenWord.IndexOf(pressedLetter);
                while (i != -1)
                {
                    //Set ne hidden word to everything before the i,
                    //change the i to the letter pressed, and everthing after the i
                    hiddenWord = hiddenWord.Substring(0, i) + pressedLetter + hiddenWord.Substring(i + 1);
                    Debug.Log(hiddenWord);

                    chosenWord = chosenWord.Substring(0, i) + "_" + chosenWord.Substring(i + 1);
                    Debug.Log(chosenWord);

                    i = chosenWord.IndexOf(pressedLetter);
                }

                wordToFindField.text = hiddenWord;
                //check if won
                CheckWin();

            }
            // add a hangman body part
            else
            {
                hangMan[fails].SetActive(true);
                fails++;
                //case lost game
            if(fails == hangMan.Length)
            {
                loseText.SetActive(true);
                replayButton.SetActive(true);
                gameEnd = true;
            }
        }
        }
    }

    private void CheckWin()
    {
        if (!hiddenWord.Contains("_"))
        {
            winText.SetActive(true);
            replayButton.SetActive(true);
            gameEnd = true;
        }
    }
}
