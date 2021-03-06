﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using GeoAPI.Geometries;
using RealEstate.Services.Database.Base;
using RealEstate.Services.Extensions;
using RealEstate.Services.ServiceLayer;

namespace RealEstate.Services.Database.Tables
{
    public class Property : BaseEntity
    {
        [Required]
        public string Street { get; set; }

        [Obsolete]
        public string Alley { get; set; }

        public string BuildingName { get; set; }
        public string Number { get; set; }
        public int Floor { get; set; }
        public int Flat { get; set; }
        public string Description { get; set; }

        public string DistrictId { get; set; }
        public string CategoryId { get; set; }
        public IPoint Geolocation { get; set; }
        public virtual Category Category { get; set; }

        [NotMapped]
        private string StreetNormalized => GlobalService.FixAddress(Street);

        [NotMapped]
        public string Address
        {
            get
            {
                var finalString = new StringBuilder();
                finalString.Append(StreetNormalized);

                if (!string.IsNullOrEmpty(Number))
                    finalString.Append("، ").Append("پلاک ").Append(Number);

                if (!string.IsNullOrEmpty(BuildingName))
                    finalString.Append("، ").Append("ساختمان ").Append(BuildingName);

                if (Floor > 0)
                    finalString.Append("، ").Append("طبقه ").Append(Floor);

                if (Flat > 0)
                    finalString.Append("، ").Append("واحد ").Append(Flat);

                return finalString.ToString();
            }
        }

        public virtual District District { get; set; }
        public virtual ICollection<PropertyOwnership> PropertyOwnerships { get; set; }

        [NotMapped]
        public PropertyOwnership CurrentPropertyOwnership => PropertyOwnerships?.OrderDescendingByCreationDateTime().FirstOrDefault();

        public virtual ICollection<Item> Items { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<PropertyFacility> PropertyFacilities { get; set; }
        public virtual ICollection<PropertyFeature> PropertyFeatures { get; set; }

        public override string ToString()
        {
            return Address;
        }
    }
}