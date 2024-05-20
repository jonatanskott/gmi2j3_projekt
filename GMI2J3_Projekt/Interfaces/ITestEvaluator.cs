using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public interface ITestEvaluator
{
    public TestResult EvaluateTest(Dictionary<Guid, List<Guid>> answers);
}
