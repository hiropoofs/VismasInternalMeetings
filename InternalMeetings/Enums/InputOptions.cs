using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings
{
    /// <summary>
    /// Enum designed to be used in Program.ReaderLine method
    /// It defines what kind of string values to expect
    /// </summary>
    public enum InputOptions
    {
        onlyDigits,
        onlyLetters,
        onlySymbol,
        onlyDate,
        any
    }
}
