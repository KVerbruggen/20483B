using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades.DataModel
{
    public partial class Teacher
    {
        private const int MAX_CLASS_SIZE = 8;

        public void EnrollInClass(Student newStudent)
        {
            int studentCount = (from student in Students
                                where student.TeacherUserId == UserId
                                select student).Count();
            // Or just call Students.Count(), but the exercise didn't want that

            if (studentCount >= MAX_CLASS_SIZE)
            {
                throw new ClassFullException();
            }

            if (newStudent.TeacherUserId == null)
            {
                newStudent.TeacherUserId = UserId;
            }
            else
            {
                throw new ArgumentException();
            }

        }
    }
}
