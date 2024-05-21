using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt_Test;
public class Test_QuestionReaderFromFile
{
    [Fact]
    public void LoadQuestions_ShouldReturnAllQuestions()
    {
        // Arrange
        string testData = TestDataGenerator.GetTestDataString();
        var fileReader = new Mock<IFileReader>();
        fileReader.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(testData);
        QuestionReaderFromFile reader = new QuestionReaderFromFile(fileReader.Object);
     
        

        // Act
        var result = reader.GetQuestions();

        // Assert
        Assert.Equal(25, result.Count);
    }

    [Fact]
    public void LoadQuestions_NoJson_ShouldThrowException()
    {
        // Arrange
        string emptyString = "";
        var fileReader = new Mock<IFileReader>();
        fileReader.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(emptyString);

        // Act, Assert
        Assert.Throws<Exception>(() => new QuestionReaderFromFile(fileReader.Object));
    }
}
