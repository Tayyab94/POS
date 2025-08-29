using Bunifu.UI.WinForms;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Country
{
    public partial class CountryControl1 : UserControl
    {
        public CountryControl1()
        {
            InitializeComponent();

            LoadCountriesForDataGridView();
        }


        //private void loadCountries()
        //{
        //    using (var context = new POSDbContext())
        //    {
        //        var countriesList = context.Countries.ToList();
        //        CountryDropDownLst.Items.Clear();
        //        CountryDropDownLst.DataSource = countriesList;
        //        CountryDropDownLst.DisplayMember = "CountryName";
        //        CountryDropDownLst.ValueMember = "Id";
        //    }
        //}


        private void LoadCountriesForDataGridView()
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

                CountryDatagridView.ReadOnly = true;
                CountryDatagridView.AllowUserToAddRows = false;

                CountryDatagridView.DataSource = dt;
            }
        }

        private void CountryDatagridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = CountryDatagridView.Rows[e.RowIndex];
                CountryNameTxt.Text = row.Cells["Name"].Value.ToString();
                countryIdTxt.Text= row.Cells["ID"].Value.ToString();
                UpdateCountrybtn.Enabled = true;
            }
        }

        private void SaveCityBtn_Click(object sender, EventArgs e)
        {
            using (var context = new POSDbContext())
            {

                ICountryRepository countryRepository = new CountryRepository(context);

                countryRepository.Insert(new Models.Country()
                {
                    CountryName = CountryNameTxt.Text
                });
                countryRepository.Save();
            }
            MessageBox.Show("City saved successfully!");
        }


        private async void UpdateCountrybtn_Click_1(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(countryIdTxt.Text) || !int.TryParse(countryIdTxt.Text, out int countryId) || countryId <= 0)
            {
                MessageBox.Show("Please select Record first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new POSDbContext())
            {
                ICountryRepository countryRepository = new CountryRepository(context);
                //ICityRepository cityRepository = new CityRepository(context);
                var response = await countryRepository.UpdateCountry(new Models.Country()
                {
                    Id = Convert.ToInt32(countryId.ToString()),
                    CountryName = CountryNameTxt.Text,
                    IsActive = true,
                });

                if (response)
                    MessageBox.Show("Record has been Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoadCountriesForDataGridView();
            }
        }

    }
}
