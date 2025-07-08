using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemGroup.Framework.MetaData.Mapping;
using SystemGroup.Framework.Service;
using SystemGroup.General.IPartyManagement.Common;
using SystemGroup.General.PartyManagement.Common;

namespace SystemGroup.General.CourseEnrollment.Common
{
    [ServiceInterface]
    public interface IPartyService
    {
        [EntityView("AllStudentParties", "Labels_Students", typeof(PartySimpleProjection), "FullName", "CourseEnrollment.Moderator")]
        IQueryable<IParty> FetchAllStudentParties();

        [EntityView("AllProfessorParties", "Labels_Professors", typeof(PartySimpleProjection), "FullName", "CourseEnrollment.Moderator")]
        IQueryable<IParty> FetchAllProfessorParties();
    }
}
