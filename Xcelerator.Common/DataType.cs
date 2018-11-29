using System;
using System.Collections.Generic;
using System.Text;

namespace Xcelerator.Common
{
    public class DataType
    {
        public enum Operation : byte
        {
            Create = 0,
            Read = 1,
            Update = 2,
            Delete = 3,
            AssignAudit = 5
        }
    }
}
