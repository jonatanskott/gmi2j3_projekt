using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public class PracticeQuestion : IQuestion
{
    public Guid QuestionId { get; set; } = Guid.NewGuid();
    public string QuestionText { get; set; } = "";

    public ICollection<PracticeOption> Options { get; set; } = new List<PracticeOption>();

    public string Unit { get; }

    [System.Text.Json.Serialization.JsonIgnore]
    public bool IsMultipleChoice => NumberOfCorrectChoices > 1;

    [System.Text.Json.Serialization.JsonIgnore]
    public int NumberOfCorrectChoices { get; private set; }
    

    [JsonConstructor]
    public PracticeQuestion(Guid questionId, string questionText, ICollection<PracticeOption> options, string unit)
    {
        QuestionId = questionId;
        QuestionText = questionText;

        Options = options;
        Unit = unit;
        NumberOfCorrectChoices = Options.Count(x => x is PracticeOption option && option.IsCorrect);
    }
}
