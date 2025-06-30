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

            record.Load<SemesterCoursePlan>(i => ((Enrollment)i).SemesterCoursePlan);
            var coursePlan = record.SemesterCoursePlan;
            coursePlan.Load<Semester>(i => ((SemesterCoursePlan)i).Semester);
            var semester = coursePlan.Semester;

            if (semester.State != SemesterStatus.Registering || 
                semester.EnrollmentStartTime > DateTime.Today || 
                semester.EnrollmentEndTime < DateTime.Today)
            {
                throw this.CreateException("ثبت نام ترم فقط در زمان مشخص شده مجاز است.");
            }

        }

    }
}
