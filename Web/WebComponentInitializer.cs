using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SystemGroup.Framework.Security;
using SystemGroup.Framework.Service;
using SystemGroup.General.CourseEnrollment.Common;
using SystemGroup.General.CourseEnrollment.Web;
using SystemGroup.Web;
using SystemGroup.Web.ApplicationServices;
using SystemGroup.Web.UI;
using SystemGroup.Web.UI.Shell;

namespace SystemGroup.General.CourseEnrollment.Web
{
    public class WebComponentInitializer : WebComponentInitializerBase
    {
        #region Semester EntityActions

        [AddNewEntityAction(typeof(Semester), SecurityKey = "CourseEnrollment.Moderator.Semester.New")]
        public void AddNewSemester()
        {
            SgShell.Show<SemesterPages.Edit>();
        }

        [ViewDetailEntityAction(typeof(Semester), SecurityKey = "CourseEnrollment.Moderator.Semester.Edit")]
        public void EditSemester(params long[] ids)
        {
            foreach (long id in ids)
            {
                SgShell.Show<SemesterPages.Edit>("id=" + id);
            }
        }

        [DeleteEntityAction(typeof(Semester), NeedsConfirmation = true, ConfirmationMessage = "آیا از حذف این ترم اطمینان دارید؟", SecurityKey = "CourseEnrollment.Moderator.Semester.Delete")]
        public void DeleteSemester(params long[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                ServiceFactory.Create<ISemesterBusiness>().Delete(ids);
            }
        }

        #endregion

        #region Course EntityActions

        [AddNewEntityAction(typeof(Course), SecurityKey = "CourseEnrollment.Moderator.Course.New")]
        public void AddNewCourse()
        {
            SgShell.Show<CoursePages.Edit>();
        }

        [ViewDetailEntityAction(typeof(Course), SecurityKey = "CourseEnrollment.Moderator.Course.Edit")]
        public void EditCourse(params long[] ids)
        {
            foreach (long id in ids)
            {
                SgShell.Show<CoursePages.Edit>("id=" + id);
            }
        }

        [DeleteEntityAction(typeof(Course), NeedsConfirmation = true, ConfirmationMessage = "آیا از حذف این درس اطمینان دارید؟", SecurityKey = "CourseEnrollment.Moderator.Course.Delete")]
        public void DeleteCourse(params long[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                ServiceFactory.Create<ICourseBusiness>().Delete(ids);
            }
        }

        #endregion

        #region Major EntityActions

        [AddNewEntityAction(typeof(Major), SecurityKey = "CourseEnrollment.Moderator.Major.New")]
        public void AddNewMajor()
        {
            SgShell.Show<MajorPages.Edit>();
        }

        [ViewDetailEntityAction(typeof(Major), SecurityKey = "CourseEnrollment.Moderator.Major.Edit")]
        public void EditMajor(params long[] ids)
        {
            foreach (long id in ids)
            {
                SgShell.Show<MajorPages.Edit>("id=" + id);
            }
        }

        [DeleteEntityAction(typeof(Major), NeedsConfirmation = true, ConfirmationMessage = "آیا از حذف این رشته اطمینان دارید؟", SecurityKey = "CourseEnrollment.Moderator.Major.Delete")]
        public void DeleteMajor(params long[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                ServiceFactory.Create<IMajorBusiness>().Delete(ids);
            }
        }

        #endregion

        #region SemesterCoursePlan EntityActions

        [AddNewEntityAction(typeof(SemesterCoursePlan), SecurityKey = "CourseEnrollment.Moderator.SemesterCoursePlan.New")]
        public void AddNewSemesterCoursePlan()
        {
            SgShell.Show<SemesterCoursePlanPages.Edit>();
        }

        [ViewDetailEntityAction(typeof(SemesterCoursePlan), SecurityKey = "CourseEnrollment.Moderator.SemesterCoursePlan.Edit")]
        public void EditSemesterCoursePlan(params long[] ids)
        {
            foreach (long id in ids)
            {
                SgShell.Show<SemesterCoursePlanPages.Edit>("id=" + id);
            }
        }

        [DeleteEntityAction(typeof(SemesterCoursePlan), NeedsConfirmation = true, ConfirmationMessage = "آیا از حذف این رشته اطمینان دارید؟", SecurityKey = "CourseEnrollment.Moderator.SemesterCoursePlan.Delete")]
        public void DeleteSemesterCoursePlan(params long[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                ServiceFactory.Create<ISemesterCoursePlanBusiness>().Delete(ids);
            }
        }


        [CustomEntityAction(typeof(SemesterCoursePlanItem), "Labels_Students")]
        public void EditSemesterCoursePlanItem(params long[] ids)
        {
            foreach (long id in ids)
            {
                SgShell.Show<SemesterCoursePlanPages.EditClass>("id=" + id);
            }

        }

        #endregion

        #region Enrollment EntityActions

        [AddNewEntityAction(typeof(Enrollment), SecurityKey="CourseEnrollment.Enrollment.New")]
        public void AddNewEnrollment()
        {
            SgShell.Show<EnrollmentPages.Edit>();
        }

