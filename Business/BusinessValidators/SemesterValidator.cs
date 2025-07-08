using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.Utilities;
using SystemGroup.General.CourseEnrollment.Common;

namespace SystemGroup.General.CourseEnrollment.Business.BusinessValidators
{
    public class SemesterValidator : BusinessValidator<Semester>
    {
        public override void Validate(Semester record, EntityActionType action)
        {
            base.Validate(record, action);

            if (action == EntityActionType.Delete)
            {
                return;
            }

            if (record.StartDate > record.EndDate)
            {
                throw this.CreateException("Messages_StartDateAfterEndDate");
            }

            if (record.EnrollmentStartTime > record.EnrollmentEndTime)
            {
                throw this.CreateException("Messages_EnrollmentStartTimeAfterEndTime");
            }

            if (record.StartDate <= record.EnrollmentEndTime)
            {
                throw this.CreateException("Messages_EnrollmentAfterStartDatet");
            }

            if (action == EntityActionType.Update)
            {
                var changeSet = record.GetChangedProperties();

                foreach (var change in changeSet)
                {
                    switch (change.PropertyName)
                    {
                        case "Name":
                            if (record.State != SemesterStatus.Registering)
                            {
                                throw this.CreateException("Messages_SemesterNameCantBeChanged");
                            }

                            break;
                        case "StartDate":
                            if (record.State != SemesterStatus.Finished)
                            {
                                throw this.CreateException("Messages_StartDateCantBeChanged");
                            }

                            break;
                        case "EndDate":
                            if (record.State != SemesterStatus.Finished)
                            {
                                throw this.CreateException("Messages_EndDateCantBeChanged");
                            }

                            break;
                        case "EnrollmentStartTime":
                            if (record.State != SemesterStatus.Registering)
                            {
                                throw this.CreateException("Messages_EnrollmentStartTimeCantBeChanged");
                            }

                            break;
                        case "EnrollmentEndTime":
                            if (record.State != SemesterStatus.Registering)
                            {
                                throw this.CreateException("Messages_EnrollmentEndTimeCantBeChanged");
                            }

                            break;
                    }
                }


            }

            var semesterBusiness = ServiceFactory.Create<ISemesterBusiness>();

            record.Name = record.Name.Trim();

            if (semesterBusiness.FetchByFilter(i => i.Name == record.Name && i.ID != record.ID).Any())
            {
                throw this.CreateException("Messages_NameUniqueness");
            }

            var result = semesterBusiness.FetchByFilter(i =>
                    ((i.StartDate <= record.StartDate && record.StartDate <= i.EndDate) ||
                    (i.StartDate <= record.EndDate && record.EndDate <= i.EndDate) ||
                    (record.StartDate <= i.StartDate && i.EndDate <= record.EndDate)) &&
                    i.ID != record.ID);

            if (result.Any())
            {
                throw this.CreateException("Messages_SemesterTimingConflict");
            }
        }
    }
}
