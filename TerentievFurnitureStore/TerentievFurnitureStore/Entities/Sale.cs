//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TerentievFurnitureStore.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sale
    {
        public int idSale { get; set; }
        public Nullable<int> idClient { get; set; }
        public Nullable<int> idProduct { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public string Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Product Product { get; set; }
    }
}
