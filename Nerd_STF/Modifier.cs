namespace Nerd_STF;

public delegate float Modifier(int index, float value);
public delegate T Modifier<T>(int index, T value);
public delegate VT Modifier<IT, VT>(IT index, VT value);
