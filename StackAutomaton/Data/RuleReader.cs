using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAutomaton.Data
{
    public class RuleReader
    {
        public string[][] Rules;
        private Dictionary<string, string> computedRules = new Dictionary<string, string>();

        public Dictionary<string, string> ComputedRules { get => computedRules; private set => computedRules = value; }
        

        public async Task ReadFile(IBrowserFile file)
        {
            Stream stream = file.OpenReadStream();
            StreamReader streamReader = new StreamReader(stream);
            string content = await streamReader.ReadToEndAsync();
            streamReader.Close();
            stream.Close();
            string[][] retVal = content.Split("\n").Select(l => l.Split(';').ToArray()).ToArray();
            this.Rules = retVal;
            GeneratedRules();
        }

        private void GeneratedRules()
        {
            for (int i = 1; i < Rules.GetLength(0); i++)
            {
                string mainRule = Rules[i][0];
                for (int j = 1; j < Rules[i].Length; j++)
                {
                    string ruleKey = mainRule + Rules[0][j];
                    ComputedRules[ruleKey] = Rules[i][j];
                }
            }
        }
    }
}
