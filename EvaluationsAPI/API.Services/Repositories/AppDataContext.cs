using System.Data.Entity;
using API.Services.Models.Entities.Customers;
using API.Services.Models.Entities.Evaluations;
using API.Services.Models.Entities.General;
using API.Services.Models.Entities.Courses;
using API.Services.Models.Mapping.Courses;
using API.Services.Models.Mapping.Customers;
using API.Services.Models.Mapping.Evaluations;
using API.Services.Models.Mapping.General;

namespace API.Services.Repositories
{
	public class AppDataContext : DbContext, IDbContext
	{
		#region Course related tables
		public DbSet<CourseInstance> CourseInstances { get; set; }
		public DbSet<CourseInstanceStudent> CoursesInstancesStudent { get; set; }
		public DbSet<TeachersRegistration> TeachersRegistration { get; set; }
		#endregion

		#region Evaluation tables
		public DbSet<EvaluationTemplate> EvaluationTemplates { get; set; }
		public DbSet<EvaluationInstance> EvaluationInstances { get; set; }
		public DbSet<EvaluationQuestion> EvaluationQuestions { get; set; }
		public DbSet<EvaluationQuestionValue> EvaluationQuestionValues { get; set; }
		public DbSet<EvaluationAnswer> EvaluationAnswers { get; set; }
		public DbSet<EvaluationReply> EvaluationReplies { get; set; }

		#endregion

		#region Other tables
		public DbSet<User> Users { get; set; }
		#endregion

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new CourseInstanceMap());
			modelBuilder.Configurations.Add(new CourseInstanceStudentMap());
			modelBuilder.Configurations.Add(new TeachersRegistrationMap());
			modelBuilder.Configurations.Add(new UserMap());

			modelBuilder.Configurations.Add(new EvaluationTemplateMap());
			modelBuilder.Configurations.Add(new EvaluationInstanceMap());
			modelBuilder.Configurations.Add(new EvaluationQuestionMap());
			modelBuilder.Configurations.Add(new EvaluationQuestionValueMap());
			modelBuilder.Configurations.Add(new EvaluationReplyMap());
			modelBuilder.Configurations.Add(new EvaluationAnswerMap());
		}

		public AppDataContext()
		{
			//SERIALIZE WILL FAIL WITH PROXIED ENTITIES
			Configuration.ProxyCreationEnabled = false;

			//ENABLING COULD CAUSE ENDLESS LOOPS AND PERFORMANCE PROBLEMS
			Configuration.LazyLoadingEnabled = false;
		}
	}
}