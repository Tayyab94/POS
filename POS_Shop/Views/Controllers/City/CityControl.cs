using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.City
{
    public partial class CityControl : UserControl
    {
        public CityControl()
        {
            InitializeComponent();
            LoadCountries();
        }

        private void SaveCityBtn_Click(object sender, EventArgs e)
        {
            using(var context = new POSDbContext())
            {

                ICountryRepository countryRepository = new CountryRepository(context);

                countryRepository.Insert(new Models.Country()
                {
                    CountryName = bunifuTextBox1.Text
                });
                countryRepository.Save(); 
            }
            MessageBox.Show("City saved successfully!");
        }


        private void LoadCountries()
        {
            using (var context = new POSDbContext())
            {
                ICountryRepository countryRepository = new CountryRepository(context);
                var countries = countryRepository.GetAll().ToList();
               
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("IsActive", typeof(bool));

                foreach (var country in countries)
                {
                    dt.Rows.Add(country.Id, country.CountryName, country.IsActive);
                }

                //CountryDatagridView.AutoGenerateColumns = true;

                CountryDatagridView.ReadOnly=true;
                CountryDatagridView.AllowUserToAddRows = false;

                CountryDatagridView.DataSource = dt;
            }
        }

        private void CountryDatagridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = CountryDatagridView.Rows[e.RowIndex];
                bunifuTextBox1.Text = row.Cells["Name"].Value.ToString();

            }
        }
    }
}
