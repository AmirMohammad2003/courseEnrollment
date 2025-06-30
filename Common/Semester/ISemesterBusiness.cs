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
    public interface ISemesterBusiness : IBusinessBase<Semester>
    {
        [EntityView("AllSemester", "لیست ترم ها", typeof(SemesterProjection), "Name", IsDefaultView = false)]
        new IQueryable<Semester> FetchAll();

    }
}
