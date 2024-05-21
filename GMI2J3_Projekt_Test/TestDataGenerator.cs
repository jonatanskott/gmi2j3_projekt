using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GMI2J3_Projekt_Test;
static internal class TestDataGenerator
{
    // Generates 10 example PracticeQuestions
    static internal string GetTestDataString()
    {
        Dictionary<string, List<PracticeQuestion>> data = [];

        data.Add("questions", new List<PracticeQuestion>());

        for (int i = 0; i < 25; i++)
        {
            List<PracticeOption> options = new();

            for (int j = 0; j < 4; j++)
            {
                bool shouldBeCorrect = (j == 0 || (i % 3 == 0 && j == 1));
                options.Add(new(Guid.NewGuid(), $"o{j} {(shouldBeCorrect ? 'c':' ')}", shouldBeCorrect));
            }
            data["questions"].Add(new(Guid.NewGuid(), $"Q{i}", options, $"{i%5}.{i%6}.{i % 15}"));
        }

        return JsonConvert.SerializeObject(data);
    }

    static internal List<PracticeQuestion> GetTestDataList()
    {
        List<PracticeQuestion> data = [];


        for (int i = 0; i < 25; i++)
        {
            List<PracticeOption> options = new();

            for (int j = 0; j < 4; j++)
            {
                bool shouldBeCorrect = (j == 0 || (i % 3 == 0 && j == 1));
                options.Add(new(Guid.NewGuid(), $"o{j} {(shouldBeCorrect ? 'c' : ' ')}", shouldBeCorrect));
            }
            data.Add(new(Guid.NewGuid(), $"Q{i}", options, $"{i % 5}.{i % 6}.{i % 15}"));
        }

        return data;
    }
}
