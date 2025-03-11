﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WebShop.Models
{
    public class ProductCardViewModel
    {
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public int Quantity {get; set;}
        public long Code { get; set; }
        public string Article { get; set;}
        public List<string> Images { get; set; }
        public string BrandImage { get; set; }
        public List<ProductCardViewModel> Analogues { get; set; }
        public List<Pair> Specifications { get; set; }
        public ApplicabilityCharacteristics Applicability { get; set; }
        public string Description { get; set; }
    }

    public class ApplicabilityCharacteristics
    {
        public string EngineVolume { get; set; }
        public double EngineVolumeCM { get; set; }
        public DateTime ProductionYearBegin { get; set; }
        public DateTime ProductionYearEnd { get; set; }
        public double Power { get; set; }
        public string BodyType { get; set; }
    }
}