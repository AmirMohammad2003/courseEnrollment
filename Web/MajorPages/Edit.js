function IgnoreIDs(grid, args) {
    let list = grid.get_dataSourceObject().get_entityList();
    let ids = [];
    let editIndex = grid.get_editIndex();
    for (let i = 0; i < list.length; ++i) {
        if (i != editIndex)
            ids.push(list[i].CourseRef);
    }
    args.get_context()["IgnoredIDs"] = ids;
}

function sltCourse_selectedIndexChanged(sender, args) {
    let grid = $find("grdCourses");
    let tempEntity = grid.get_tempEntity();
    if (tempEntity != null) {
        Sys.Observer.setValue(tempEntity, "CourseName", sender.getSelectedDataProperty("Name"));
        Sys.Observer.setValue(tempEntity, "CourseUnits", sender.getSelectedDataProperty("Units"));
    }
}

function sltCourse_itemsRequesting(sender, args) {
    let grid = $find("grdCourses");
    IgnoreIDs(grid, args);
}
