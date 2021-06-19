using FastTripApp.DAO.Models.Enums;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FastTripApp.DAO.Models.Trip
{
    /// <summary>
    /// Contain Possible trip end statuses
    /// </summary>
    public enum StatusEnum : int
    {
        [StringValue("Abandon")]
        Abandon,
        [StringValue("Success")]
        Success
    };
}
