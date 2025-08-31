using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_IQ_Application
{
    class GameSession
    {
        // 🔹 Store player's name
        public static string PlayerName { get; set; } = "";

        // 🔹 Store player's score
        public static int Score { get; set; } = 0;

        public static int wrongAttempts { get; set; } = 0;

        

        // 🔹 Method to reset game session
        public static void Reset()
        {
            PlayerName = "";
            Score = 0;
            wrongAttempts = 0;
        }
    }
}
