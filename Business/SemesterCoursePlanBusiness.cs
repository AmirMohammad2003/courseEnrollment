using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Eventing;
using SystemGroup.Framework.Exceptions;
using SystemGroup.Framework.Host;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.Service.Attributes;
using SystemGroup.General.CourseEnrollment.Common;


namespace SystemGroup.General.CourseEnrollment.Business
{
    [Service]
    public class SemesterCoursePlanBusiness : BusinessBase<SemesterCoursePlan>, ISemesterCoursePlanBusiness
    {
        [ServiceDependency]
        public virtual IPartyMajorBusiness PartyMajorBusiness { get; set; }

        [SubscribeTo(typeof(IHostService), "HostStarted")]
        public void OnHostStarted(object sender, EventArgs e)
        {
            BusinessValidationProvider.RegisterValidator<SemesterCoursePlan>(new SemesterCoursePlanValidator());
        }

        public virtual IQueryable<SemesterCoursePlan> FetchAllUserEligibleSemesterCoursePlan()
        {
            var partyRef = CurrentUserInfo.PartyRef;
            var partyMajors = PartyMajorBusiness.FetchByFilter(i => i.PartyRef == partyRef);
            var coursePlans = FetchAll(LoadOptions
                .With<SemesterCoursePlan>(i => i.Semester)
                .With<SemesterCoursePlan>(i => i.Enrollments)
                .AssociateWith<SemesterCoursePlan>(plans => plans.Enrollments.Where(i => i.PartyRef == partyRef)));

            return from partyMajor in partyMajors
                   join coursePlan in coursePlans
                   on partyMajor.MajorRef equals coursePlan.MajorRef
                   where coursePlan.Semester.State == SemesterStatus.Registering && coursePlan.Enrollments.Count == 0
                   select coursePlan;
        }

        public virtual IQueryable<SemesterCoursePlanItem> FetchAllSemesterCoursePlanItems(long id)
        {
            return FetchDetail<SemesterCoursePlanItem>().Where(i => i.SemesterCoursePlanRef == id);
        }

        public virtual IQueryable<SemesterCoursePlanItem> FetchAllProfessorSemesterCoursePlanItems()
        {
            return FetchDetail<SemesterCoursePlanItem>().Where(i => i.PartyRef == CurrentUserInfo.PartyRef);
        }
    }
}
