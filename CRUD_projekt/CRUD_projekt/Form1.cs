using System.Data;
using System.Data.SqlClient;

namespace CRUD_projekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        SqlConnection con = new("Data Source=LAPTOP-LRCKM8E9\\SQLEXPRESS;Initial Catalog=CRUD_PROJECT;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedItem == null)
                MessageBox.Show("Uzupe³nij wszystkie pola!", "UWAGA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            con.Open();
            string status = "";
            if (radioButton1.Checked == true)
            {
                status = radioButton1.Text;
            }
            else
            {
                status=radioButton2.Text;
            }
            SqlCommand com = new SqlCommand("exec dbo.SP_Produkt_Dodaj '"+int.Parse(textBox1.Text)+"','"+textBox2.Text+ "','" + comboBox1.Text + "','" + status +"' , '" + DateTime.Parse(dateTimePicker1.Text) + "'",  con);
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Udany zapis");
            ZaladujWszystkieWyniki();

        }

        void ZaladujWszystkieWyniki() 
        {
            SqlCommand com = new SqlCommand("exec dbo.SP_Produkt_Widok ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt; 
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ZaladujWszystkieWyniki();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string status = "";
            if (radioButton1.Checked == true)
            {
                status = radioButton1.Text;
            }
            else
            {
                status = radioButton2.Text;
            }
            SqlCommand com = new SqlCommand("exec dbo.SP_Produkt_Aktualizuj '" + int.Parse(textBox1.Text) + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + status + "' , '" + DateTime.Parse(dateTimePicker1.Text) + "'", con);
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Udana akutalizacja");
            ZaladujWszystkieWyniki();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Jesteœ pewny, ¿e chcesz to usun¹æ?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                con.Open();
                SqlCommand com = new SqlCommand("exec dbo.SP_Produkt_Usun '" + int.Parse(textBox1.Text) + "'", con);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Udane usuniêcie");
                ZaladujWszystkieWyniki();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("exec dbo.SP_Produkt_Szukaj '"+int.Parse(textBox1.Text)+"'", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back); 
        }
    }
}