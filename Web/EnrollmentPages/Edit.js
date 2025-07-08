function sltCourse_selectedIndexChanged(sender, args) {
    let grid = $find("grdCourses");
    let tempEntity = grid.get_tempEntity();
    let list = grid.get_dataSourceObject().get_entityList();
    if (tempEntity != null) {
        Sys.Observer.setValue(tempEntity, "CourseName", sender.getSelectedDataProperty("CourseName"));
        Sys.Observer.setValue(tempEntity, "PartyName", sender.getSelectedDataProperty("PartyName"));
    }

}

function sltCourse_selectedIndexChanging(sender, args) {
    console.log("Changing...");
}

function sltCourse_itemsRequesting(sender, args) {
    let grid = $find("grdCourses");
    let list = grid.get_dataSourceObject().get_entityList();
    let ids = [];
    let editIndex = grid.get_editIndex();
    for (let i = 0; i < list.length; ++i) {
        if (i != editIndex)
            ids.push(list[i].SemesterCoursePlanItemRef);
    }
    args.get_context()["IgnoredIDs"] = ids;

    args.get_context()["id"] = $find("sltSemesterCoursePlan").get_selectedID();
}

function ds_insertedEntity(sender, args) {
    console.log("there")
    $find("sltSemesterCoursePlan").disable();
}

function ds_removedEntity(sender, args) {
    console.log("here")
    if (sender.get_entityList().length == 0)
        $find("sltSemesterCoursePlan").enable();
}

function setGridEnabled(enabled) {
    let grid = $find("grdCourses")
    grid.set_allowDelete(enabled);
    grid.set_allowInsert(enabled);
    grid.set_allowEdit(enabled);
}

function sltCoursePlan_selectedIndexChanged(sender, args) {
    let slt = $find("sltSemesterCoursePlan");
    setGridEnabled(slt.get_selectedID() != null);
}
function sltCoursePlan_selectedIndexChanging(sender, args) {
    let grid = $find("grdCourses")
    if (grid.get_dataSourceObject().get_entityList().length > 0) {
        throw Error.create("برنامه تحصیلی حین ثبت نام قابل تغییر نمی باشد.");
    }

}

