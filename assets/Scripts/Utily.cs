/*
 * Classe contenente diversi metodi statici utili per la programmazione
 * */

public class Utily 
{

	/*
	 * Trasforma un booleano in un intero: se è true ritorna 1 se è false ritorna 0
	 * */
	public static int BooleanToInt(bool boolean)
	{
		if (boolean)
		{
			return 1;
		}
		else
		{
			return -1;
		}
	}

	/*
	 * Trasforma un float in un booleano: se è superiore a 0 ritorna true, se è inferiore a 0 ritorna false
	 * */
	public static bool FloatToBoolean(float one)
	{
		if (one > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	/*
	 * Trasforma un puntatore ad un oggetto in un booleano, se esso è nullo ritorna false, altrimenti true
	 * */
	public static bool ObjectToBoolean(System.Object obj)
	{
		if (obj != null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
