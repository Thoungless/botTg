using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace botseeds.Models
{
    public class Operation
    {
        public string group_id { get; set; }
        public string operation_id { get; set; }
        public string title { get; set; }
        public double amount { get; set; }
        public string direction { get; set; }
        public DateTime datetime { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public List<object> spendingCategories { get; set; }
        public string amount_currency { get; set; }
        public bool is_sbp_operation { get; set; }
    }

    public class RootObject
    {
        public string next_record { get; set; }
        public List<Operation> operations { get; set; }
    }
}