using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    
    public interface ISignals
    {
        List<(string dt, bool sg)> xx(List<(string dt, double pr)> D);

        string[] dates { get; set; }

        bool[] signals { get; set; }

    }


    [Serializable()]
    class MavSignals : ISignals

    {
        public string[] dates { get; set; }

        public bool[] signals { get; set; }

        public List<(string dt, bool sg)> xx(List<(string dt, double pr)> D)
        {
            Random R = new Random();

            List<(string dt, bool sg)> Signals = new List<(string, bool)>();

            for (int i = 0; i < 12; i++)
            {
                Signals.Add((D[R.Next(0, D.Count)].dt, false));
                Signals.Add((D[R.Next(0, D.Count)].dt, true));
            }
            return Signals;

        }

    }


    [Serializable()]
    public class MacdSignals : ISignals
    {
        public string[] dates { get; set; }

        public bool[] signals { get; set; }

        public List<(string dt, bool sg)> xx(List<(string dt, double pr)> D)

        {

            Random R = new Random();

            List<(string dt, bool sg)> Signals = new List<(string, bool)>();

            for (int i = 0; i < 12; i++)
            {

                Signals.Add((D[R.Next(0, D.Count)].dt, false));

                Signals.Add((D[R.Next(0, D.Count)].dt, true));

            }
            return Signals;

        }
    }




    [Serializable()]
    public class RsiSignals : ISignals
    {
        public string[] dates { get; set; }

        public bool[] signals { get; set; }

        public List<(string dt, bool sg)> xx(List<(string dt, double pr)> D)
        {
            Random R = new Random();

            List<(string dt, bool sg)> Signals = new List<(string, bool)>();

            for (int i = 0; i < 12; i++)
            {

                Signals.Add((D[R.Next(0, D.Count)].dt, false));

                Signals.Add((D[R.Next(0, D.Count)].dt, true));

            }

            return Signals;
        }



    }

    [Serializable()]
    public class Operations
    {
        ISignals _sg;
        public Operations(ISignals sg)

        {
            _sg = sg;
        }

        public void DoAll()

        {
            //_sg.xx(...);
        }

    }
}
