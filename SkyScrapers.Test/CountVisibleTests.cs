using System.Runtime.InteropServices;
using FluentAssertions;
using NUnit.Framework;
using Skyscrapers.Code;

namespace SkyScrapers.Test;

[TestFixture]
public class CountVisibleTests
{
    [TestCase(new [] {1,2,3}, 3, 1)]
    [TestCase(new [] {1,2,3,4}, 4, 1)]
    [TestCase(new [] {4, 1,2,3}, 1, 2)]
    [TestCase(new [] {1,2,4,3}, 3, 2)]
    [TestCase(new [] {1,2}, 2, 1)]
    [TestCase(new [] {2,3,4,1}, 3, 2)]
    [TestCase(new [] {1,4,3,2}, 2, 3)]
    [TestCase(new [] {1}, 1, 1)]
    public void HeightCalculatorWorks(int[] value, int heightStart, int heightEnd)
    {
        var (countStart, countEnd) = Util.CountVisible(value);
        countStart.Should().Be(heightStart);
        countEnd.Should().Be(heightEnd);
    }
}