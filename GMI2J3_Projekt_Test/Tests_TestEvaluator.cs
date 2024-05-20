using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt_Test;

public class Tests_TestEvaluator
{
    [Fact]
    public void EvaluateTest_ReturnsAllCorrectTestScore()
    {
        // Arrange
        var questionProvider = new Mock<IQuestionProvider>();
        var testEvaluator = new TestEvaluator(questionProvider.Object);
        var answers = new Dictionary<Guid, List<Guid>>();
        
        var question = new PracticeQuestion(Guid.NewGuid(), "Question 1", [new(Guid.NewGuid(), "Option 1", true)], "1.1");
        
        
        questionProvider.Setup(x => x.GetQuestionById(It.IsAny<Guid>())).Returns(question);

        answers.Add(question.QuestionId, [question.Options.First().OptionId]);

        // Act
        var result = testEvaluator.EvaluateTest(answers);

        // Assert
        Assert.Equal(1f, result.GetScore());
    }

    [Fact]
    public void EvaluateTest_ReturnsAllIncorrectTestScore()
    {
        // Arrange
        var questionProvider = new Mock<IQuestionProvider>();
        var testEvaluator = new TestEvaluator(questionProvider.Object);
        var answers = new Dictionary<Guid, List<Guid>>();

        var question = new PracticeQuestion(Guid.NewGuid(), "Question 1", [new(Guid.NewGuid(), "Option 1", true)], "1.1");
        questionProvider.Setup(x => x.GetQuestionById(It.IsAny<Guid>())).Returns(question);

        answers.Add(question.QuestionId, [Guid.NewGuid()]);

        // Act
        var result = testEvaluator.EvaluateTest(answers);

        // Assert
        Assert.Equal(0f, result.GetScore());
    }

    [Fact]
    public void EvaluateTest_WithMultipleChoiceQuestion_CorrectAnswer()
    {
        // Arrange
        var q1Id = Guid.NewGuid();

        var o1Id = Guid.NewGuid();
        var o2Id = Guid.NewGuid();
        var o3Id = Guid.NewGuid();

        var questionProvider = new Mock<IQuestionProvider>();
        var testEvaluator = new TestEvaluator(questionProvider.Object);
        var answers = new Dictionary<Guid, List<Guid>>();

        var question = new PracticeQuestion(q1Id, "Q1", [new(o1Id, "O1", true), new(o2Id, "O1", true), new(o3Id, "O1", false)], "1.1");

        questionProvider.Setup(x => x.GetQuestionById(q1Id)).Returns(question);
        answers.Add(q1Id, [o1Id, o2Id]);

        // Act
        var result = testEvaluator.EvaluateTest(answers);

        // Assert
        Assert.Equal(1f, result.GetScore());
    }

    [Fact]
    public void EvaluateTest_WithMultipleChoiceQuestion_IncorrectAnswer()
    {
        // Arrange
        var q1Id = Guid.NewGuid();

        var o1Id = Guid.NewGuid();
        var o2Id = Guid.NewGuid();
        var o3Id = Guid.NewGuid();

        var questionProvider = new Mock<IQuestionProvider>();
        var testEvaluator = new TestEvaluator(questionProvider.Object);
        var answers = new Dictionary<Guid, List<Guid>>();

        var question = new PracticeQuestion(q1Id, "Q1", [new(o1Id, "O1", true), new(o2Id, "O1", true), new(o3Id, "O1", false)], "1.1");

        questionProvider.Setup(x => x.GetQuestionById(q1Id)).Returns(question);
        answers.Add(q1Id, [o1Id, o3Id]);

        // Act
        var result = testEvaluator.EvaluateTest(answers);

        // Assert
        Assert.Equal(0f, result.GetScore());
    }
}
