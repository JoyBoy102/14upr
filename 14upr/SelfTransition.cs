using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14upr
{
    public class SelfTransition
    {
        private Dictionary<string, Dictionary<string, List<string>>> statesDict;
        public SelfTransition()
        {
            statesDict = new Dictionary<string, Dictionary<string, List<string>>>
            {
                {"q1_s", new Dictionary<string, List<string>>{ {"0", new List<string>{"q1_s", "0" } }, 
                                                               {"1", new List<string>{"q1_s", "1"} }, 
                                                               {"2", new List<string>{"q3_s", "0"} } } },

                {"q2_s", new Dictionary<string, List<string>>{ {"0", new List<string>{"q2_s", "0" } },
                                                               {"1", new List<string>{"q3_s", "0"} },
                                                               {"2", new List<string>{"q1_s", "1"} } } },

                {"q3_s", new Dictionary<string, List<string>>{ {"0", new List<string>{"q1_s", "0" } },
                                                               {"1", new List<string>{"q1_s", "0"} },
                                                               {"2", new List<string>{"q1_s", "0"} } } },

                {"q4_s", new Dictionary<string, List<string>>{ {"0", new List<string>{"q2_s", "1" } },
                                                               {"1", new List<string>{"q1_s", "0"} },
                                                               {"2", new List<string>{"q2_s", "1"} } } },

                {"q5_s", new Dictionary<string, List<string>>{ {"0", new List<string>{"q2_s", "1" } },
                                                               {"1", new List<string>{"q5_s", "1"} },
                                                               {"2", new List<string>{"q1_s", "1"} } } },



            };
        }

        public string RunSelfTransition(string inputStr, string current_state)
        {
            string result = "";
            foreach (char c in inputStr)
            {
                List<string> resultCharAndState = getNextStepList(c.ToString(), current_state);
                result += resultCharAndState[1];
                current_state = resultCharAndState[0];
            }
            return result;
        }

        private List<string> getNextStepList(string inputNum, string current_state)
        {
            List<string> result = statesDict[current_state][inputNum];
            return result;
        }
    }
}
