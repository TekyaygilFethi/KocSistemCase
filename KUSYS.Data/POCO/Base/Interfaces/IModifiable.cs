﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Data.POCO.Base.Interfaces
{
    public interface IModifiable
    {
        public DateTime ModifiedDate { get; set; }
    }
}