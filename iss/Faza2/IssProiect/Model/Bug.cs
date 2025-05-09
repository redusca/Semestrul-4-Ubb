using model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    [Table("Bugs")]
    public class Bug
    {
        [Key]
        [Column("bugNo")]
        public long BugNo { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Status")]
        public BugStatus Status { get; set; }

        public Bug(long bugNo, string description, BugStatus status)
        {
            BugNo = bugNo;
            Description = description;
            Status = status;
        }
        public Bug(string description, BugStatus status)
        {
            Description = description;
            Status = status;
        }

        public override string ToString()
        {
            return $"Bug [{Description.Split('\n')[0]}]";
        }

        public override bool Equals(object? obj)
        {
            if(obj is Bug bug)
            {
                return Description.Split('\n')[0] == bug.Description.Split('\n')[0];
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Description.Split('\n')[0].GetHashCode();
        }
    }
}
