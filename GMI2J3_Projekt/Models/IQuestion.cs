
namespace GMI2J3_Projekt;

public interface IQuestion
{
    bool IsMultipleChoice { get; }
    int NumberOfCorrectChoices { get; }
    Guid QuestionId { get; }
    string QuestionText { get; }
    string Unit { get; }
}