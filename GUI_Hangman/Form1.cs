using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace GUI_Hangman
{
    public delegate void CheckLetter(string letter);

    public partial class Form1 : Form
    {
        const int userLives = 5;
        event CheckLetter ChkLtr;

        string input;
        string missedLetters = "";
        string wordToFind = "";

        string wordToDisplay = "";

        char[] wordToFindArray;
        int[] wordToFindLettersPosition;
        bool IsLetterFound = false;

        Random rndm = new Random(0);

        List<string> wordList = new List<string>();
        List<int> wordsIndexAlreadyPlayed = new List<int>();

        int missedLetterCount = 0;

        public Form1()
        {
            InitializeComponent();
            this.ChkLtr += new CheckLetter(Form1_ChkLtr);
            CreateWordList();
            RestartTheGame();
        }

        private void CreateWordList()
        {
            // All 64 animals word list
            wordList.Add("Ant");
            wordList.Add("Baboon");
            wordList.Add("Badger");
            wordList.Add("Bat");
            wordList.Add("Bear");
            wordList.Add("Beaver");
            wordList.Add("Camel");
            wordList.Add("Cat");
            wordList.Add("Clam");
            wordList.Add("Cobra");
            wordList.Add("Cougar");
            wordList.Add("Coyote");
            wordList.Add("Crow");
            wordList.Add("Deer");
            wordList.Add("Dog");
            wordList.Add("Donkey");
            wordList.Add("Duck");
            wordList.Add("Eagle");
            wordList.Add("Ferret");
            wordList.Add("Fox");
            wordList.Add("Frog");
            wordList.Add("Goat");
            wordList.Add("Goose");
            wordList.Add("Hawk");
            wordList.Add("Lion");
            wordList.Add("Lizard");
            wordList.Add("Llama");
            wordList.Add("Mole");
            wordList.Add("Monkey");
            wordList.Add("Moose");
            wordList.Add("Mouse");
            wordList.Add("Mule");
            wordList.Add("Newt");
            wordList.Add("Otter");
            wordList.Add("Owl");
            wordList.Add("Panda");
            wordList.Add("Parrot");
            wordList.Add("Pigeon");
            wordList.Add("Python");
            wordList.Add("Rabbit");
            wordList.Add("Ram");
            wordList.Add("Rat");
            wordList.Add("Raven");
            wordList.Add("Rhino");
            wordList.Add("Salmon");
            wordList.Add("Seal");
            wordList.Add("Shark");
            wordList.Add("Sheep");
            wordList.Add("Skunk");
            wordList.Add("Sloth");
            wordList.Add("Snake");
            wordList.Add("Spider");
            wordList.Add("Stork");
            wordList.Add("Swan");
            wordList.Add("Tiger");
            wordList.Add("Toad");
            wordList.Add("Trout");
            wordList.Add("Turkey");
            wordList.Add("Turtle");
            wordList.Add("Weasel");
            wordList.Add("Whale");
            wordList.Add("Wolf");
            wordList.Add("Wombat");
            wordList.Add("Zebra");
        }

        private string GetNewWordFromPool()
        {
            bool IsNewWord = false;
            //Default word
            string temp = "HANGMAN";

            try
            {
                while (IsNewWord == false)
                {
                    
                    int index = rndm.Next(); //To get word randomly 
                    index = index % wordList.Count;

                    if (!wordsIndexAlreadyPlayed.Exists(e => e == index))
                    {
                        temp = wordList[index];
                        wordsIndexAlreadyPlayed.Add(index);
                        IsNewWord = true;
                        break;
                    }
                    else
                    {
                        IsNewWord = false;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return temp.ToUpper();
        }

        private void RestartTheGame()
        {
            try
            {
                wordToFind = GetNewWordFromPool();
                wordToFind = wordToFind.ToUpper();
                wordToFindArray = wordToFind.ToCharArray();

                wordToFindLettersPosition = new int[wordToFind.Length];

                //Resetting other counters and variables
                input = "";
                wordToDisplay = "";
                for (int i = 0; i < wordToFind.Length; i++)
                {
                    wordToDisplay += " - ";
                }

                missedLetters = "";
                missedLetterCount = 0;

                hword.Text = wordToDisplay.ToUpper();
                label5.Text = missedLetters;
                lives.Text = userLives.ToString();
                Application.DoEvents();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        //Event handler
        private void Form1_ChkLtr(string currentInputletter)
        {
            try
            {
                if (missedLetterCount < userLives)
                {
                    IsLetterFound = false;
                    for (int i = 0; i < wordToFindArray.Length; i++)
                    {
                        if (currentInputletter[0] == wordToFindArray[i])
                        {
                            wordToFindLettersPosition[i] = 1;
                            IsLetterFound = true;
                        }
                    }

                    if (IsLetterFound == false)
                    {
                        missedLetters += currentInputletter + ", ";
                        missedLetterCount++;
                        lives.Text = (userLives - missedLetterCount).ToString();
                    }

                    wordToDisplay = "";
                    for (int i = 0; i < wordToFindArray.Length; i++)
                    {
                        if (wordToFindLettersPosition[i] == 1)
                        {
                            wordToDisplay += wordToFindArray[i].ToString();
                        }
                        else
                        {
                            wordToDisplay += " - ";
                        }
                    }

                    hword.Text = wordToDisplay.ToUpper();
                    label5.Text = missedLetters;

                    if (wordToFindLettersPosition.All(e => e == 1))
                    {
                        MessageBox.Show("You Guessed Right.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Sorry, You Lost the Game!" + "\nThe Correct Word was: " + wordToFind, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Application.DoEvents();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btcA_Click(object sender, EventArgs e)
        {
            input = "A";
            ChkLtr(input);
        }

        private void btcB_Click(object sender, EventArgs e)
        {
            input = "B";
            ChkLtr(input);
        }

        private void btcC_Click(object sender, EventArgs e)
        {
            input = "C";
            ChkLtr(input);
        }

        private void btcD_Click(object sender, EventArgs e)
        {
            input = "D";
            ChkLtr(input);
        }

        private void btcE_Click(object sender, EventArgs e)
        {
            input = "E";
            ChkLtr(input);
        }

        private void btcF_Click(object sender, EventArgs e)
        {
            input = "F";
            ChkLtr(input);
        }

        private void btcG_Click(object sender, EventArgs e)
        {
            input = "G";
            ChkLtr(input);
        }

        private void btcH_Click(object sender, EventArgs e)
        {
            input = "H";
            ChkLtr(input);
        }

        private void btcI_Click(object sender, EventArgs e)
        {
            input = "I";
            ChkLtr(input);
        }

        private void btcJ_Click(object sender, EventArgs e)
        {
            input = "J";
            ChkLtr(input);
        }

        private void btcK_Click(object sender, EventArgs e)
        {
            input = "K";
            ChkLtr(input);
        }

        private void btcL_Click(object sender, EventArgs e)
        {
            input = "L";
            ChkLtr(input);
        }

        private void btcM_Click(object sender, EventArgs e)
        {
            input = "M";
            ChkLtr(input);
        }

        private void btcN_Click(object sender, EventArgs e)
        {
            input = "N";
            ChkLtr(input);
        }

        private void btcO_Click(object sender, EventArgs e)
        {
            input = "O";
            ChkLtr(input);
        }

        private void btcP_Click(object sender, EventArgs e)
        {
            input = "P";
            ChkLtr(input);
        }

        private void btcQ_Click(object sender, EventArgs e)
        {
            input = "Q";
            ChkLtr(input);
        }

        private void btcR_Click(object sender, EventArgs e)
        {
            input = "R";
            ChkLtr(input);
        }

        private void btcS_Click(object sender, EventArgs e)
        {
            input = "S";
            ChkLtr(input);
        }

        private void btcT_Click(object sender, EventArgs e)
        {
            input = "T";
            ChkLtr(input);
        }

        private void btcU_Click(object sender, EventArgs e)
        {
            input = "U";
            ChkLtr(input);
        }

        private void btcV_Click(object sender, EventArgs e)
        {
            input = "V";
            ChkLtr(input);
        }

        private void btcW_Click(object sender, EventArgs e)
        {
            input = "W";
            ChkLtr(input);
        }

        private void btcX_Click(object sender, EventArgs e)
        {
            input = "X";
            ChkLtr(input);
        }

        private void btcY_Click(object sender, EventArgs e)
        {
            input = "Y";
            ChkLtr(input);
        }

        private void btcZ_Click(object sender, EventArgs e)
        {
            input = "Z";
            ChkLtr(input);
        }

        private void btcRestart_Click(object sender, EventArgs e)
        {
            RestartTheGame();
        }

        private void btcExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}