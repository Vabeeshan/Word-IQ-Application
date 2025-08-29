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
                // Load dictionary file once
                dictionary = new HashSet<string>(System.IO.File.ReadAllLines("dictionary.txt"));
                //calling the method to load random words
                LoadRandomWord();

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
                MessageBox.Show("Your answer is correct!");
            }
            else
            {
                MessageBox.Show("Entered answer is wrong!");
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
    }
}
