using System;

namespace RestNew.Models
{
    public class Quote
    {
        public int id { get; set; }
        public string building_type { get; set; }
        public string amount_elevators { get; set; }
        public string product_line { get; set; }
        public string installation_fees { get; set; }
        public string total_cost { get; set; }
        public string amount_floors { get; set; }
        public string amount_basements { get; set; }
        public string amount_parking_spots { get; set; }
        public string maximum_occupancy { get; set; }
        public string amount_apartments { get; set; }
        public string amount_companies { get; set; }
        public string amount_corporations { get; set; }
        public string business_hours { get; set; }
        public string email { get; set; }
        public string company_name { get; set; }
        public string full_name { get; set; }
        public string phone { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}