using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity{
    class Scenario
    {
        public string ScenarioID;
        public List<string> Texts = new List<string>();
        public List<Option> Options = new List<Option>();
        public string NextScenarioID;
    }
}