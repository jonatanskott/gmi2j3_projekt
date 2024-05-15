
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public class TestQuestion : IQuestion
{
    public Guid QuestionId { get; set; } = Guid.NewGuid();
    public string QuestionText { get; set; } = "";
    public ICollection<TestOption> Options { get; set; } = new List<TestOption>();

    public bool IsMultipleChoice { get; set; }
    public int NumberOfCorrectChoices { get; set; }
    public string Unit { get; }

    public TestQuestion(PracticeQuestion question)
    {
        QuestionText = question.QuestionText;
        QuestionId = question.QuestionId;
        IsMultipleChoice = question.IsMultipleChoice;
        NumberOfCorrectChoices = question.NumberOfCorrectChoices;
        Unit = question.Unit;

        Options = Helper.GetCoreOptionList(question.Options);
    }

}
