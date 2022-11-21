using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Data.POCO.Base.Interfaces
{
    public interface IPrimaryKey<T>
    {
        public T Id { get; set; }
    }
}
