using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reflexgame
{
    public partial class ReflexForm : Form
    {
        private Random random = new Random();
        private Timer waitTimer = new Timer();

        private bool isWaiting = false;
        private bool isReady = false;
        private int points = 0;

        public ReflexForm()
        {
            InitializeComponent();

            waitTimer.Interval = 3000;
            waitTimer.Tick += waitTimer_Tick;
            gamebtn.Text = "Start";
        }

        private void gamebtn_Click(object sender, EventArgs e)
        {
            if (isReady)
            {
                isReady = false;
                waitTimer.Stop();
                gamebtn.Text = "You won! Play again?";
                points += 1;
                pointsLabel.Text = "Score: " + points.ToString();
            }
            else if (isWaiting)
            {
                waitTimer.Stop();
                isWaiting = false;
                gamebtn.Text = "Too soon!";
            }
            else
            {
                isWaiting = true;
                waitTimer.Interval = 3000;
                waitTimer.Start();
                gamebtn.Text = "Wait 3 seconds...";
            }
        }

        private void waitTimer_Tick(object sender, EventArgs e)
        {
            waitTimer.Stop();

            if (isWaiting)
            {
                isWaiting = false;
                isReady = true;
                int maxX = gamebtn.Parent.ClientSize.Width - gamebtn.Width;
                int maxY = gamebtn.Parent.ClientSize.Height - gamebtn.Height;
                gamebtn.Location = new Point(random.Next(Math.Max(1, maxX + 1)), random.Next(Math.Max(1, maxY + 1)));
                waitTimer.Interval = 1100;
                waitTimer.Start();
                gamebtn.Text = "Click me!";
            }
            else if (isReady && gamebtn.Text.Contains("Click me!"))
            {
                isReady = false;
                gamebtn.Text = "Time's up! You were too late!";
            }
        }
    }
}
