﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.Microservice.Shared.Enums
{
    public class Enums
    {
        public enum Gender
        {
            Male = 1,
            Female = 2
        }
        public enum RemittanceType
        {
            Internal = 1,
            External = 2
        }
        public enum Remittance_Status
        {

            Draft = 1,
            Ready = 2,
            CheckedAML = 3,
            Approved = 4,
            Release = 5,


        }



    }
}