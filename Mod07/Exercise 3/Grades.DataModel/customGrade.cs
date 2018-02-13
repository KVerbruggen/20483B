using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Grades.DataModel
{
    public partial class Grade
    {
        public bool ValidateAssessmentDate(DateTime assessmentDate)
        {
            if (assessmentDate > DateTime.Now.Date)
            {
                throw new ArgumentOutOfRangeException();
            }
            return true;
        }

        public bool ValidateAssessmentGrade(string assessment)
        {
            Match matchGrade = Regex.Match(assessment, @"^[A-E][+-]?$");
            if (!matchGrade.Success)
            {
                throw new ArgumentOutOfRangeException();
            }
            return true;
        }
    }
}
