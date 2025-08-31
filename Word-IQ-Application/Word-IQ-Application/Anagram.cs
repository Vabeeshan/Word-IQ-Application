using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Word_IQ_Application
{
    public partial class Anagram : Form
    {
        private int countdown = 10;
        private Timer questionTimer;

        //stored word in an array named words
        string[] words = { "listen", "silent", "earth", "heart", "stone", "tones", "night", "thing", "male", "meal", "lemon" ,
            "melon", "care", "race", "loop", "pool"};

        //method to select random words
        Random rand = new Random();
        string currentWord = "";
        HashSet<string> dictionary;

        public Anagram()
        {
            InitializeComponent();
        }

        private void Anagram_Load(object sender, EventArgs e)
        {
            // Show player info
            txtName.Text = GameSession.PlayerName;
            txtScore.Text = GameSession.Score.ToString();

            // ❌ Prevent Designer from trying to load dictionary.txt
            if (!this.DesignMode)
            {
                try
                {
                    // Load dictionary only at runtime
                    dictionary = new HashSet<string>(System.IO.File.ReadAllLines("dictionary.txt"));

                    // Pick a random word for the question
                    LoadRandomWord();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading dictionary: " + ex.Message);
                    dictionary = new HashSet<string>(); // fallback empty dictionary
                }
            }
            StartQuestionTimer();
        }


        //method to load random words
        private void LoadRandomWord()
        {
            currentWord = words[rand.Next(words.Length)];
            lblAnagram.Text = currentWord;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string userInput = tztAnagram.Text.ToLower();

            // calling the methods to check if the answer is correct
            /*it checks if the entered word has all the characters and also
             it check if the entered word is in dictionary.txt*/
            if (IsAnagram(currentWord, userInput) && IsDictionaryWord(userInput))
            {
                questionTimer.Stop();
                MessageBox.Show("Your answer is correct!");
                if (countdown<0)
                {
                    GameSession.Score -= countdown;
                }
                else
                {
                    GameSession.Score += (10 + countdown);
                }

                ReverseSentenceQuestion rq = new ReverseSentenceQuestion();
                rq.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Entered answer is wrong!");
                if( GameSession.wrongAttempts >2)
                {
                    GameSession.wrongAttempts++;
                    GameSession.Score -=  countdown;
                   
                }
                ReverseSentenceQuestion rq = new ReverseSentenceQuestion();
                rq.Show();
                this.Hide();
            }
        }

        //method to  check whether the word is anagram (1st condition)
        private bool IsAnagram(string word1, string word2)
        {
            if (string.IsNullOrEmpty(word2) || word1.Length != word2.Length)
                return false;

            char[] arr1 = word1.ToCharArray();
            char[] arr2 = word2.ToCharArray();
            Array.Sort(arr1);
            Array.Sort(arr2);

            return new string(arr1) == new string(arr2);
        }

        //method to check whether the word has a meaning(2nd condition)
        private bool IsDictionaryWord(string word)
        {
            return dictionary.Contains(word);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // For now it only closes the form. later we'll code it to go to the next game
        }

        private void StartQuestionTimer()
        {
          
            lblTimer.Text = countdown.ToString();

            questionTimer = new Timer();
            questionTimer.Interval = 1000; // 1000 ms = 1 second
            questionTimer.Tick += timer1_Tick;
            questionTimer.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            countdown--; // decrease by 1 each second
            lblTimer.Text = countdown.ToString();

        }
    }
}
