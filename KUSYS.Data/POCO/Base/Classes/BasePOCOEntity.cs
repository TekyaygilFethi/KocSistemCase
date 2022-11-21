using KUSYS.Data.POCO.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Data.POCO.Base.Classes
{
    public class BasePOCOEntity<T> : PrimaryKeyEntity<T>, IModifiable
    {
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
