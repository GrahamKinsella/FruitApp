﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eFruitWorld.Objects
{
    public class ShippingDetails
    {

        [Key]
        public int ShippingDetailsId { get; set; }

        public string Name { get; set; }
    
        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
        public string Username { get; set; }

        [NotMapped]
        public string CreditCard { get; set; }


    }
}
