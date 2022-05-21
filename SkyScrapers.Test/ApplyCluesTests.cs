using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Skyscrapers.Code;

namespace SkyScrapers.Test;

[TestFixture]
public class ApplyCluesTests
{
    [Test]
    public void SimpleTest1()
    {
        var skyscrapers = new Skyscraper(3, new []
        {
            2,0,0,
            0,0,0,
            0,0,0,
            0,0,0,
        });
        
        skyscrapers.ApplyAllowedOptions();


        skyscrapers.Rows[0].ToArray()[0].AllowedHeights.Should().HaveCount(2);
        skyscrapers.Rows[0].ToArray()[0].AllowedHeights[0].Should().Be(1);
        skyscrapers.Rows[0].ToArray()[0].AllowedHeights[1].Should().Be(2);
    }
    [Test]
    public void SimpleTest2()
    {
        var skyscrapers = new Skyscraper(3, new []
        {
            1,0,0,
            0,0,0,
            0,0,0,
            0,0,0,
        });
        
        skyscrapers.ApplyAllowedOptions();


        skyscrapers.Rows[0].ToArray()[0].AllowedHeights.Should().HaveCount(1);
        skyscrapers.Rows[0].ToArray()[0].AllowedHeights[0].Should().Be(1);
        skyscrapers.Rows[0].ToArray()[0].AllowedHeights[1].Should().Be(2);
    }
}