using System;

namespace Entity{
    public class Option
    {
        public string Text;

        // delegateについて
        //public delegate void OnCompleteDelegate(string html);
        //public event OnCompleteDelegate CompleteHandler;
        // public event Action<string> CompleteHandler; //上の２行を書き換えた

        // System.Action
        public Action Action;

        public Func<bool> IsFlagOK = () => { return true; };
    }
}


