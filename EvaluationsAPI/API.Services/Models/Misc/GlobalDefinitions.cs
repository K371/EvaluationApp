using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Models.Misc
{
	/// <summary>
	/// Class that holds global definitions
	/// </summary>
	public static class GlobalDefinitions
	{
		#region Course Grade Types

		// 1 = Lokið, 6 = Fallinn, 7 = Staðið, 9 = Fallinn mætti ekki
		public static List<int> allTakenCourseGradeTypes()
		{
			return new List<int> { 1, 6, 7, 9 };
		}

		// 4 = Metið
		public static List<int> allEvaluatedCourseGradeTypes()
		{
			return new List<int> { 4 };
		}

		// 1 = Lokið (Með einkunn)
		public static int courseCompletedWithGrade()
		{
			return 1;
		}

		// 1 = Lokið, 7 = Staðið
		public static List<int> allCompletedCourseGradeTypes()
		{
			return new List<int> { 1, 4, 7 };
		}

		// 2 = Úrsögn, 3 = Hættur, 5 = Veikur, 6 = Fallinn, 8 = Úrelt
		// 9 = Fallinn mætti ekki, 10 = Ógilt, 13 = Árekstur prófa, 14 = Ólokið
		public static List<int> allRemainingCourseGradeTypes()
		{
			return new List<int> { 2, 3, 5, 6, 8, 9, 10, 13, 14 };
		}

		#endregion
	}
}
