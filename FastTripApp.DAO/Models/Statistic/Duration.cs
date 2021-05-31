using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastTripApp.DAO.Models.Statistic
{
    [Schema]
    public class Duration
    {
        public int Shortest { get; set; }
        public int Maximum { get; set; }
        public int Average { get; set; }
    }
}
