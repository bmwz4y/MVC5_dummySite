using System;

namespace dummy1
{
    public static class Class1
    {
        public static string Sentence
            => "WHAT A SENTENCE: current time in hh:mm:ss is " + DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
    }
}