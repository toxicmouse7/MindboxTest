namespace Mindbox.Figures;

public class Rectangle : Figure
{
    private readonly float _a, _b;

    public Rectangle(float a, float b)
    {
        if (a < 0 || b < 0)
            throw new ArgumentException("Sides must be greater or equal than 0");
        
        _a = a;
        _b = b;
    }

    public override float Square() => _a * _b;

    public override float Perimeter() => 2 * _a + 2 * _b;
}