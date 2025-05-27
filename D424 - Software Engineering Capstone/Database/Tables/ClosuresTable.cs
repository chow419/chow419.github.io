using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D424___Software_Engineering_Capstone.Database.Tables
{
    public class ClosuresTable
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("closure_date")]
        public DateTime ClosureDate { get; set; }

        [Column("closure_reason")]
        public string ClosureReason { get; set; }
    }
}
