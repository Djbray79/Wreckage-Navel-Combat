using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace WreckageNavelCombat
{
    public partial class Wreckage : Form
    {
        int totalWins = 0;
        int totalLosses = 0;        
        List<Button> playerPosition;
        List<Button> enemyPosition;
        Random rand = new Random();

        
        int totalShips = 5;
        int totalEnemy = 5;
        int rounds = 0;
        int playerTotalScore = 0;
        int enemyTotalScore = 0;

        //private object enemyPositionPicker;


        public Wreckage()
        {
            InitializeComponent();
            loadbuttons();
            enemyLocationList.Text = null;
        }

        private void loadbuttons()
        {
            playerPosition = new List<Button>
            {
                q1, q2, q3, q4, q5, q6, q7, q8, q9, q10, 
                r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, 
                s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, 
                t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, 
                u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, 
                v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, 
                w1, w2, w3, w4, w5, w6, w7, w8, w9, w10, 
                x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, 
                y1, y2, y3, y4, y5, y6, y7, y8, y9, y10, 
                z1, z2, z3, z4, z5, z6, z7, z8, z9, z10
            };
            enemyPosition = new List<Button>
            {
                a1, a2, a3, a4, a5, a6, a7, a8, a9, a10,
                b1, b2, b3, b4, b5, b6, b7, b8, b9, b10,
                c1, c2, c3, c4, c5, c6, c7, c8, c9, c10,
                d1, d2, d3, d4, d5, d6, d7, d8, d9, d10,
                e1, e2, e3, e4, e5, e6, e7, e8, e9, e10,
                f1, f2, f3, f4, f5, f6, f7, f8, f9, f10,
                g1, g2, g3, g4, g5, g6, g7, g8, g9, g10,
                h1, h2, h3, h4, h5, h6, h7, h8, h9, h10,
                i1, i2, i3, i4, i5, i6, i7, i8, i9, i10,
                j1, j2, j3, j4, j5, j6, j7, j8, j9, j10
            };

            for (int i = 0; i < enemyPosition.Count; i++)
            {
                enemyPosition[i].Tag = null;
                enemyLocationList.Add(enemyPosition[i].Text);
            }
        }

        private void playerPicksPosition(object sender, EventArgs e)
        {
            if (totalShips > 0)
            {
                var button = (Button)sender;
                button.Enabled = false;
                button.Tag = "playerShip";
                button.BackColor = System.Drawing.Color.Blue;
                totalShips--;
            }
            if (totalShips == 0)
            {
                helpText.Text = "2: Now pick a attack position from the top grid.";
            }
        }

        private void enemyPickPosition()
        {
            int index = rand.Next(enemyPosition.Count);

            if (enemyPosition[index].Enabled == true && enemyPosition[index].Tag == null)
            {
                enemyPosition[index].Tag = "enemyShip";
                totalEnemy--;

                Debug.WriteLine("Enemy Position  " + enemyPosition[index].Text);
            }
            else
            {
                index = rand.Next(enemyPosition.Count);
            }
            if (totalEnemy < 1)
            {
                return;
            }
        }

        private void attackEnemyPosition(object sender, EventArgs e)
        {        
            var attackPos = (Button)sender;                

            if (attackPos.Enabled)
            {                   
                if (attackPos.Tag == "enemyShip")
                {
                    attackPos.Enabled = false;
                    attackPos.BackColor = System.Drawing.Color.Red;
                    playerTotalScore++;
                    playerScore.Text = "" + playerTotalScore;
                }
                else
                {
                    attackPos.Enabled = false;
                    attackPos.BackColor = System.Drawing.Color.Gray;
                }
                enemyAttackPlayer(sender, e);
            }            
        }       

        private void enemyAttackPlayer(object sender, EventArgs e)
        {
            if (playerPosition.Count > 0)
            {

                rounds++;
                roundsText.Text = "" + rounds;

                int index = rand.Next(playerPosition.Count);

                if (playerPosition[index].Tag == "playerShip")
                {
                    enemyMoves.Text = "" + playerPosition[index].Text;
                    playerPosition[index].Enabled = false;
                    playerPosition[index].BackColor = System.Drawing.Color.Red;
                    playerPosition.RemoveAt(index);
                    enemyTotalScore++;
                    enemyScore.Text = "" + enemyTotalScore;
                }
                else
                {
                    enemyMoves.Text = "" + playerPosition[index].Text;
                    playerPosition[index].Enabled = false;
                    playerPosition[index].BackColor = System.Drawing.Color.Gray;
                    playerPosition.RemoveAt(index);
                }
            }

            if (playerTotalScore > 4 || enemyTotalScore > 4)
            {
                if (playerTotalScore > enemyTotalScore)
                {
                    MessageBox.Show("You Win", "Winning");
                    totalWins++;
                    playerWins.Text = "" + totalWins;
                }
                if (playerTotalScore == enemyTotalScore)
                {
                    MessageBox.Show("No one wins this", "Draw");
                }
                if (enemyTotalScore > playerTotalScore)
                {
                    MessageBox.Show("Haha! I Sunk Your Battle Ship", "Lost");
                    totalLosses++;
                    playerLosses.Text = "" + totalLosses;
                }
            }
        }        

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void playAgainButton_Click(object sender, EventArgs e)
        {
            ;
        }         
    }
}
