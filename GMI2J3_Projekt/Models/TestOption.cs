
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public class TestOption : IOption
{
    public Guid OptionId { get; private set; } = Guid.NewGuid();
    public string OptionText { get; private set; } = "default";

    public TestOption() {}
    public TestOption(PracticeOption option)
    {
        OptionText = option.OptionText;
        OptionId = option.OptionId;
    }

    public TestOption(Guid id, string text)
    {
        OptionId = id;
        OptionText = text;
    }
}
