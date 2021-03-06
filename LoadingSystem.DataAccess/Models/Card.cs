//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoadingSystem.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Card
    {
        public int CardId { get; set; }
        public string CardNo { get; set; }
        public Nullable<int> CardType { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> DiscountType { get; set; }
        public string PwdId { get; set; }
        public string SeniorCitizenControlNumber { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }

        [NotMapped]
        public int TodayDiscountCount { get; set; }
    }
}
