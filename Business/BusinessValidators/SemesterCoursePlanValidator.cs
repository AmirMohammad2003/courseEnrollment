using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.Utilities;
using SystemGroup.General.CourseEnrollment.Common;

namespace SystemGroup.General.CourseEnrollment.Business
{
    public class SemesterCoursePlanValidator : BusinessValidator<SemesterCoursePlan>
    {
        public override void Validate(SemesterCoursePlan record, EntityActionType action)
        {
            base.Validate(record, action);

            record.Load(i => i.SemesterCoursePlanItems);
            var planItems = record.SemesterCoursePlanItems;

            foreach (var item in planItems)
            {
                item.Load(i => i.TimeTables);

                Dictionary<int, List<Pair<int, int>>> intervals = [];
                foreach (var timetable in item.TimeTables)
                {
                    if (timetable.Start >= timetable.End)
                    {
                        throw this.CreateException("Messages_SemesterCoursePlanItemTimeTableInvalid");
                    }
                    if (!intervals.TryGetValue(((int)timetable.DayOfTheWeek), out List<Pair<int, int>> value))
                    {
                        value = [];
                        intervals.Add((int)timetable.DayOfTheWeek, value);
                    }
                    value.Add(new Pair<int, int>(timetable.Start, timetable.End));
                }

                foreach (var it in intervals)
                {
                    var value = it.Value;
                    value.Sort((p1, p2) =>
                    {
                        int result = p1.First.CompareTo(p2.First);
                        if (result != 0)
                        {
                            return result;
                        }

                        return p1.Second.CompareTo(p2.Second);
                    });

                    for (int i = 0; i < value.Count - 1; i++)
                    {
                        if (value[i].Second > value[i + 1].First)
                        {
                            throw this.CreateException("Messages_SemesterCoursePlanItemTimeTableConflict");
                        }
                    }
                }
            }
        }
    }
}
