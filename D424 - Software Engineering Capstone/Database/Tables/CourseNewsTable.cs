using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D424___Software_Engineering_Capstone.Database.Tables
{
    public class CourseNewsTable
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("datePosted")]
        public DateTime PostedDate { get; set; }

        [Column("newsDetails")]
        public string NewsDetails { get; set; }

        [Column("closureId")]
        public int ClosureId { get; set; }
    }
}
