﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorTrack.Domain.DTOs
{
    public class NoSlotsFoundException : Exception
    {
        public NoSlotsFoundException(string message) : base(message) { }
    }
}
