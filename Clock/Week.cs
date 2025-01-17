using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Clock
{
	public class Week
	{
		public static readonly string[] Weekdays = new string[] { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };

		[JsonIgnore]
		private byte week;

		[JsonProperty("WeekDays")]
		public bool[] WeekDays
		{
			get => ExtractWeekDays();
			set => CompressWeekDays(value);
		}

		public Week()
		{
			week = 127;
		}
		public Week(bool[] days)
		{
			CompressWeekDays(days);
		}
		//public void SetDay(int day)
		//{
		//	if (day > 6) return;
		//	week |= (byte)(1 << day);
		//}
		public void CompressWeekDays(bool[] days)
		{
			week = 0;
			for (byte i = 0; i < days.Length; i++)
			{
				if (days[i]) week |= (byte)(1 << i);
			}
		}
		public bool[] ExtractWeekDays()
		{
			bool[] weekDays = new bool[7];
			for (byte i = 0; i < 7; i++)
			{
				weekDays[i] = (week & (1 << i)) != 0;
			}
			return weekDays;
		}
		public bool Contains(DayOfWeek day)
		{
			int i_day = (int)day;
			i_day -= 1;
			if (i_day == -1) i_day = 6;
			return (week & (1<<i_day)) != 0;
		}
		public override string ToString()
		{
			string weekdays = "";
			for (byte i = 0; i < Weekdays.Length; i++)
			{
				if (((1 << i) & week) != 0)
					weekdays += $"{Weekdays[i]},";
			}
			return weekdays.TrimEnd(',');
		}
	}
}