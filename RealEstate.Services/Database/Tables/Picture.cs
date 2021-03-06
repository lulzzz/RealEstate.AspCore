﻿using RealEstate.Services.Database.Base;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace RealEstate.Services.Database.Tables
{
    public class Picture : BaseEntity
    {
        [Required]
        public string File { get; set; }

        public string Text { get; set; }
        public string PropertyId { get; set; }
        public virtual Property Property { get; set; }
        public string PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public string DealId { get; set; }
        public virtual Deal Deal { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string ReminderId { get; set; }
        public virtual Reminder Reminder { get; set; }

        public override string ToString()
        {
            return Path.GetFileName(File);
        }
    }
}