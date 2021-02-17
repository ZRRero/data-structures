using System;
using Zorrero.Utils.IntervalLookup.Exceptions;

namespace Zorrero.Utils.IntervalLookup.Model
{
    public class ExclusiveIntervalWithValue<T, TK> : IntervalWithValue<T, TK> where T : IComparable<T>
    {

        public ExclusiveIntervalWithValue(T init, T end, TK value) : base(init, end, value)
        {
        }

        public override int CompareTo(IntervalWithValue<T, TK> interval)
        {
            var other = (ExclusiveIntervalWithValue<T, TK>) interval;
            if (Init.CompareTo(other.End) > 0) return 1;
            if (End.CompareTo(other.Init) < 0) return -1;
            throw new IntervalsOverlappedException();
        }
    }
}