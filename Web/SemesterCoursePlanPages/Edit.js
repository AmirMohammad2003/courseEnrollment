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
