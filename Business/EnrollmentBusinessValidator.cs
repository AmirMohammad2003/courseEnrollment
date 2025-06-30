using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Exceptions;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Service;
using SystemGroup.General.CourseEnrollment.Common;


namespace SystemGroup.General.CourseEnrollment.Business
{
    public class EnrollmentBusinessValidator : BusinessValidator<Enrollment>
    {
        public override void Validate(Enrollment record, EntityActionType action)
        {
            base.Validate(record, action);

            var semester = record.SemesterCoursePlan.Semester;

            if (semester.State != SemesterStatus.Registering || 
                semester.EnrollmentStartTime > DateTime.Today || 
                semester.EnrollmentEndTime < DateTime.Today)
            {
                throw this.CreateException("");
            }

        }

    }
}
