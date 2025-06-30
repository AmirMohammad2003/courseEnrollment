using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.MetaData.Mapping;
using SystemGroup.Framework.Security;
using SystemGroup.Framework.Service;

namespace SystemGroup.General.CourseEnrollment.Common
{
    [ServiceInterface]
    public interface IPartyMajorBusiness : IBusinessBase<PartyMajor>
    {
        [EntityView("AllPartyMajor", "دانشجویان ثبت نامی", typeof(PartyMajorProjection), "PartyName", IsDefaultView = true)]
        new IQueryable<PartyMajor> FetchAll();
    }
}
