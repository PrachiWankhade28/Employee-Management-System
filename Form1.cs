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


namespace ADODisconnected
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmb;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Prachi;Integrated Security=True;Pooling=False");
            da = new SqlDataAdapter("Select * from Emp", con);

            //Associate command builder with Adapter
            cmb = new SqlCommandBuilder(da);

            //Keep the primary constraints of table into dataset
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            ds = new DataSet();
            //Fill the Dataset-da will open connection fires query-fetch the data and fill it in ds-close connection.
            da.Fill(ds,"Emp");

            //get reference of table of ds into dt
            dt = ds.Tables["Emp"];

            //show data in gridview 
            dataGridView1.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            //search id
            DataRow dr = dt.Rows.Add(id);
            if (dr != null)
            {
                //if found,delete
                dr.Delete();
                //update database
                da.Update(ds, "Emp");
                MessageBox.Show("Record Deleted.......");
            }
            else
                MessageBox.Show("Record not Found..!!");

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //Create new row
            DataRow dr = dt.NewRow();
            //put the data from textbox into row
            dr["Id"] = Convert.ToInt32(txtId.Text);
            dr[1] = txtName.Text;
            dr["Salary"] = Convert.ToDouble(txtSalary.Text);
            dr[3] = Convert.ToInt32(txtDid.Text);

            //add row to dt
            dt.Rows.Add(dr);

            //update database
            da.Update(ds, "Emp");
            MessageBox.Show("Insertion Successful!!!!");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            //search id
            DataRow dr = dt.Rows.Find(id);
            if (dr != null)
            {
                dr["Name"] = txtName.Text;
                dr["Salary"] = Convert.ToDouble(txtSalary.Text);

                //update database
                da.Update(ds, "Emp");
                MessageBox.Show("Record Updated...");
            }
            else
                MessageBox.Show("Record not found..");
        }
    }
}
