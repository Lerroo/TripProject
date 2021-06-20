﻿using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Reports.Pdf.Default
{
    class FooterSettingsDefault : FooterSettings
    {
        public FooterSettingsDefault()
        {
            base.FontSize = 12;
            base.FontName = "Ariel";
            base.Center = "This is for demonstration purposes only.";
            base.Line = true;
        }
    }
}