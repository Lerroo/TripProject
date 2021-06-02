using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FastTripApp.DAO.Models.StatusEnum
{
    public enum StatusEnum : int
    {
        [StringValue("Abandon")]
        Abandon,
        [StringValue("Success")]
        Success
    };
}
