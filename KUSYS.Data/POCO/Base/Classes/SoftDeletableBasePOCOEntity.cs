using KUSYS.Data.POCO.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Data.POCO.Base.Classes
{
    public class SoftDeletableBasePOCOEntity<T> : BasePOCOEntity<T>, ISoftDeletable
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
