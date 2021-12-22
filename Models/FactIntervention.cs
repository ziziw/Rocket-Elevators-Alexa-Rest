using System;
using System.ComponentModel.DataAnnotations;
namespace RestNew.Models
{
    public class FactIntervention
    {
        [Key]
        public int intervention_id { get; set; }
        public int employee_id { get; set; }
        public int building_id { get; set; }
        public System.Nullable<int> column_id { get; set; }
        public System.Nullable<int> elevator_id { get; set; }
        public DateTime start_intervention { get; set; }
        public System.Nullable<DateTime> end_intervention { get; set; }
        public string result { get; set; }
        public string status { get; set; }

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
            return "intervention_id: " + this.intervention_id + " employee_id: " + this.employee_id + " ";
        }
    }
}