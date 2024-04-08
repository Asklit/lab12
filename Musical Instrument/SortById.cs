using System;
using System.Collections;
using Musical_Instrument;

namespace Musical_Instruments
{
	public class SortById : IComparer
	{
		/// <summary>
		/// Сравнение двух объектов с помощью IComparer
		/// </summary>
		/// <param name="x">Первый объект</param>
		/// <param name="y">Второй объект</param>
		/// <returns></returns>
		public int Compare(object x, object y)
		{
			
			MusicalInstrument mi1 = x as MusicalInstrument;
			MusicalInstrument mi2 = y as MusicalInstrument;
			if (mi1 == null && mi2 == null) return 0;
			if (mi1 == null) return -1;
			if (mi2 == null) return 1;
			if (mi1.Id.Number < mi2.Id.Number) return -1;
			else if (mi1.Id.Number == mi2.Id.Number) return 0;
			return 1;
		}
	}
}

