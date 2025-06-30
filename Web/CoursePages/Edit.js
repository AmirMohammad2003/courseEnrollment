function sltCourse_selectedIndexChanged(sender, args) {
    let grid = $find("grdPrerequisites");
    let tempEntity = grid.get_tempEntity();
    if (tempEntity != null) {
        Sys.Observer.setValue(tempEntity, "CourseName", sender.getSelectedDataProperty("Name"));
    }
}

function sltCourse_itemsRequesting(sender, args) {
    let grid = $find("grdPrerequisites");
    let list = grid.get_dataSourceObject().get_entityList();
    let ids = [];
    let editIndex = grid.get_editIndex();
    for (let i = 0; i < list.length; ++i) {
        if (i != editIndex)
            ids.push(list[i].PrerequisiteCourseRef);
    }
    args.get_context()["IgnoredIDs"] = ids;
}