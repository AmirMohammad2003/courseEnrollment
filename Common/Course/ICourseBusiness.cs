﻿using System;
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
    public interface ICourseBusiness : IBusinessBase<Course>
    {
        [EntityView("AllCourse", "Labels_AllCourse", typeof(CourseProjection), "Name", IsDefaultView = true)]
        new IQueryable<Course> FetchAll();

        [EntityView("AllMajorCourses", "_", typeof(CourseProjection), "Name", ShowInViewList = false)]
        IQueryable<Course> FetchAllMajorCourses(long id);
    }
}
