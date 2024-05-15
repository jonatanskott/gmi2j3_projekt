using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public class PracticeOption : IOption
{
    public Guid OptionId { get; private set; }
    public string OptionText { get; private set; }
    public bool IsCorrect { get; private set; }

    public PracticeOption(string optionText, bool isCorrect) : this(Guid.NewGuid(), optionText, isCorrect)
    {
    }

    [JsonConstructor]
    public PracticeOption(Guid optionId, string optionText, bool isCorrect)
    {
        OptionId = optionId;
        OptionText = optionText;
        IsCorrect = isCorrect;
    }
}
