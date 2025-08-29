using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Word_IQ_Application
{
    public partial class Palindrome : Form
    {
        // Word bank (can later replace with DB words) - moved inside the class
        private string[] words = { "madam", "level", "world", "racecar", "hello", "noon", "rotor" };
        private Random rand = new Random();
        private int currentIndex = -1; // keep track of current word index

        public Palindrome()
        {
            InitializeComponent();
        }

        // Function to get random word from array
        private string GetRandomWord()
        {
            currentIndex = rand.Next(words.Length);
            return words[currentIndex];
        }

        // Function to check palindrome using Stack + Queue
        private bool IsPalindrome(string word)
        {
            Stack<char> stack = new Stack<char>();
            Queue<char> queue = new Queue<char>();

            // Add characters to stack and queue
            foreach (char c in word.ToLower())
            {
                if (char.IsLetter(c)) // ignore spaces/punctuation
                {
                    stack.Push(c);
                    queue.Enqueue(c);
                }
            }

            // Compare stack (LIFO) with queue (FIFO)
            while (stack.Count > 0)
            {
                if (stack.Pop() != queue.Dequeue())
                    return false; // mismatch → not palindrome
            }
            return true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Palindrome_Load(object sender, EventArgs e)
        {
            lblWord.Text = GetRandomWord();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string word = lblWord.Text;
            bool actual = IsPalindrome(word); // real result
            bool userAnswer = rbYes.Checked;  // true if "Yes" selected

            // Compare user's choice with actual result
            if ((actual && userAnswer) || (!actual && rbNo.Checked))
            {
                MessageBox.Show("✅ Correct!");
            }
            else
            {
                MessageBox.Show($"❌ Wrong! The correct answer is {(actual ? "Palindrome" : "Not a Palindrome")}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblWord.Text = GetRandomWord();
            rbYes.Checked = false; // reset radio buttons
            rbNo.Checked = false;
        }
    }
}