using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public interface IOption
{
    Guid OptionId { get; }
    string OptionText { get; }
}
