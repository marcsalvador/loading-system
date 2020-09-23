using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingSystem.BusinessLogic.Enums
{
    public enum CardType
    { 
        Regular = 0,
        Senior = 1,
        Pwd = 2
    }

    public enum DiscountType
    {
        None = 0,
        Senior = 1,
        Pwd = 2,
        Standard4TripPerDay = 3
    }

    public enum DirectionType
    { 
        North
    }

    public enum SortType
    { 
        Asc = 1,
        Desc = 2
    }
}
