//********************************************************************************************************/
// Filename: ProductAndOrder.cs
// Designed for SQL DataSet Connection
// Created by: Sophie Tan
// Change history:
// July 13.2020 by Sophie Tan

/********************************************************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductAndOrder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.productsBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.northwindDataSet);
            }

            // code with handles ADO.NET errors
            catch (DBConcurrencyException)
            {
                MessageBox.Show("Other users changed of deleted data. Try again", "Concurrency Error");
                this.productsTableAdapter.Fill(this.northwindDataSet.Products); //refresh data
            }

            catch (SqlException ex)
            {
                MessageBox.Show("Database erro # " + ex.Number + ": " + ex.Message, ex.GetType().ToString());

            }

            catch (Exception ex)
            {
                MessageBox.Show("Other  error  while saving changes: " + ex.Message, ex.GetType().ToString());
                productsBindingSource.CancelEdit();
            }


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet.Categories' table. You can move, or remove it, as needed.
            this.categoriesTableAdapter.Fill(this.northwindDataSet.Categories);
            // TODO: This line of code loads data into the 'northwindDataSet.Suppliers' table. You can move, or remove it, as needed.
            this.suppliersTableAdapter.Fill(this.northwindDataSet.Suppliers);
            // TODO: This line of code loads data into the 'northwindDataSet.Order_Details' table. You can move, or remove it, as needed.
            this.order_DetailsTableAdapter.Fill(this.northwindDataSet.Order_Details);
            // TODO: This line of code loads data into the 'northwindDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.northwindDataSet.Products);

        }

        private void order_DetailsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            int row = e.RowIndex + 1; //numbering  starting with 1
            int col = e.ColumnIndex + 1;
            MessageBox.Show("Bad data in the grid: row " + row + " and column " + col + "   Data Error");
        }
    }
}
