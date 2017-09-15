using System.Collections.Generic;

namespace ASPNETPT.Models
{
    public interface IBtcRepo
    {
        IEnumerable<BtCprop> GetBtCprops();
    }
}