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
    public interface IMajorBusiness : IBusinessBase<Major>
    {
        [EntityView("AllMajor", "Labels_AllMajor", typeof(MajorProjection), "Name", IsDefaultView = true)]
        new IQueryable<Major> FetchAll();
    }
}
