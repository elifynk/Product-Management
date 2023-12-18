using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ProductDal _productDal = new ProductDal();
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            dgvProducts.DataSource = _productDal.GetAll();
        }
        private void SearchProducts(string key)
        {
            var result = _productDal.GetAll().Where(p => p.Name.ToLower().Contains(key.ToLower())).ToList();
            dgvProducts.DataSource = result;
        }
        private void FilterProducts(decimal price)
        {
            var result = _productDal.GetAll().Where(p => p.UnitPrice <= price).ToList();
            dgvProducts.DataSource = result;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product { 
                Name = tbxName.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                StockAmount = Convert.ToInt32(tbxStockAmount.Text),
            });

            LoadProducts();
            MessageBox.Show("Product Added !");
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxNameUpdate.Text = dgvProducts.CurrentRow.Cells[1].Value.ToString();
            tbxUnitPriceUpdate.Text = dgvProducts.CurrentRow.Cells[2].Value.ToString();
            tbxStockAmountUpdate.Text = dgvProducts.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product product = new Product{ 
                Id = Convert.ToInt32(dgvProducts.CurrentRow.Cells[0].Value),
                Name = tbxNameUpdate.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                StockAmount = Convert.ToInt32(tbxStockAmountUpdate.Text)
        };

            _productDal.Update(product);
            LoadProducts();
            MessageBox.Show("Upgraded !");

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvProducts.CurrentRow.Cells[0].Value);

            _productDal.Delete(id);
            LoadProducts();
            MessageBox.Show("Deleted !");
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchProducts(tbxSearch.Text);  
        }

        private void tbxMaxUnitPrice_TextChanged(object sender, EventArgs e)
        {
            if (tbxMaxUnitPrice.Text == "")
            {
                tbxMaxUnitPrice.Text = "0";
                Convert.ToDecimal(tbxMaxUnitPrice.Text);
            }

            FilterProducts(Convert.ToDecimal(tbxMaxUnitPrice.Text));
  
        }
    }
}

