using System;

namespace RestNew.Models
{
    public class Address
    {
        public int id { get; set; }
        public string type_of_address { get; set; }
        public string status { get; set; }
        public string entity { get; set; }
        public string number_and_street { get; set; }
        public string suite_or_apartment { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
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