﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.Entities.Settings
{
    public class TokenParameters
    {
        public bool ValidateIssuer { get; set; }
        public string ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public string ValidAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public string IssuerSigningKey { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
    }
}
