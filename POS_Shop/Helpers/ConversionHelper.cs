using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Helpers
{

    public static class ConversionHelper
    {
        public static decimal ParseDecimal(string text, decimal defaultValue = 0)
        {
            return decimal.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal result)
                ? result
                : defaultValue;
        }

        public static int ParseInt(string text, int defaultValue = 0)
        {
            return int.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out int result)
                ? result
                : defaultValue;
        }

        public static double ParseDouble(string text, double defaultValue = 0)
        {
            return double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out double result)
                ? result
                : defaultValue;
        }

        public static bool ParseBool(string text, bool defaultValue = false)
        {
            return bool.TryParse(text, out bool result)
                ? result
                : defaultValue;
        }

        // Optional: Add null-safe version
        public static decimal ParseDecimalSafe(object value, decimal defaultValue = 0)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            return ParseDecimal(value.ToString(), defaultValue);
        }
    }



    public static class DataTableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data, DataTable templateTable = null)
        {
            DataTable table = templateTable?.Clone() ?? new DataTable();

            if (data == null || !data.Any())
                return table;

            var properties = typeof(T).GetProperties();

            // If no template, create columns based on properties
            if (templateTable == null)
            {
                foreach (var prop in properties)
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }

            foreach (var item in data)
            {
                DataRow row = table.NewRow();

                foreach (var prop in properties)
                {
                    if (table.Columns.Contains(prop.Name))
                    {
                        object value = prop.GetValue(item) ?? DBNull.Value;
                        row[prop.Name] = value;
                    }
                }

                table.Rows.Add(row);
            }

            return table;
        }
    }
}
