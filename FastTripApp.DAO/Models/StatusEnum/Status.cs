using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FastTripApp.DAO.Models.StatusEnum
{
    public enum Status : int
    {
        [StringValue("Abandon")]
        Abandon,
        [StringValue("Success")]
        Success
    };

}
