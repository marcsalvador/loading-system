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
    
    public partial class CardLoadHistory
    {
        public int CardLoadHistoryId { get; set; }
        public Nullable<int> CardId { get; set; }
        public Nullable<decimal> TenderedAmount { get; set; }
        public Nullable<decimal> Cash { get; set; }
        public Nullable<decimal> Changed { get; set; }
        public Nullable<System.DateTime> ProcessDateAndTime { get; set; }
    }
}
