using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Logging;
using SystemGroup.Framework.Lookup;
using SystemGroup.Framework.MetaData;
using SystemGroup.Framework.MetaData.Mapping;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.StateManagement;

namespace SystemGroup.General.CourseEnrollment.Common
{
    [Serializable]
    [DetailOf(typeof(Course), "CourseRef")]
    [SearchFields]
    [AssociatedWith(typeof(Course), "PrerequisiteCourseRef", AssociationType.ManyToOne)]
    partial class Prerequisite : Entity
    {
        #region Properties

        public string CourseName { get; set; }

        #endregion

        #region Methods

        public override string GetEntityName()
        {
            return "پیشنیاز"; 
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new ReferenceColumnInfo("CourseRef", "_"));
            columns.Add(new ReferenceColumnInfo("PrerequisiteCourseRef", "_"));
        }

        public static void FillExtraProperties(IList<Prerequisite> list)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }
            var withCourseName = (from item in list
            join course in ServiceFactory.Create<ICourseBusiness>().FetchAll() 
            on item.PrerequisiteCourseRef equals course.ID
            select new { course.ID, course.Name }).Distinct().ToDictionary(i => i.ID);

            foreach (var item in list)
            {
                item.CourseName = withCourseName[item.PrerequisiteCourseRef].Name;
            }
        }

        #endregion
    }
}
