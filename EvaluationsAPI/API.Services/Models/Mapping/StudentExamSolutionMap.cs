using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Services.Models.Entities.Exams;

namespace API.Services.Models.Mapping
{
    public class StudentExamSolutionMap : EntityTypeConfiguration<StudentExamSolution>
    {
        public StudentExamSolutionMap()
        {
            this.ToTable("Exams_StudentSolutions");

            this.HasKey(t => t.SolutionID);

            this.Property(t => t.SolutionID).HasColumnName("ID_Solution");
            this.Property(t => t.StudentID).HasColumnName("ID_Student");
            this.Property(t => t.QuestionID).HasColumnName("ID_Question");
            this.Property(t => t.InstanceID).HasColumnName("ID_Instance");
            this.Property(t => t.AnswerID).HasColumnName("ID_Answer");
            this.Property(t => t.SolutionScore).HasColumnName("dSolutionScore");
            this.Property(t => t.SolutionText).HasColumnName("sSolutionText");
            this.Property(t => t.SolutionComment).HasColumnName("sSolutionComment");
            this.Property(t => t.Insert).HasColumnName("dtInsert");
            this.Property(t => t.Update).HasColumnName("dtUpdate");
            this.Property(t => t.Delete).HasColumnName("dtDelete");
        }
    }
}


//this.Property(t => t.ID).HasColumnName("ID_Exam");