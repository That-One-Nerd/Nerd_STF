namespace Nerd_STF;

public delegate float Modifier2d(Int2 index, float value);
public delegate T Modifier2d<T>(Int2 index, T value);
public delegate VT Modifier2d<IT, VT>(IT x, IT y, VT value);
