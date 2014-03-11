using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Services.Helpers
{
	public class Helper
	{
		/// <summary>
		/// Checks if SSN is legal or not
		/// </summary>
		/// <param name="ssn">Social Security number in string format</param>
		/// <returns>true if legitmate, false otherwise</returns>
		public static bool CheckSSN(string ssn)
		{
			if (ssn.Length != 10) return false;
			if (string.IsNullOrEmpty(ssn)) return false;
			var ssnArr = ssn.ToCharArray();
			//Check if legimate SSN by checking the century
			if (!(ssnArr[9] == '9' || ssnArr[9] == '0')) return false;
			//Calculate the 9th digit checksum
			var checkArr = new [] { 3, 2, 7, 6, 5, 4, 3, 2 };
			var checkSum = 0;
			for (int i = 0; i < 8; i++)
			{
				checkSum += (int)Char.GetNumericValue(ssnArr[i]) * checkArr[i];
			}
			int checkNum = 11 - (checkSum % 11);
			return ((int)Char.GetNumericValue(ssnArr[8]) == checkNum);
		}

		/// <summary>
		/// This method scrubs data of type IEnumerable of all NULL results in string properties
		/// and converts them to string.empty or ""
		/// Note: Use with caution and only if the desired effect is to have no null's.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns></returns>
		public static IEnumerable<T> Scrub<T>(IEnumerable<T> data)
		{
// ReSharper disable PossibleMultipleEnumeration
			if (!data.Any())
			{
				return data;
			}
			//Get properties of Class
			var modelProperties = typeof(T).GetProperties();

			//Loop trough each result and scrubb nulls from result and replace with empty string
			foreach (var item in data)
			{
				foreach (var property in modelProperties)
				{
					if (property.PropertyType != typeof(string) || item == null)
					{
						continue;
					}
					if ((string)property.GetValue(item, null) == null)
					{
						property.SetValue(item, "", null);
					}
				}
			}
			return data;
// ReSharper restore PossibleMultipleEnumeration
		}

		/// <summary>
		/// Given a course instance string
		/// This function parses parameters that are not usually allowed in the url
		/// Example, + is not allowed so we encode it into {plus}
		/// </summary>
		/// <param name="courseInstance">Short name of a course</param>
		/// <returns></returns>
		public static string ParseCourseInstance(string courseInstance)
		{
			if (courseInstance != null)
			{
				return courseInstance.Replace("{plus}", "+");
			}
			return "";
		}

		/// <summary>
		/// Convert string of file extensions to List of file extensions.
		/// If the string is empty, this function returns null.
		/// </summary>
		/// <param name="fileExt">String of file extensions in format *.exe;*.pdf</param>
		/// <returns>Returns list of file extensions</returns>
		public static IEnumerable<string> SplitFileExtensionToList(string fileExt)
		{
			if (fileExt.Length == 0)
			{
				return null;
			}

			fileExt = fileExt.Replace("*.", string.Empty);

			var length = fileExt.Length;
			// If fileExt string has ; at end, then remove it to prevent extra empty value in array when doing split
			if (fileExt.Substring(length - 1).Equals(";"))
			{
				fileExt = fileExt.Substring(0, length - 1);
			}

			IEnumerable<string> extList = fileExt.Split(';');

			return extList;
		}
	}
}
