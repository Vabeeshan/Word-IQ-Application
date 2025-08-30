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
    public partial class ReverseSentence : Form
    {
        // 🔹 Holds the current question text retrieved from DB or passed to this form
        private string questionSentence = "";

        public ReverseSentence()
        {
            InitializeComponent();
        }
        // Constructor that accepts a sentence to transform
        public ReverseSentence(string questionSentence)
        {
            InitializeComponent(); // Initialize form controls
            this.questionSentence = questionSentence; // Store passed sentence in the variable

            // Transform the sentence using our algorithm (reverse + compress duplicates)
            string transformed = TransformSentence(questionSentence);

         
        }

        // 🔹 Function to reverse the sentence and compress consecutive duplicate letters
        private string TransformSentence(string sentence)
        {
            // If input sentence is empty, just return it
            if (string.IsNullOrEmpty(sentence))
                return sentence;

            // Step 1: Split the sentence into individual words
            string[] words = sentence.Split(' ');

            // Step 2: Push words onto a stack to reverse their order
            Stack<string> stack = new Stack<string>();
            foreach (string word in words)
            {
                stack.Push(word); // Add each word to the stack
            }

            // Step 3: Pop words from stack, compress duplicates, and store in a new list
            List<string> transformedWords = new List<string>();
            while (stack.Count > 0)
            {
                string word = stack.Pop(); // Remove the top word from stack
                string compressedWord = CompressDuplicates(word); // Compress consecutive duplicate letters
                transformedWords.Add(compressedWord); // Add processed word to the list
            }

            // Step 4: Join all transformed words back into a single sentence with spaces
            return string.Join(" ", transformedWords);
        }

        // 🔹 Function to compress consecutive duplicate letters in a word
        private string CompressDuplicates(string word)
        {
            // Return empty input as-is
            if (string.IsNullOrEmpty(word))
                return word;

            char[] chars = word.ToCharArray(); // Convert word into a char array
            string result = ""; // Initialize result string
            int i = 0; // Index for iterating through chars

            while (i < chars.Length)
            {
                char current = chars[i]; // Current character being checked
                int count = 1; // Count of consecutive duplicates

                // Count consecutive occurrences of the same character
                while (i + 1 < chars.Length && chars[i + 1] == current)
                {
                    count++; // Increment count if next char is the same
                    i++;     // Move to next character
                }

                // Append character + count if duplicates exist, otherwise just the character
                if (count > 1)
                    result += current + count.ToString(); // e.g., 'ee' → 'e2'
                else
                    result += current;

                i++; // Move to next character
            }

            return result; // Return the compressed word
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            // 1️⃣ Get the user's answer from the TextBox
            string userAnswer = tztAnagram.Text.Trim();

            // 2️⃣ Transform the original sentence to expected format
            string expectedAnswer = TransformSentence(questionSentence);

            // 3️⃣ Compare user input with expected answer (case-insensitive)
            if (userAnswer.Equals(expectedAnswer, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("✅ Correct! Well done.");
            }
            else
            {
                MessageBox.Show("❌ Wrong! \nExpected: " );
            }
        }
    }
}
