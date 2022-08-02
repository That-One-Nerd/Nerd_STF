namespace Nerd_STF;

public delegate float Modifier2D(Int2 index, float value);
public delegate T Modifier2D<T>(Int2 index, T value);
public delegate VT Modifier2D<IT, VT>(IT x, IT y, VT value);
