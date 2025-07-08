using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Lookup;
using SystemGroup.Framework.MetaData;
using SystemGroup.Framework.MetaData.Mapping;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.StateManagement;

namespace SystemGroup.General.CourseEnrollment.Common
{
    [Serializable]
    [DetailOf(typeof(Major), "MajorRef")]
    partial class MajorCourse : Entity
    {
        #region Properties

        public string CourseName { get; set; }

        public int CourseUnits { get; set; }

        #endregion

        #region Methods

        public override string GetEntityName()
        {
            return "Labels_Course"; 
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new ReferenceColumnInfo("MajorRef", "_"));
            columns.Add(new ReferenceColumnInfo("CourseRef", "_"));
        }

        public static void FillExtraProperties(IList<MajorCourse> list)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }

            var courseDetails = (from item in list
            join course in ServiceFactory.Create<ICourseBusiness>().FetchAll() 
            on item.CourseRef equals course.ID
            select new { course.ID, course.Name, course.Units }).Distinct().ToDictionary(i => i.ID);

            foreach (var item in list)
            {
                item.CourseName = courseDetails[item.CourseRef].Name;
                item.CourseUnits = courseDetails[item.CourseRef].Units;
            }
        }

        #endregion
    }
}
