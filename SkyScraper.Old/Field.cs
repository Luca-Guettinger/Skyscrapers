using System.Text;
using SkyScraper.Old.Clue;

namespace SkyScraper.Old;

public class Field
{
    public static readonly Field InvalidField = new Field(new int[0, 0]);

    private readonly int[,] cells;

    public int Size => cells.GetLength(0);

    public Field(Row line, int index)
    {
        cells = new int[line.Length, line.Length];

        for (var i = 0; i < line.Length; i++)
        {
            cells[i, index] = line[i];
        }

    }

    public Field(Column line, int index)
    {
        cells = new int[line.Length, line.Length];

        for (var i = 0; i < line.Length; i++)
        {
            cells[index, i] = line[i];
        }
    }


    public Row GetRow(int r)
    {
        var rowData = new int[this.Size];

        for (var i = 0; i < this.Size; i++)
        {
            rowData[i] = this[i, r];
        }

        return new Row(rowData);
    }
    public Column GetColumn(int r)
    {
        var rowData = new int[this.Size];

        for (var i = 0; i < this.Size; i++)
        {
            rowData[i] = this[r, i];
        }

        return new Column(rowData);
    }

    private Field(int[,] cells)
    {
        this.cells = cells;
    }

    public int this[int x, int y] => cells[x, y];

    public static Field operator +(Field a, Field b)
    {

        if (a == null) throw new ArgumentNullException(nameof(a));
        if (b == null) throw new ArgumentNullException(nameof(b));

        if (a == InvalidField || b == InvalidField)
            throw new InvalidOperationException("Can't add with InvalidField");

        if (a.Size != b.Size)
            throw new InvalidOperationException("Can't add two fields with uneven Size. ");

        if (a == b)
            return a;

        var output = new int[a.Size, a.Size];
        for (var row = 0; row < a.Size; row++)
        {
            for (var column = 0; column < a.Size; column++)
            {
                if (a[row, column] == b[row, column])
                    output[row, column] = a[row, column];
                else
                {
                    if (a[row, column] == 0)
                        output[row, column] = b[row, column];
                    else if (b[row, column] == 0)
                        output[row, column] = a[row, column];
                    else
                        return InvalidField;
                }
            }
        }
        var outputField = new Field(output);
        for (var i = 0; i < a.Size; i++)
        {
            if (outputField.GetColumn(i).ContainsDuplicates() || outputField.GetRow(i).ContainsDuplicates())
                return InvalidField;
        }
        return outputField;
    }

    public static bool operator ==(Field a, Field b)
    {
        return a.Equals(b);
    }
    public static bool operator !=(Field a, Field b)
    {
        return !(a == b);
    }

    public bool isPossible(int x, int y, int number)
    {
        var row = GetRow(y).Values;
        var column = GetColumn(x).Values;
        row[x] = number;
        column[y] = number;
        return !(Line.ContainsDuplicates(row) || Line.ContainsDuplicates(column));
    }

