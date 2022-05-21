using FluentAssertions;
using NUnit.Framework;
using Skyscrapers.Code;

namespace SkyScrapers.Test;

[TestFixture]
public class ClueAssignmentTests
{
    
    [Test]
    public void RowCluesWork()
    {
        var sut = new Skyscraper(3,new []
        {
            1,2,3,4,5,6,7,8,9,10,11,12
        });
        
        sut.Rows[0].StartClue.Should().Be(12);
        sut.Rows[0].EndClue.Should().Be(4);
        
        sut.Rows[1].StartClue.Should().Be(11);
        sut.Rows[1].EndClue.Should().Be(5);
        
        sut.Rows[2].StartClue.Should().Be(10);
        sut.Rows[2].EndClue.Should().Be(6);
    }
    
    [Test]
    public void ColumnCluesWork()
    {
        var sut = new Skyscraper(3,new []
        {
            1,2,3,
            4,5,6,
            7,8,9,
            10,11,12
        });
        
        sut.Columns[0].StartClue.Should().Be(1);
        sut.Columns[0].EndClue.Should().Be(9);
        
        sut.Columns[1].StartClue.Should().Be(2);
        sut.Columns[1].EndClue.Should().Be(8);
        
        sut.Columns[2].StartClue.Should().Be(3);
        sut.Columns[2].EndClue.Should().Be(7);
    }
}