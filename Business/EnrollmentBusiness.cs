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
using SystemGroup.Framework.Logging;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.Service.Attributes;
using SystemGroup.Framework.Utilities;
using SystemGroup.General.CourseEnrollment.Common;


namespace SystemGroup.General.CourseEnrollment.Business
{
    [Service]
    public class EnrollmentBusiness : BusinessBase<Enrollment>, IEnrollmentBusiness
    {
        [ServiceDependency]
        private IPartyMajorBusiness PartyMajorBusiness => ServiceFactory.Create<IPartyMajorBusiness>();

        [SubscribeTo(typeof(IHostService), "HostStarted")]
        public void OnHostStarted(object sender, EventArgs e)
        {
            BusinessValidationProvider.RegisterValidator<Enrollment>(new EnrollmentBusinessValidator());
        }

        public virtual IQueryable<Enrollment> FetchAllCurrentUserEnrollments()
        {
            return FetchByFilter(i => i.PartyRef == CurrentUserInfo.PartyRef);
        }

        public virtual IQueryable<Enrollment> FetchAllCurrentUserStudentEnrollments()
        {
            var students = PartyMajorBusiness
                .FetchByFilter(i => i.ProfessorPartyRef == CurrentUserInfo.PartyRef)
                .Select(i => i.PartyRef);

            return FetchByFilter(i => students.Contains(i.PartyRef))
                .Where(i => i.State != EnrollmentStatus.Registering);
        }

        public virtual IQueryable<Enrollment> FetchAllCurrentUserStudentNotApprovedEnrollments()
        {
            return FetchAllCurrentUserStudentEnrollments()
                .Where(i => i.State == EnrollmentStatus.WaitingForApproval);
        }
    }
}
