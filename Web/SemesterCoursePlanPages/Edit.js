function sltCourse_itemsRequesting(sender, args) {
    let major = $find("sltMajor");
    args.get_context()["id"] = major.get_selectedID();
}
function sltCourse_selectedIndexChanged(sender, args) {
    let grid = $find("grdCourses");
    let tempEntity = grid.get_tempEntity();
    if (tempEntity != null) {
        Sys.Observer.setValue(tempEntity, "CourseName", sender.getSelectedDataProperty("Name"));
    }
}

function sltParty_selectedIndexChanged(sender, args) {
    let grid = $find("grdCourses");
    let tempEntity = grid.get_tempEntity();
    if (tempEntity != null) {
        Sys.Observer.setValue(tempEntity, "PartyName", sender.getSelectedDataProperty("FullName"));
    }
}
function ds_insertedEntity(sender, args) {
    console.log("there")
    $find("sltMajor").disable();
}

function ds_removedEntity(sender, args) {
    console.log("here")
    if (sender.get_entityList().length == 0 && args.get_entity().SemesterCoursePlanRef == 0)
        $find("sltMajor").enable();
}

