﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    interface IImport
    {
        Model Load(int i);
        int GetCount();
    }
}
