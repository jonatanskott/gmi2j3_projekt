using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt_Test;
public class Tests_QuestionProvider
{
    [Fact]
    public void GetCoreQuestionSet_ReturnsOneQuestions()
    {
        // Arrange
        var questionReader = new Mock<IQuestionReader>();
        var questionProvider = new QuestionProvider(questionReader.Object);
        var questions = new List<PracticeQuestion>();

        for(int i = 1; i <= 10; i++) 
        {
            questions.Add(new(Guid.NewGuid(), $"Question {i}", [new PracticeOption(Guid.NewGuid(), "Option 1", true)], $"1.{i}"));
        }

        questionReader.Setup(x => x.GetQuestions()).Returns(questions);

        // Act
        var result = questionProvider.GetCoreQuestionSet(1, "1");

        // Assert
        Assert.Single(result.Questions);
    }

    [Fact]
    public void GetCoreQuestionSet_ReturnsMaxQuestions()
    {
        int maxQuestions = 20;

        var questionReader = new Mock<IQuestionReader>();
        var questionProvider = new QuestionProvider(questionReader.Object);
        var questions = new List<PracticeQuestion>();

        for (int i = 1; i <= 30; i++)
        {
            questions.Add(new(Guid.NewGuid(), $"Question {i}", [new PracticeOption(Guid.NewGuid(), "Option 1", true)], $"1.{i}"));
        }

        questionReader.Setup(x => x.GetQuestions()).Returns(questions);

        // Act
        var result = questionProvider.GetCoreQuestionSet(maxQuestions, "1");

        // Assert
        Assert.Equal(maxQuestions, result.Questions.Count);
    }

    [Fact]
    public void GetCoreQuestions_TooMany_ShouldFail()
    {
        int maxQuestions = 20;

        var questionReader = new Mock<IQuestionReader>();
        var questionProvider = new QuestionProvider(questionReader.Object);
        var questions = new List<PracticeQuestion>();

        for (int i = 1; i <= 30; i++)
        {
            questions.Add(new(Guid.NewGuid(), $"Question {i}", [new PracticeOption(Guid.NewGuid(), "Option 1", true)], $"1.{i}"));
        }

        questionReader.Setup(x => x.GetQuestions()).Returns(questions);

        Assert.Throws<Exception>(() => questionProvider.GetCoreQuestionSet(maxQuestions + 1, "1"));
    }
}
