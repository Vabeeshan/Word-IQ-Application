using System;
using System.Collections.Generic;
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

        // 🔹 Timer object (5 seconds)
        private Timer questionTimer;

        // 🔹 Countdown value
        private int countdown = 10;

        public ReverseSentenceQuestion()
        {
            InitializeComponent();
        }

      

        // 🔹 Function to fetch ALL sentences from database
        private List<string> LoadAllQuestions()
        {
            List<string> questions = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT sentence FROM reversequestions;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    questions.Add(reader.GetString("sentence"));
                }
            }

            return questions;
        }

        // 🔹 Function to pick one random sentence
        private string GetRandomQuestion(List<string> questions)
        {
            Random rnd = new Random();
            int randomIndex = rnd.Next(0, questions.Count);
            return questions[randomIndex];
        }

        // 🔹 Function to handle timer with countdown
        private void StartQuestionTimer()
        {
          
            lblTimer.Text =  countdown.ToString() ;

            questionTimer = new Timer();
            questionTimer.Interval = 1000; // 1000 ms = 1 second
            questionTimer.Tick += QuestionTimer_Tick;
            questionTimer.Start();
        }

        // 🔹 Called every second
        private void QuestionTimer_Tick(object sender, EventArgs e)
        {
            countdown--; // decrease by 1 each second
            lblTimer.Text =  countdown.ToString();

            if (countdown <= 0)
            {
                questionTimer.Stop();

                // Switch to next form when time is up
                ReverseSentence nextForm = new  ReverseSentence(currentSentence); // replace with your next form
                nextForm.Show();

                this.Hide();
            }
        }

        private void ReverseSentenceQuestion_Load_1(object sender, EventArgs e)
        {
            txtName.Text = GameSession.PlayerName;
            txtScore.Text = GameSession.Score.ToString();
            // Step 1: Load all questions from DB into the list
            allQuestions = LoadAllQuestions();

            // Step 2: Pick one random question from the list
            currentSentence = GetRandomQuestion(allQuestions);

            lblAnagram.Text = currentSentence;
            // Step 4: Start the 5-second timer
            StartQuestionTimer();
        }
    }
}
