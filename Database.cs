using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace DBOWN
{
    public partial class Database : Form
    {
        List<GAME> games = new List<GAME>();
        

        public Database()
        {
            InitializeComponent();
            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            
            if (textBox4.Text == "" || textBox5.Text == ""|| textBox2.Text == "" || textBox3.Text == "" )
            {
                MessageBox.Show("Something missing.");
                return;
            }
            

            if (!Int32.TryParse(textBox4.Text, out int result2) || !Int32.TryParse(textBox5.Text, out int result3))
            {
                MessageBox.Show("Something is wrong.");
                
                goto here;
            }
            
            

            DB.gamesF(games, textBox2.Text, textBox3.Text, Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox5.Text), textBox6.Text);
            for (int i = 0; i < games.Count; i++)
            {
                dataGridView1.DataSource = games[i];
            }
            dataGridView1.DataSource = games;

            here:
            
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

        }

        
    }
}