
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
    
public partial class Delivery
{

    public int idDeliveries { get; set; }

    public int idProvider { get; set; }

    public int idProduct { get; set; }

    public System.DateTime DateOfDeliveries { get; set; }



    public virtual Product Product { get; set; }

    public virtual Provider Provider { get; set; }

}

}
