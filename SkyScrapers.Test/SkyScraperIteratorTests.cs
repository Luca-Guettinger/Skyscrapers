using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Skyscrapers.Code;

namespace SkyScrapers.Test;

[TestFixture]
public class SkyScraperIteratorTests
{
    [Test]
    public void RowEnumeratorWorks()
    {
        var sut = new Skyscraper(new Position[,]
        {
            {new(1), new(2), new(3)},
            {new(4), new(5), new(6)},
            {new(7), new(8), new(9)}
        },new int[12]);
        

        GetRow(sut, 0 , 0).Should().Be(1);
        GetRow(sut, 0 , 1).Should().Be(2);
        GetRow(sut, 0 , 2).Should().Be(3);
        
        GetRow(sut, 1 , 0).Should().Be(4);
        GetRow(sut, 1 , 1).Should().Be(5);
        GetRow(sut, 1 , 2).Should().Be(6);
        
        GetRow(sut, 2 , 0).Should().Be(7);
        GetRow(sut, 2 , 1).Should().Be(8);
        GetRow(sut, 2 , 2).Should().Be(9);
    }
    
    
    [Test]
    public void ColumnEnumeratorWorks()
    {
        var sut = new Skyscraper(new Position[,]
        {
            {new(1), new(2), new(3)},
            {new(4), new(5), new(6)},
            {new(7), new(8), new(9)}
        }, new int[12]);

        
        GetColumn(sut, 0 , 0).Should().Be(1);
        GetColumn(sut, 0 , 1).Should().Be(4);
        GetColumn(sut, 0 , 2).Should().Be(7);
        
        GetColumn(sut, 1 , 0).Should().Be(2);
        GetColumn(sut, 1 , 1).Should().Be(5);
        GetColumn(sut, 1 , 2).Should().Be(8);
        
        GetColumn(sut, 2 , 0).Should().Be(3);
        GetColumn(sut, 2 , 1).Should().Be(6);
        GetColumn(sut, 2 , 2).Should().Be(9);
    }

    private static byte GetColumn(Skyscraper sut, int c, int i)
    {
        return sut.Columns[c].ToArray()[i].AllowedHeights.Single();
    }
    private static byte GetRow(Skyscraper sut, int c, int i)
    {
        return sut.Rows[c].ToArray()[i].AllowedHeights.Single();
    }
}