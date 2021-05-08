using System;

public class RandomEnumGenerator
{
	static Random _R = new Random();
	public static T RandomEnumValue<T>()
	{
		var v = Enum.GetValues(typeof(T));
		return (T)v.GetValue(_R.Next(v.Length));
	}
}