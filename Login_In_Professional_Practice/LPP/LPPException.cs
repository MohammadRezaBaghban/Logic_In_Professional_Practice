using System;

namespace LPP
{
    public class LPPException : Exception
    {
        public LPPException(string error) : base(error)
        {
            Console.WriteLine($"Exception: {error} {this.Source}");
        }
    }
}