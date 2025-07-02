using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Eventing;
using SystemGroup.Framework.Exceptions;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.Service.Attributes;
using SystemGroup.Framework.Security;
using SystemGroup.General.CourseEnrollment.Common;
using SystemGroup.General.IPartyManagement.Common;
using SystemGroup.General.PartyManagement.Common;
using SystemGroup.General.Grouping.Common;
using SystemGroup.Framework.Logging;
using SystemGroup.Security.Common;


namespace SystemGroup.General.CourseEnrollment.Business
{
    [Service]
    public class PartyService : ServiceBase, IPartyService
    {

        [ServiceDependency]
        public virtual IPartyManagementService PartyManagementService { get; set; }

        [ServiceDependency]
        public virtual IUserGroupMemberBusiness UserGroupMemberBusiness { get; set; }

        [ServiceDependency]
        public virtual IEntityGroupBusiness EntityGroupBusiness { get; set; }

        [ServiceDependency]
        public virtual IUserService UserService { get; set; }

        public virtual IQueryable<IParty> FetchAllStudentParties()
        {
            return FetchByGroupName("دانشجو");
        }

        public virtual IQueryable<IParty> FetchAllProfessorParties()
        {
            return FetchByGroupName("استاد");
        }

        private IQueryable<IParty> FetchByGroupName(string grpName)
        {
            var grpRefs = UserGroupMemberBusiness.FetchAll();
            var grps = EntityGroupBusiness.FetchAll();
            var users = (from user in UserService.FetchAllUsers()
                         join grpRef in UserGroupMemberBusiness.FetchAll()
                         on user.ID equals grpRef.MemberID
                         join grp in EntityGroupBusiness.FetchAll()
                         on grpRef.GroupRef equals grp.ID
                         where grp.Name == grpName
                         select user.PartyRef).Distinct();

            return from party in PartyManagementService.FetchParties()
                   where users.Contains(party.ID)
                   select party;
        }
    }
}
