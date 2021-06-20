﻿using FastTripApp.DAO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text;

namespace FastTripApp.DAO.Models.Trip.Way
{
    [Index(nameof(Place.Name), IsUnique = true)]
    public class Place
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int CoordsId { get; set; }
        [ForeignKey("CoordsId")]
        public Coords Coords { get; set; }
    }
}
