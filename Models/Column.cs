using System;

namespace RestNew.Models
{
    public class Column
    {
        public int id { get; set; }
        public string column_type { get; set; }
        public int number_of_floors_served { get; set; }
        public string status { get; set; }
        public string information { get; set; }
        public string notes { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int battery_id { get; set; }

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