        [ViewDetailEntityAction(typeof(Enrollment), SecurityKey="{CourseEnrollment.Enrollment.Edit | CourseEnrollment.Enrollment.Approval}")]
        public void EditEnrollment(params long[] ids)
        {
            foreach (long id in ids)
            {
                SgShell.Show<EnrollmentPages.Edit>("id=" + id);
            }
        }

        #endregion

        #region PartyMajor EntityActions

        [AddNewEntityAction(typeof(PartyMajor), SecurityKey = "CourseEnrollment.Moderator.PartyMajor.New")]
        public void AddNewPartyMajor()
        {
            SgShell.Show<PartyMajorPages.Edit>();
        }

        [ViewDetailEntityAction(typeof(PartyMajor), SecurityKey = "CourseEnrollment.Moderator.PartyMajor.Edit")]
        public void EditPartyMajor(params long[] ids)
        {
            foreach (long id in ids)
            {
                SgShell.Show<PartyMajorPages.Edit>("id=" + id);
            }
        }

        [DeleteEntityAction(typeof(PartyMajor), NeedsConfirmation = true, ConfirmationMessage = "آیا از حذف این ثبت نام دانشجو اطمینان دارید؟", SecurityKey = "CourseEnrollment.Moderator.PartyMajor.Delete")]
        public void DeletePartyMajor(params long[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                ServiceFactory.Create<IPartyMajorBusiness>().Delete(ids);
            }
        }

        #endregion

        #region Methods

        public override List<ComponentLink> RegisterLinks()
        {
            return
            [
                new ComponentLink("University", "Labels_University", "~/Icons/Education.png", null, 3, [
                    new ComponentLink("Student", "Labels_Student", "~/Icons/List.gif", null, 0,
                    [
                        new ComponentLink("ListEnrollments", "Labels_StudentEnrollmentInfo", "~/Icons/BasicInfo.gif", "~/List.aspx?ComponentName=SystemGroup.General.CourseEnrollment&EntityName=Enrollment", 0, "{CourseEnrollment.Enrollment.New | CourseEnrollment.Enrollment.Edit}"),
                        new ComponentLink("Enrollment", "Labels_Enrollment", "~/Icons/Add.gif", "CourseEnrollment.Enrollment.New", typeof(EnrollmentPages.Edit), 0)
                    ]),
                    new ComponentLink("Professor", "Labels_Professor", "~/Icons/BasicInfo.gif", null, 0,
                    [
                        new ComponentLink("ListEnrollments", "Labels_StudentsEnrollmentInfo", "~/Icons/BasicInfo.gif", "~/List.aspx?ComponentName=SystemGroup.General.CourseEnrollment&EntityName=Enrollment", 0, "CourseEnrollment.Enrollment.Approval"),
                        new ComponentLink("ListClasses", "Labels_ListClasses", "~/Icons/List.gif", "~/List.aspx?ComponentName=SystemGroup.General.CourseEnrollment&EntityName=SemesterCoursePlanItem&ViewName=AllProfessorSemesterCoursePlanItems", 1, "CourseEnrollment.Enrollment.Approval"),
                    ]),
                    new ComponentLink("Moderator", "Labels_Moderator", "~/Icons/List.gif", null, 0, "CourseEnrollment.Moderator",
                    [
                        new ComponentLink("Lists", "s:Labels_Lists", "~/Icons/List.gif", null, 0, "CourseEnrollment.Moderator",
                        [
                            new ComponentLink("ListSemester", "Labels_Semester", "~/Icons/BasicInfo.gif", "~/List.aspx?ComponentName=SystemGroup.General.CourseEnrollment&EntityName=Semester", 0, "CourseEnrollment.Moderator.Semester"),
                            new ComponentLink("ListCourses", "Labels_Courses", "~/Icons/BasicInfo.gif", "~/List.aspx?ComponentName=SystemGroup.General.CourseEnrollment&EntityName=Course", 1, "CourseEnrollment.Moderator.Course"),
                            new ComponentLink("ListMajors", "Labels_Majors", "~/Icons/BasicInfo.gif", "~/List.aspx?ComponentName=SystemGroup.General.CourseEnrollment&EntityName=Major", 2, "CourseEnrollment.Moderator.Major"),
                            new ComponentLink("ListPlans", "Labels_SemesterCoursePlans", "~/Icons/BasicInfo.gif", "~/List.aspx?ComponentName=SystemGroup.General.CourseEnrollment&EntityName=SemesterCoursePlan", 3, "CourseEnrollment.Moderator.SemesterCoursePlan"),
                            new ComponentLink("ListStudents", "Labels_Students", "~/Icons/BasicInfo.gif", "~/List.aspx?ComponentName=SystemGroup.General.CourseEnrollment&EntityName=PartyMajor", 3, "CourseEnrollment.Moderator.PartyMajor")
                        ]),
                    ]),
                ])
            ];
        }

        #endregion

    }
}
