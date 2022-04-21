using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings
{
    /// <summary>
    /// Filter options used in InOutUtils.PrintByFilter method
    /// Helps to pick out which data type to filter out
    /// </summary>
    public enum FilterOptions
    {
        description = 0,
        responsiblePerson = 1,
        category = 2,
        type = 3,
        dates = 4,
        number = 5
    }
}
