using System;

namespace POS_Shop.Models
{
    public class AppConfiguration
    {
        public FeatureSettings Features { get; set; }
        public InvoiceSettings InvoiceSettings { get; set; } // Changed from 'Invoice' to 'InvoiceSettings'

        public AppConfiguration()
        {
            Features = new FeatureSettings();
            InvoiceSettings = new InvoiceSettings();
        }
    }

    public class FeatureSettings
    {
        public bool EnableUpdateQty { get; set; } = false; // Default value
        // Add more feature flags as needed
    }

    public class InvoiceSettings
    {
        public string ShopName { get; set; } = "Demo Shop POS";
        public string ShopAddress { get; set; } = "123 Main Street";
        public string ContactNumber { get; set; } = "+1 234-567-8900";
        public string Email { get; set; } = "shop@example.com";
        public string TaxNumber { get; set; } = "TAX-12345";
        public string FooterMessage { get; set; } = "Thank you for your business!";
    }
}