    public Field autoComplete()
    {
        var output = cells;
        for (var x = 0; x < Size; x++)
        {
            for (var y = 0; y < Size; y++)
            {
                if (this[x, y] == 0)
                {
                    var row = GetRow(y);
                    var column = GetColumn(x);
                    var rowNotZero = row.Values.Where(n => n != 0).ToArray();
                    var columNotZero = column.Values.Where(n => n != 0).ToArray();
                    Field tempField;

                    if (rowNotZero.Length != 0)
                    {
                        if (Size - rowNotZero.Count() == 1)
                        {
                            output[x, y] = ClueManager.Array.Except(rowNotZero).ToArray()[0];
                        }
                        if (Size - rowNotZero.Count() == 2)
                        {
                            var v = ClueManager.Array.Except(rowNotZero).ToArray();
                            var bool1 = isPossible(x, y, v[0]);
                            var bool2 = isPossible(x, y, v[1]);
                            int x2 = -1;
                            for (var i = 0; i < row.Length; i++)
                            {
                                if (cells[i, y] == 0 && i != x)
                                {
                                    x2 = i;
                                }
                            }

                            if (x2 == -1)
                                continue;
                            var bool3 = isPossible(x2, y, v[0]);
                            var bool4 = isPossible(x2, y, v[1]);

                            if (bool1 && !bool2)
                            {
                                output = getOutput(x, y, v[0], output);
                                output = getOutput(x2, y, v[1], output);
                            }
                            else if (bool2 && !bool1)
                            {
                                output = getOutput(x2, y, v[0], output);
                                output = getOutput(x, y, v[1], output);
                            }
                            else if (bool3 && !bool4)
                            {
                                output = getOutput(x2, y, v[0], output);
                                output = getOutput(x, y, v[1], output);
                            }
                            else if (bool4 && !bool3)
                            {
                                output = getOutput(x, y, v[0], output);
                                output = getOutput(x2, y, v[1], output);
                            }
                            tempField = new Field(output);
                            rowNotZero = tempField.GetRow(y).Values.Where(n => n != 0).ToArray();
                            continue;
                        }
                    }
                    if (columNotZero.Length != 0)
                    {
                        if (Size - columNotZero.Count() == 1)
                        {
                            output[x, y] = ClueManager.Array.Except(columNotZero).ToArray()[0];
                        }

                        if (Size - columNotZero.Count() == 2)
                        {
                            var v = ClueManager.Array.Except(columNotZero).ToArray();
                            var bool1 = isPossible(x, y, v[0]);
                            var bool2 = isPossible(x, y, v[1]);
                            int y2 = -1;
                            for (var i = 0; i < row.Length; i++)
                            {
                                if (cells[x, i] == 0 && i != x)
                                {
                                    y2 = i;
                                }
                            }
                            if (y2 == -1)
                                continue;
                            var bool3 = isPossible(x, y2, v[0]);
                            var bool4 = isPossible(x, y2, v[1]);

                            if (bool1 && !bool2)
                            {
                                output = getOutput(x, y, v[0], output);
                                output = getOutput(x, y2, v[1], output);
                            }
                            else if (bool2 && !bool1)
                            {
                                output = getOutput(x, y, v[0], output);
                                output = getOutput(x, y2, v[1], output);
                            }
                            else if (bool3 && !bool4)
                            {
                                output = getOutput(x, y2, v[0], output);
                                output = getOutput(x, y, v[1], output);
                            }
                            else if (bool4 && !bool3)
                            {
                                output = getOutput(x, y, v[0], output);
                                output = getOutput(x, y2, v[1], output);
                            }
                            tempField = new Field(output);
                            columNotZero = tempField.GetColumn(x).Values.Where(n => n != 0).ToArray();
                        }
                    }
                }
            }
        }
        return new Field(output);
    }

    private int[,] getOutput(int x, int y, int number, int[,] output)
    {
        if (isPossible(x, y, number))
            output[x, y] = number;
        if (isPossible(x, y, number))
            output[x, y] = number;
        return output;
    }

    public override string ToString()
    {
        var s = new StringBuilder();
        for (var y = 0; y < Size; y++)
        {
            if (y != 0)
                s.Append(" ");
            for (var x = 0; x < Size; x++)
            {
                s.Append(cells[x, y]);
            }
        }
        return s.ToString();
    }
    public override bool Equals(object obj)
    {
        if (!(obj is Field))
            return false;

        Field otherField = (Field)obj;

        if (otherField.Size != this.Size)
            return false;

        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                if (this.cells[x, y] != otherField[x, y])
                    return false;
            }
        }
        return true;
    }

    public override int GetHashCode()
    {
        var output = 0;

        var i = 1;
        foreach (var cell in cells)
        {
            output += i * cell;
            i++;
        }
        return output;
    }
}