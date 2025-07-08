function onHotKeyPress(sender, args) {
    if (args.get_keyName() == "StandardSave") {
        $find("grdParties").fireCommand("Save");
    }
}
