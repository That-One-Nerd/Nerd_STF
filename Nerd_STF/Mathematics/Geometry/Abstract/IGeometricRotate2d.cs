namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface IGeometricRotate2d<T> where T : IGeometricRotate2d<T>
{
    public void Rotate(Angle rot);
    public void Rotate(Complex rot);
    public void Rotate(Matrix2x2 rotMatrix);

    public T RotateImmutable(Angle rot);
    public T RotateImmutable(Complex rot);
    public T RotateImmutable(Matrix2x2 rotMatrix);
}
