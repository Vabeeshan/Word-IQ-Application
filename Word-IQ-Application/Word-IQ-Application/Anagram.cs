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
        public Anagram()
        {
            InitializeComponent();
        }

        private void Anagram_Load(object sender, EventArgs e)
        {
            lblName.Text = classSession.PlayerName;
            lblScore.Text = classSession.Score.ToString();
        }
    }
}
