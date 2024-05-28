using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt_Test;
public class Tests_QuizService
{

    [Fact]
    public void StartTestQuiz_CallGetCoreQuestionSetWithNull()
    {
        var q = new Mock<IQuestionProvider>();
        q.Setup(x => x.GetCoreQuestionSet(It.IsAny<int>(), It.IsAny<string>()));
        var quizService = new QuizService(q.Object, Mock.Of<ITestEvaluator>());

        quizService.StartTestQuiz(1, null);

        q.Verify(x => x.GetCoreQuestionSet(1, string.Empty), Times.Once());
    }

    [Fact]
    public void StartTestQuiz_CallGetCoreQuestionsSetWithUnit()
    {
        int numOfQuestions = 1;    
        string unit = "1.2";

        var q = new Mock<IQuestionProvider>();
        q.Setup(x => x.GetCoreQuestionSet(It.IsAny<int>(), It.IsAny<string>()));
        var quizService = new QuizService(q.Object, Mock.Of<ITestEvaluator>());

        quizService.StartTestQuiz(numOfQuestions, unit);

        q.Verify(x => x.GetCoreQuestionSet(numOfQuestions, unit), Times.Once());

    }

    [Fact]
    public void StartPracticeQuiz_CallGetFullQuestionSetWithUnit()
    {
        int numOfQuestions = 1;
        string unit = "1.2";

        var q = new Mock<IQuestionProvider>();
        q.Setup(x => x.GetFullQuestionSet(It.IsAny<int>(), It.IsAny<string>()));
        var quizService = new QuizService(q.Object, Mock.Of<ITestEvaluator>());

        quizService.StartPracticeQuiz(numOfQuestions, unit);

        q.Verify(x => x.GetFullQuestionSet(numOfQuestions, unit), Times.Once());

    }

    [Fact]
    public void SubmitQuiz_ShouldCallTestEvalutator()
    {
        var mockTestEvaluator = new Mock<ITestEvaluator>();
        mockTestEvaluator.Setup(x => x.EvaluateTest(It.IsAny<Dictionary<Guid, List<Guid>>>())).Returns(new TestResult(1,1));
    
        var quizService = new QuizService(Mock.Of<IQuestionProvider>(), mockTestEvaluator.Object);

        var answer = new Dictionary<Guid, List<Guid>> { { Guid.NewGuid(), [Guid.NewGuid()] } };


        quizService.SubmitQuiz(answer);

        mockTestEvaluator.Verify(x => x.EvaluateTest(answer), Times.Once());
    }
}
