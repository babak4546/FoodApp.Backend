using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class Restaurant :BaseEntity
    {
        public string Title { get; set; }
        public ApplicationUser Owner { get; set; }
        public string OwnerUsername { get; set; }
        public Boolean IsApproved { get; set; }
        public ApplicationUser? Approver { get; set; }
        public string? ApproverUsername { get; set; }
        public DateTime? ApprovedTime { get; set; }
        public Boolean IsActive { get; set; }

    }
}
