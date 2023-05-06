using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace DBOWN
{
    public partial class Database : Form
    {
        List<GAME> games = new List<GAME>();
        string editId;
        private string filePath = null; // Declare filePath as a class-level variable to hold the file path

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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (var game in games)
                    {
                        sw.WriteLine($"{game.ID};{game.Name};{game.Studio};{game.Rating};{game.Year};{game.Description}");
                    }
                }
                MessageBox.Show("List saved to file.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No path.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            
            string searchQuery = textBox7.Text.ToLower();
            List<GAME> searchResult = new List<GAME>();

            // Search for games with matching ID
            if (int.TryParse(searchQuery, out int id))
            {
                var matchingGames = games.Where(g => g.ID == id);
                if (matchingGames.Any())
                {
                    searchResult.AddRange(matchingGames);
                }
            }

            // Search for games with matching Name
            var nameMatchingGames = games.Where(g => g.Name.ToLower().Contains(searchQuery));
            if (nameMatchingGames.Any())
            {
                searchResult.AddRange(nameMatchingGames);
            }

            if (searchResult.Any())
            {
                dataGridView1.DataSource = searchResult;
            }
            else
            {
                MessageBox.Show("No matching games found.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected item from the ComboBox
            string selectedItem = comboBox1.SelectedItem.ToString();

            // Sort the list based on the selected item
            switch (selectedItem)
            {
                case "ID<":
                    games = games.OrderBy(g => g.ID).ToList();
                    break;
                case "ID>":
                    games = games.OrderByDescending(g => g.ID).ToList();
                    break;
                case "Name<":
                    games = games.OrderBy(g => g.Name).ToList();
                    break;
                case "Name>":
                    games = games.OrderByDescending(g => g.ID).ToList();
                    break;
                case "Studio<":
                    games = games.OrderBy(g => g.Studio).ToList();
                    break;
                case "Studio>":
                    games = games.OrderByDescending(g => g.ID).ToList();
                    break;
                case "Rating>":
                    games = games.OrderByDescending(g => g.Rating).ToList();
                    break;
                case "Rating<":
                    games = games.OrderBy(g => g.Rating).ToList();
                    break;
                case "Year<":
                    games = games.OrderBy(g => g.Year).ToList();
                    break;
                case "Year>":
                    games = games.OrderByDescending(g => g.ID).ToList();
                    break;
            }

            // Set the sorted list as the DataSource of the DataGridView
            dataGridView1.DataSource = games;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (GAME game in games)
                    {
                        writer.WriteLine(game.ID + ";" + game.Name + ";" + game.Studio + ";" + game.Rating + ";" + game.Year + ";" + game.Description);
                    }
                    MessageBox.Show("Game data saved to " + saveFileDialog.FileName);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    games.Clear(); // clear the current list of games
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(';');
                        int id = int.Parse(parts[0]);
                        string name = parts[1];
                        string studio = parts[2];
                        int rating = int.Parse(parts[3]);
                        int year = int.Parse(parts[4]);
                        string description = parts[5];
                        games.Add(new GAME { ID = id, Name = name, Studio = studio, Rating = rating, Year = year, Description = description });
                    }
                    dataGridView1.DataSource = games;
                    MessageBox.Show("Game data loaded from " + openFileDialog.FileName);
                    filePath = openFileDialog.FileName;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < games.Count; i++)
            {
                if (games[i].ID == int.Parse(textBox1.Text))
                {
                    games.RemoveAt(i);
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    dataGridView1.DataSource = games;
                    textBox1.Text = "";
                    break;
                }                
            }
        }
    
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < games.Count; i++)
            {
                if (games[i].ID == int.Parse(editId))
                {
                    games.RemoveAt(i);
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    dataGridView1.DataSource = games;
                }
            }

            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
            if (textBox5.Text == "")
            {
                textBox5.Text = "0";
            }

            if (!Int32.TryParse(textBox4.Text, out int result2) || !Int32.TryParse(textBox5.Text, out int result3))
            {
                MessageBox.Show("Something is wrong.");

                goto here;
            }



            games.Add(new GAME {ID = int.Parse(editId),Name = textBox2.Text, Studio = textBox3.Text, Rating = int.Parse(textBox4.Text), Year = int.Parse(textBox5.Text), Description = textBox6.Text});

            string selectedItem = (comboBox1.SelectedItem ?? "DF").ToString();

            switch (selectedItem)
            {
                case "ID<":
                    games = games.OrderBy(g => g.ID).ToList();
                    break;
                case "ID>":
                    games = games.OrderByDescending(g => g.ID).ToList();
                    break;
                case "Name<":
                    games = games.OrderBy(g => g.Name).ToList();
                    break;
                case "Name>":
                    games = games.OrderByDescending(g => g.ID).ToList();
                    break;
                case "Studio<":
                    games = games.OrderBy(g => g.Studio).ToList();
                    break;
                case "Studio>":
                    games = games.OrderByDescending(g => g.ID).ToList();
                    break;
                case "Rating>":
                    games = games.OrderByDescending(g => g.Rating).ToList();
                    break;
                case "Rating<":
                    games = games.OrderBy(g => g.Rating).ToList();
                    break;
                case "Year<":
                    games = games.OrderBy(g => g.Year).ToList();
                    break;
                case "Year>":
                    games = games.OrderByDescending(g => g.ID).ToList();
                    break;
                case "DF":
                    games = games.OrderBy(g => g.ID).ToList();
                    break;
            }






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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow cell = dataGridView1.Rows[e.RowIndex];

                textBox8.Text = cell.Cells["Description"].Value.ToString();
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Set the text box values to the values from the selected 
                editId = row.Cells["ID"].Value.ToString();
                textBox2.Text = row.Cells["Name"].Value.ToString();
                textBox3.Text = row.Cells["Studio"].Value.ToString();
                textBox4.Text = row.Cells["Rating"].Value.ToString();
                textBox5.Text = row.Cells["Year"].Value.ToString();
                textBox6.Text = row.Cells["Description"].Value.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}