using API.Models.DTO.Courses;
using API.Services.Models.Entities.Courses;

namespace API.Services.Helpers
{
	/// <summary>
	/// This class should contain the functions which take care of mapping between
	/// Entity classes and DTO classes. NOTE: we might want to switch to some other
	/// structure later on, this could easily get very messy!
	/// </summary>
	public static class Mappers
	{
		public static CourseInstanceDTO ToDTO(this CourseInstance ci)
		{
			return new CourseInstanceDTO
			{
				ID       = ci.ID,
				CourseID = ci.CourseID,
				NameIS    = ci.NameIS,
				NameEN    = ci.NameEN,
				DateBegin = ci.DateBegin,
				DateEnd = ci.DateEnd
			};
		}
	}
}
