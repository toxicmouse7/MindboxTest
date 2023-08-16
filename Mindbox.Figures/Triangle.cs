namespace Mindbox.Figures;

public class Triangle : Figure
{
    private readonly float[] _sides;
    private readonly float[]? _cathets;
    private float? _cachedSquare;
    public bool IsRightTriangle => _cathets is not null;

    public Triangle(float a, float b, float c)
    {
        _sides = new[] { a, b, c };
        Array.Sort(_sides);

        if (_sides.Any(side => side < 0))
            throw new ArgumentException("Sides must be greater or equal than 0");

        var hypotenuse = _sides.Last();
        _cathets = _sides.Take(2).ToArray();

        if (_cathets.Sum() < hypotenuse)
            throw new ArgumentException("Couldn't create triangle with such sides");

        var isRightTriangle =
            MathF.Pow(hypotenuse, 2) - (MathF.Pow(_cathets[0], 2) + MathF.Pow(_cathets[1], 2)) 
                is < 1e-6f and > -1e-6f;

        _cathets = isRightTriangle ? _cathets : null;
    }

    public override float Square()
    {
        if (_cachedSquare is not null)
            return _cachedSquare.Value;

        if (IsRightTriangle)
        {
            _cachedSquare = _cathets![0] * _cathets[1] / 2;
            return _cachedSquare.Value;
        }

        var halfPerimeter = Perimeter() / 2;
        _cachedSquare = MathF.Sqrt(halfPerimeter
                                   * (halfPerimeter - _sides[0])
                                   * (halfPerimeter - _sides[1])
                                   * (halfPerimeter - _sides[2]));

        return _cachedSquare.Value;
    }

    public override float Perimeter()
        => _sides.Sum();
}