﻿using Xunit;
using Zorrero.Utils.IntervalLookup.Exceptions;
using Zorrero.Utils.IntervalLookup.Model;

namespace Zorrero.Utils.IntervalLookup.Tests
{
    public class ExclusiveIntervalTests
    {
        [Fact]
        public void GivenIntervalShouldEvaluateContainedIncludingEnd()
        {
            var interval = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            Assert.Equal(IntervalResult.CONTAINED, interval.Evaluate(5, false, true));
        }

        [Fact]
        public void GivenIntervalShouldEvaluateContainedIncludingInit()
        {
            var interval = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            Assert.Equal(IntervalResult.CONTAINED, interval.Evaluate(1, true, false));
        }

        [Fact]
        public void GivenIntervalShouldEvaluateContainedNotIncludingEnd()
        {
            var interval = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            Assert.Equal(IntervalResult.CONTAINED, interval.Evaluate(4, false, false));
        }

        [Fact]
        public void GivenIntervalShouldEvaluateContainedNotIncludingInit()
        {
            var interval = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            Assert.Equal(IntervalResult.CONTAINED, interval.Evaluate(2, false, false));
        }

        //Interval evaluate
        [Fact]
        public void GivenIntervalShouldEvaluateUnderIncludingInit()
        {
            var interval = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            Assert.Equal(IntervalResult.UNDER, interval.Evaluate(0, true, false));
        }

        [Fact]
        public void GivenIntervalShouldEvaluateUnderNotIncludingInit()
        {
            var interval = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            Assert.Equal(IntervalResult.UNDER, interval.Evaluate(1, false, false));
        }

        [Fact]
        public void GivenIntervalShouldEvaluateUpperIncludingEnd()
        {
            var interval = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            Assert.Equal(IntervalResult.UPPER, interval.Evaluate(6, false, true));
        }

        [Fact]
        public void GivenIntervalShouldEvaluateUpperNotIncludingEnd()
        {
            var interval = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            Assert.Equal(IntervalResult.UPPER, interval.Evaluate(5, false, false));
        }

        //Interval creation
        [Fact]
        public void ShouldCreateIntervalCorrectly()
        {
            var interval = new ExclusiveIntervalWithValue<long, string>(1, 2, "");
            Assert.Equal(1, interval.Init);
            Assert.Equal(2, interval.End);
            Assert.Equal("", interval.Value);
        }

        //Interval compare
        [Fact]
        public void ShouldEvaluateAsOverlappedIntervals()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            Assert.Throws<IntervalsOverlappedException>(() => intervalOne.CompareTo(intervalTwo));
        }

        [Fact]
        public void ShouldEvaluateAsOverlappedIntervalContainedInOtherInterval()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(-5, 10, 0);
            Assert.Throws<IntervalsOverlappedException>(() => intervalOne.CompareTo(intervalTwo));
        }

        [Fact]
        public void ShouldEvaluateAsOverlappedIntervalsWithInitAndEndMinor()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(0, 3, 0);
            Assert.Throws<IntervalsOverlappedException>(() => intervalOne.CompareTo(intervalTwo));
        }

        [Fact]
        public void ShouldEvaluateAsMajorIntervalsWithInitAndEndMinorExclusive()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(-5, 0, 0);
            Assert.Equal(1, intervalOne.CompareTo(intervalTwo));
        }

        [Fact]
        public void ShouldEvaluateAsOverlappedIntervalWithInitMinorAndEndEqual()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(0, 5, 0);
            Assert.Throws<IntervalsOverlappedException>(() => intervalOne.CompareTo(intervalTwo));
        }

        [Fact]
        public void ShouldEvaluateAsOverlappedIntervalsWithInitAndEndMajor()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(2, 6, 0);
            Assert.Throws<IntervalsOverlappedException>(() => intervalOne.CompareTo(intervalTwo));
        }

        [Fact]
        public void ShouldEvaluateAsMinorIntervalsWithInitAndEndMajorExclusive()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(6, 10, 0);
            Assert.Equal(-1, intervalOne.CompareTo(intervalTwo));
        }

        [Fact]
        public void ShouldEvaluateAsOverlappedIntervalWithInitEqualAndEndMajor()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(1, 6, 0);
            Assert.Throws<IntervalsOverlappedException>(() => intervalOne.CompareTo(intervalTwo));
        }

        [Fact]
        public void ShouldNotCreateIntervalOnbadParameters()
        {
            Assert.Throws<InvalidIntervalException>(() => new ExclusiveIntervalWithValue<long, long>(2, 1, 0));
        }

        //Interval Equals
        [Fact]
        public void TwoIntervalWithAllDifferentPropertiesShouldNotBeEqual()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(-5, 10, -9);
            Assert.False(intervalOne.Equals(intervalTwo));
        }

        [Fact]
        public void TwoIntervalWithSameInitAndEndAndDifferentValueShouldNotBeEqual()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(1, 5, 1);
            Assert.False(intervalOne.Equals(intervalTwo));
        }

        [Fact]
        public void TwoIntervalWithSamePropertiesShouldBeEqual()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            Assert.True(intervalOne.Equals(intervalTwo));
        }

        [Fact]
        public void TwoIntervalWithSameValueAndDifferentInitAndEndShouldNotBeEqual()
        {
            var intervalOne = new ExclusiveIntervalWithValue<long, long>(1, 5, 0);
            var intervalTwo = new ExclusiveIntervalWithValue<long, long>(-5, 10, 0);
            Assert.False(intervalOne.Equals(intervalTwo));
        }
    }
}