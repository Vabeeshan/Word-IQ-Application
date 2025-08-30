using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Word_IQ_Application
{
    public partial class ReverseSentenceQuestion : Form
    {
        // 🔹 Database connection details
        private string connectionString = "server=localhost;user=root;password=;database=wordiq;";

        // 🔹 Holds the current question text
        private string currentSentence = "";

        // 🔹 List to store all questions loaded from DB
        private List<string> allQuestions = new List<string>();

        // 🔹 Countdown value
        private int countdown = 5;

        public ReverseSentenceQuestion()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ReverseSentenceQuestion_Load(object sender, EventArgs e)
        {
            lbltimer.Text = "";
            // Step 1: Load all questions from DB into the list
            allQuestions = LoadAllQuestions();

            // Step 2: Pick one random question from the list
            currentSentence = GetRandomQuestion(allQuestions);

            // Step 3: Display the sentence on the label so user can see it
            lblSentence.Text =  currentSentence;

            StartQuestionTimer();
        }

        // 🔹 Function to fetch ALL sentences from database
        private List<string> LoadAllQuestions()
        {
            // Create an empty list to store questions
            List<string> questions = new List<string>();

            // Create DB connection using given connection string
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open(); // Open the connection

                // SQL query: select all sentences from ReverseQuestions table
                string query = "SELECT sentence FROM reversequestions;";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Execute the query → return all rows
                MySqlDataReader reader = cmd.ExecuteReader();

                // Read each row one by one
                while (reader.Read())
                {
                    // Add the value of "sentence" column to the list
                    questions.Add(reader.GetString("sentence"));
                }
            }

            // Return the list of questions
            return questions;
        }

        // 🔹 Function to pick one random sentence from the list
        private string GetRandomQuestion(List<string> questions)
        {
            Random rnd = new Random(); // Create random generator
            int randomIndex = rnd.Next(0, questions.Count);
            // Random.Next(0, n) → returns a random number between 0 and n-1

            return questions[randomIndex]; // Pick question at random index
        }

        private void StartQuestionTimer()
        {
            
            countdown = 6; // reset countdown each time
            lbltimer.Text =  countdown.ToString();

            timer1 = new Timer();
            timer1.Interval = 1000; // 1000 ms = 1 second
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbltimer.Text = "";
            countdown--; // decrease by 1 each second
            lbltimer.Text = countdown.ToString();

            if (countdown <= 0)
            {
                timer1.Stop();

                // Switch to next form when time is up
                ReverseSentence ReverseSentence = new ReverseSentence(currentSentence); // replace with your next form
                ReverseSentence.Show();

                this.Hide();
            }
        }
    }
}
