using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public static class Helper
{
    public static ICollection<TestOption> GetCoreOptionList(ICollection<PracticeOption> o)
    {
        List<TestOption> tempList = new List<TestOption>(); 
        foreach (PracticeOption option in o.Cast<PracticeOption>())
        {
            tempList.Add(new TestOption(option));
        }

        return tempList.OrderBy(x => Random.Shared.Next()).ToList();

    }
}
