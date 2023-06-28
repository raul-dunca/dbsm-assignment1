using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Ex
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlDataAdapter dastores;
        SqlDataAdapter daclients;
        DataSet ds;
        BindingSource bsstores;
        BindingSource bsclients;

        SqlCommandBuilder cmdBuilder;

        string querycStores;
        string queryClients;

        public Form1()
        {
            InitializeComponent();
            FillData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                daclients.Update(ds, "Client");
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }
        string getConnectionString()
        {
            return "Data Source=DESKTOP-PDRHPCB;" + "Initial Catalog=GroceyStore;Integrated Security=true";

        }
        void FillData()
        {
            conn = new SqlConnection(getConnectionString());

            querycStores = "Select * From Store";
            queryClients = "Select * From Client";

            dastores = new SqlDataAdapter(querycStores, conn);
            daclients = new SqlDataAdapter(queryClients, conn);

            ds = new DataSet();

            dastores.Fill(ds, "Store");
            daclients.Fill(ds, "Client");


            cmdBuilder = new SqlCommandBuilder(daclients);
            ds.Relations.Add("Relation", ds.Tables["Store"].Columns["Sid"],
               ds.Tables["Client"].Columns["Sid"]);



            bsstores = new BindingSource();
            bsstores.DataSource = ds.Tables["Store"];



            bsclients = new BindingSource(bsstores, "Relation");
            // bsplayers = new BindingSource(bsteams, "TeamsPlayer");


            this.dgvStores.DataSource = bsstores;
            this.dgvClients.DataSource = bsclients;

            cmdBuilder.GetUpdateCommand();
        }
    }
}
