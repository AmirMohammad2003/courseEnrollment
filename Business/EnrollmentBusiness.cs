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
        public virtual IPartyMajorBusiness PartyMajorBusiness { get; set; }

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

            return from enrollment in FetchAll()
                   join s in students on enrollment.PartyRef equals s
                   where enrollment.State != EnrollmentStatus.Registering
                   select enrollment;
        }

    }
}
