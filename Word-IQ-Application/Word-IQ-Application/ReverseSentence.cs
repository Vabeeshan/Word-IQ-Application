using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Word_IQ_Application
{
    public partial class ReverseSentence : Form
    {
        // 🔹 Holds the current question text retrieved from DB or passed to this form
        private string questionSentence = "";

        // 🔹 Holds the expected transformed answer
        private string expectedAnswer = "";

        // Default constructor
        public ReverseSentence()
        {
            InitializeComponent(); // Initialize form controls (buttons, labels, textboxes)
        }

        // Constructor that accepts a sentence to transform
        public ReverseSentence(string questionSentence)
        {
            InitializeComponent(); // Initialize form controls
            this.questionSentence = questionSentence; // Store passed sentence in the variable

            // Transform the sentence using our algorithm (reverse + compress duplicates)
            expectedAnswer = TransformSentence(questionSentence);

          
        }

        // 🔹 Function to reverse the sentence and compress consecutive duplicate letters
        private string TransformSentence(string sentence)
        {
            if (string.IsNullOrEmpty(sentence))
                return sentence;

            string[] words = sentence.Split(' '); // Step 1: Split sentence into words
            Stack<string> stack = new Stack<string>();

            foreach (string word in words)
                stack.Push(word); // Step 2: Push words to stack to reverse order

            List<string> transformedWords = new List<string>();
            while (stack.Count > 0)
            {
                string word = stack.Pop(); // Pop from stack
                string compressedWord = CompressDuplicates(word); // Compress duplicates
                transformedWords.Add(compressedWord); // Add to transformed list
            }

            return string.Join(" ", transformedWords); // Step 4: Join into final sentence
        }

        // 🔹 Function to compress consecutive duplicate letters in a word
        private string CompressDuplicates(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            char[] chars = word.ToCharArray();
            string result = "";
            int i = 0;

            while (i < chars.Length)
            {
                char current = chars[i];
                int count = 1;

                while (i + 1 < chars.Length && chars[i + 1] == current)
                {
                    count++;
                    i++;
                }

                result += (count > 1) ? current + count.ToString() : current.ToString();
                i++;
            }

            return result;
        }

        // 🔹 btnCheck click event to validate user input

        private void btnCheck_Click_1(object sender, EventArgs e)
        {
            string userAnswer = txtAnswer.Text.Trim(); // Get user input and trim spaces

            // Compare user input with expected answer (case-insensitive)
            if (userAnswer.Equals(expectedAnswer, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("✅ Correct! Well done.");
            }
            else
            {
                MessageBox.Show("❌ Wrong! \nExpected: " + expectedAnswer);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
